using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlightController : MonoBehaviour
{
    [Header("Throttle Settings")]
    public float ThrottlePower = 0.0f;
    public float ThrottleRate  = 4.0f;
    public float MaxThrottle   = 12.0f;
    public float MinAirThrottle = 2.0f;

    [Header("Landing")]
    public float MaxLandingThrottle = 3.0f;
    public float MinLandingPitch = -1.0f;
    public float MaxLandingPitch = 25.0f;

    [Header("Icing")]
    public float Ice = 0.0f;
    public float IcingThreshold = 45.0f;
    public float IcingRate = 1.0f;


    [Header("Attitude Settings")]
    public float PitchRate = 120.0f;
    public float RollRate  = 180.0f;
    public float YawRate   = 100.0f;
    public float Drag = 4.0f;
    public float CurrentDrag = 0.0f;
    public float CurrentPitch = 0.0f;
    

    [Header("Autopilot")]
    public float Altitude = 0.0f;
    public float GroundSpeed = 0.0f;
    public float ClimbRate = 0.0f;
    public float DirectionOfTravel = 0.0f;


    [Header("Controller Inputs")]
    public float ThrottleInput = 0.0f;
    public float PitchInput    = 0.0f;
    public float RollInput     = 0.0f;
    public float YawInput      = 0.0f;
    public bool InvertPitchInput = true;
    public bool Shoot = false;

    public bool Stalled = false;

    

    [Header("Ground Checks")]
    public LayerMask groundMask;
    public GameObject GroundCheck;
    public bool IsGrounded = true;

    [Header("Events")]
    public GameEvent Killed;
    public GameEvent Destroyed;

    private float activeThrottle;
    private Vector3 activeRotation;


    private readonly Collider[] ground = new Collider[1];
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    //Do "physics" here
    private void FixedUpdate()
    {
        Ice = Mathf.Max(Ice + (transform.position.y > IcingThreshold ? Time.fixedDeltaTime : -Time.fixedDeltaTime)*IcingRate,0.0f);        
    }

    //Update controller inputs here
    void Update()
    {
        var grounded = Physics.OverlapSphereNonAlloc(GroundCheck.transform.position, 0.2f, ground, groundMask) > 0;
        RaycastHit hit;
        Ray downRay = new Ray(transform.position, -Vector3.up);
        if(Physics.Raycast(downRay,out hit))
        {
            Altitude = hit.distance;
        }


        CurrentPitch = transform.rotation.eulerAngles.z;
        if(CurrentPitch > 180.0)
        {
            CurrentPitch = -(360.0f - CurrentPitch);
        }
        
        if (!IsGrounded && grounded)
        {
            if(ThrottlePower > MaxLandingThrottle || CurrentPitch < MinLandingPitch || CurrentPitch > MaxLandingPitch)
            {
                //SendMessage("OnCrash", SendMessageOptions.DontRequireReceiver);
            } else
            {
                CurrentPitch = 0;
            }
            IsGrounded = true;
        } else if(IsGrounded && !grounded)
        {
            IsGrounded = false;
        }


        //Apply "drag" such that low throttle at high pitch can result in a stall as
        //in the original game. This drag is reversed at negative pitches as in the
        //original game as well. Don't do it while we're on the ground though, that's weird.
        if (!IsGrounded)
        {
            float absPitch = Mathf.Abs(CurrentPitch);
            absPitch = absPitch > 90.0f ? 180.0f - absPitch : absPitch;
            CurrentDrag = (absPitch / 90.0f) * Drag;
        }
        else
        {
            CurrentDrag = 0.0f;
        }

        if (!Stalled)
        {
            activeThrottle = ThrottleInput * ThrottleRate;
            activeRotation = new Vector3(RollInput * RollRate, YawInput * YawRate, (InvertPitchInput ? -1.0f : 1.0f) * PitchInput * PitchRate);
        } else { 
            float pitchDifference = -90 - CurrentPitch;
            activeRotation = new Vector3(0, 0, Mathf.Sign(pitchDifference) * Mathf.Min(Mathf.Abs(pitchDifference), PitchRate * Time.deltaTime) / Time.deltaTime);
        }

        if (IsGrounded)
        {
            ThrottlePower = Mathf.Clamp(ThrottlePower + activeThrottle * Time.deltaTime, 0.0f, MaxThrottle);
        } else
        {
            ThrottlePower = Mathf.Clamp(ThrottlePower + activeThrottle * Time.deltaTime, Mathf.Min(ThrottlePower,MinAirThrottle), MaxThrottle);
        }

        float velocity = ThrottlePower - Mathf.Sign(CurrentPitch) * (CurrentDrag + Ice);
        if(Stalled && Mathf.Abs(-90 - CurrentPitch) < 1)
        {
            Debug.Log("Can Control!");
            Stalled = false;
        }
        if(!Stalled && velocity < 0)
        {
            Debug.Log("Stalled!");
            Stalled = true;
            ThrottlePower = 0.0f;
        }

        transform.Rotate(activeRotation * Time.deltaTime, Space.Self);
        var lastPosition = transform.position;
        transform.Translate(Vector3.right * velocity * Time.deltaTime, Space.Self);
        ClimbRate = (transform.position.y - lastPosition.y) / Time.deltaTime;
        GroundSpeed = (transform.position.x - lastPosition.x) / Time.deltaTime;
        DirectionOfTravel = Mathf.Sign(GroundSpeed);
        GroundSpeed = Mathf.Abs(GroundSpeed);
    }

    void OnRoll()
    {
        anim.SetBool("Inverted", !anim.GetBool("Inverted"));
    }

}

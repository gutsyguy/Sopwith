using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{


    [Header("Autopilot Targets")]
    public Transform Waypoint;
    public float TargetThrottle;
    public float TargetAltitude;
    public float TargetPitch;


    [Header("Information")]
    public float PositionDeviation;
    public float AltitudeDeviation;
    public float PitchDeviation;


    private FlightController flightController;
    private Vector3 z = new Vector3(0, 0, 1);
    private float updateWait = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        flightController = GetComponent<FlightController>();
    }

    // Update is called once per frame
    void Update()
    {
        //No waypoint or we've been hit. Either way no control!
        if(Waypoint == null || !flightController.Stalled)
        {
            return;
        }
        float throttleAdjust = Mathf.Min(TargetThrottle - flightController.ThrottlePower, flightController.ThrottleRate * Time.deltaTime);

        //Make sure we can actually fly before starting to adjust our flight path
        if (flightController.ThrottlePower >= flightController.MinAirThrottle)
        {
            var waypos = Waypoint.transform.position;
            var mypos = transform.position;

            TargetAltitude = waypos.y;
            PositionDeviation = mypos.x - waypos.x;
            AltitudeDeviation = waypos.y - mypos.y;

            if (updateWait <= 0.0f)
            {
                TargetPitch = Mathf.Atan2(AltitudeDeviation, PositionDeviation) * Mathf.Rad2Deg;
                PitchDeviation = flightController.CurrentPitch - TargetPitch;
                updateWait = PitchDeviation / flightController.PitchRate; //Wait until we complete our turn
            }
            else
            {
                PitchDeviation = flightController.CurrentPitch - TargetPitch;
            }

            transform.Rotate(0, 0, -Mathf.Sign(PitchDeviation) * Mathf.Min(flightController.PitchRate * Time.deltaTime, Mathf.Abs(PitchDeviation)), Space.Self);
            updateWait -= Time.deltaTime;
        }

        flightController.ThrottlePower += throttleAdjust;



    }
}

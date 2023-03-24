using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SopwithControls : MonoBehaviour
{
    private FlightController flightControls;
    private bool canRoll = true;

    // Start is called before the first frame update
    void Start()
    {
        flightControls = GetComponent<FlightController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnPitch(InputValue inputValue)
    {
        flightControls.PitchInput = inputValue.Get<float>();
    }

    void OnThrottle(InputValue inputValue)
    {
        flightControls.ThrottleInput = inputValue.Get<float>();
    }

    void OnRollButton(InputValue inputValue)
    {
        if(inputValue.isPressed)
        {
            if(canRoll)
            {
                SendMessage("OnRoll", SendMessageOptions.DontRequireReceiver);
                canRoll = false;
            }
        } else
        {
            canRoll = true;
        }
    }

    void OnCrash()
    {
        Debug.Log("Crashed!");

    }

    void OnStall()
    {
        Debug.Log("Stalled!");
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    public float MaxSpinRate = 180.0f;

    private FlightController flightController;

    // Start is called before the first frame update
    void Start()
    {
        flightController = GetComponentInParent<FlightController>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(flightController != null)
        {
            float spinSpeed = MaxSpinRate * (flightController.ThrottlePower / flightController.MaxThrottle);
            transform.Rotate(spinSpeed * Time.deltaTime, 0, 0);
        }
    }
}

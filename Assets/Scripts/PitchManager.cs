using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchManager : MonoBehaviour
{
    public GameObject PitchTarget;

    private FlightController flightControls;

    // Start is called before the first frame update
    void Start()
    {
        flightControls = GetComponent<FlightController>();

    }

    // Update is called once per frame
    void Update()
    {
        float AngleDifference = Quaternion.Angle(PitchTarget.transform.rotation, transform.rotation);
        transform.Rotate(Mathf.Min(AngleDifference, flightControls.PitchRate * Time.deltaTime), 0, 0, Space.Self);
    }
}

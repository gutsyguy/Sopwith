using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    [Header("Assignment")]
    public GameObjectValue CurrentWaypoint;

    [Header("Waypoints")]
    public int waypoint;
    public List<Vector2> waypoints;

    private int lastWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        waypoint = lastWaypoint = 0;
        if(CurrentWaypoint != null) {
            CurrentWaypoint.RuntimeValue = gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(waypoint != lastWaypoint)
        {
            transform.position = new Vector3(waypoints[waypoint].x, waypoints[waypoint].y, 0.0f);
            lastWaypoint = waypoint;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            waypoint = (waypoint + 1) % waypoints.Count;
            Debug.Log("Next Waypoint");
        }
    }

}

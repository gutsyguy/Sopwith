using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifecycleTracking : MonoBehaviour
{
    public GameObjectValue Player;
    public bool IsAlive;
    public float Distance;

    public GameObject CurrentPlayer;
    private FlightController flightController;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //If the active player object has changed, update
        if(Player.RuntimeValue != CurrentPlayer)
        {
            CurrentPlayer = Player.RuntimeValue;
            flightController = CurrentPlayer == null ? null : CurrentPlayer.GetComponent<FlightController>();
        }
        IsAlive = (CurrentPlayer != null && flightController != null) ? flightController.Stalled : false;
        if(IsAlive)
        {
            Distance = CurrentPlayer.transform.position.x - transform.position.x;
        }
    }
}

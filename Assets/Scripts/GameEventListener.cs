using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour,IGameEventListener
{
    public GameEvent  Event;
    public UnityEvent<GameEvent,GameObject> Response;

    private void OnEnable()
    {
        Event.Register(this);
    }
    private void OnDisable()
    {
        Event.Unregister(this);
    }
    public void OnEventRaised(GameEvent gameEvent,GameObject gameObject)
    {
        Response.Invoke(gameEvent,gameObject);
    }
}

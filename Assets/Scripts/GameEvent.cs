using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<IGameEventListener> listeners = new List<IGameEventListener>();

    public void Raise(GameObject source)
    {
        for(int i=0;i<listeners.Count;i++)
        {
            listeners[i].OnEventRaised(this,source);
        }
    }

    public void Register(IGameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void Unregister(IGameEventListener listener)
    {
        listeners.Remove(listener);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameObjectValue : ScriptableObject, ISerializationCallbackReceiver
{
    [NonSerialized]
    public GameObject RuntimeValue;

    public void OnAfterDeserialize()
    {
    }

    public void OnBeforeSerialize()
    {
    }

}

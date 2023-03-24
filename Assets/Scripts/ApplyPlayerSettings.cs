using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyPlayerSettings : MonoBehaviour
{
    public PlayerSettings PlayerSettings;


    // Start is called before the first frame update
    void Start()
    {
        if(PlayerSettings != null)
        {
            PlayerSettings.ApplySettings(transform);
        }
    }

}

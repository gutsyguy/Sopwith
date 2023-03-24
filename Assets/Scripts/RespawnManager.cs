using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RespawnManager : MonoBehaviour, IGameEventListener
{
    public float RespawnWaitTime = 3.0f;
    public GameObject Spawn;
    public GameObject Camera;

    public GameEvent       SpawnEvent;
    public GameEvent RespawnEvent;
    public GameObjectValue Current;
    

    private void SpawnObject()
    {
        var pos = transform.GetChild(0).position;
        GameObject obj = Instantiate(Spawn);
        obj.transform.position = pos;
        if(Camera != null)
        {
            var cam = Camera.GetComponent<CinemachineVirtualCamera>();
            cam.Follow = obj.transform;
            cam.LookAt = obj.transform;
        }
        if (Current != null)
        {
            Current.RuntimeValue = obj;
        }
        obj.SendMessage("OnSpawned");
        SpawnEvent.Raise(obj);
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(RespawnWaitTime);
        SpawnObject();
    }


    // Start is called before the first frame update
    IEnumerator Start()
    {
        if(RespawnEvent != null)
        {
            RespawnEvent.Register(this);
        }
        //Wait until after we complete the first frame to initiate the spawn
        //event. This avoids race conditions with registering for the spawn
        //event within the game.
        yield return new WaitForEndOfFrame();
        SpawnObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEventRaised(GameEvent gameEvent, GameObject source)
    {
        Debug.Log("Got Respawn Event");
        if (Current != null)
        {
            Current.RuntimeValue = null;
        }
        Destroy(source); //Actually remove the object
        StartCoroutine(Respawn());
    }
}

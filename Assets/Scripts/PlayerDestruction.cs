using UnityEngine;
using System.Collections;

public class PlayerDestruction : MonoBehaviour
{
    public GameEvent cameraTrackEvent;
    public GameObject lightDeathPrefab;
    public GameObject heavyDeathPrefab;

    // Use this for initialization
    void Start()
    {
        if(TryGetComponent<Destructible>(out var d))
        {
            d.OnDestroyed += OnDestroyed;
        }
    }

    private void OnDestroyed(float amount,float Health,float percent)
    {
        var go = Health < -10 ? Instantiate(lightDeathPrefab) : Instantiate(heavyDeathPrefab);
        if(cameraTrackEvent != null)
        {
            cameraTrackEvent.Raise(go);
        }
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;
        if(TryGetComponent<Rigidbody>(out var r))
        {
            r.AddForce(Vector2.right);
        }
    }

}

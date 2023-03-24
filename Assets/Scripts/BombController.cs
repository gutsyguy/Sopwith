using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public Vector3 velocity;
    
    public float gravity = 10.0f;

    private float gravityVelocity = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        gravityVelocity += gravity * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocity * Time.deltaTime,Space.Self);
        transform.Translate(0, -gravityVelocity * Time.deltaTime, 0, Space.World);
        if(transform.position.y < -100) // Just in case
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit " + other + " on object " + other.gameObject.name);

        gameObject.SendMessage("OnExplode", SendMessageOptions.DontRequireReceiver);
        other.gameObject.SendMessage("OnExplode",SendMessageOptions.DontRequireReceiver);

        Destroy(gameObject);
    }
}

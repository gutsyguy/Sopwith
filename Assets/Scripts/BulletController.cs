using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float duration = 10.0f;
    public Vector3 velocity;

    private double start;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, duration);
    }

    private void Awake()
    {
        start = Time.timeAsDouble;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocity * Time.deltaTime, Space.Self);
    }
}

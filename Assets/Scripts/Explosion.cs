using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Transform ExplosionPoint;
    public float ExplosionForceMin = 400.0f;
    public float ExplosionForce = 400.0f;
    public float HorizontalFactor = 0.1f;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForFixedUpdate();
        Debug.Log("Exploding from: "+transform.position);
        
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Rigidbody>(out var rb))
            {
                child.parent = null;
                
                Vector3 forceVector = Vector3.up * Random.Range(ExplosionForceMin,ExplosionForce) + Vector3.right * (child.position.x < ExplosionPoint.position.x ? 1 : -1) * ExplosionForce * HorizontalFactor;
                rb.AddForce(forceVector);
                rb.AddTorque(Vector3.right*ExplosionForce);
            }
        }
    }
}

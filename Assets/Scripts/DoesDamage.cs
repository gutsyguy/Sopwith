using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoesDamage : MonoBehaviour
{
    public float Damage = 1.0f;
    public bool Reciprocal = true;
    public bool IgnoreSelf = true;
    public GameObject ImpactVfxPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If IgnoreSelf is active then ignore other objects with the same tag as us.
        if(IgnoreSelf && gameObject.tag == collision.gameObject.tag)
        {
            return;
        }

        //If the other object is destructible apply damage to it
        if(collision.gameObject.TryGetComponent<IDestructible>(out var d))
        {
            d.Damage(Damage, collision.GetContact(0).point);
        }
        //If we take reciprocal damage do that, otherwise just eliminate
        //ourselves
        if(TryGetComponent<IDestructible>(out var d2))
        {
            d2.Damage(Damage, collision.GetContact(0).point);
        } else
        {
            Destroy(gameObject);
        }
        //Finally if we have impact visual effects, create them
        if(ImpactVfxPrefab != null)
        {
            var go = Instantiate(ImpactVfxPrefab);
            go.transform.position = collision.GetContact(0).point;
            go.transform.rotation = transform.rotation;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destructible : MonoBehaviour, IDestructible
{
    [Header("Health Information")]
    public float Health = 100.0f;
    public float HealthPercent
    {
        get
        {
            return 100.0f * Health / initialHealth;
        }
    }

    [Header("Optional Prefab")]
    public GameObject DestroyedPrefab;

    public delegate void OnDamageDelegate(float amount, float Health, float percent);
    public OnDamageDelegate OnDamage;

    public delegate void OnDestroyedDelegate(float amount, float Health, float percent);
    public OnDestroyedDelegate OnDestroyed;

    public delegate void OnHealDelegate(float amount, float Health, float percent);
    public OnHealDelegate OnHeal;
    
    
    private float initialHealth;

    public void Start()
    {
        initialHealth = Health;
    }

    public void Damage(float amount, Vector3 damageLocation)
    {
        Health -= amount;
        if(Health <= 0.0f)
        {
            if (DestroyedPrefab != null)
            {
                var go = Instantiate(DestroyedPrefab);
                go.transform.position = transform.position;
                go.transform.rotation = transform.rotation;                
            } else
            {
                if(OnDestroyed != null)
                {
                    OnDestroyed(amount, Health, -(-Health / initialHealth));
                }
            }
            Destroy(gameObject);

        }
        else
        {
            if(OnDamage != null)
            {
                OnDamage(amount, Health, HealthPercent);
            }
        }
    }

    public void Heal(float amount)
    {
        Health += amount;
        if(Health > initialHealth)
        {
            Health = initialHealth;
            if(OnHeal != null)
            {
                OnHeal(amount, Health, HealthPercent);
            }
        }
    }

}

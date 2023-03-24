using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireGun : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform GunTransform;
    public IntegerValue Rounds;

    public float RateOfFire = 2.0f;
    public float GunVelocity = 30.0f;

    public bool Shoot = false;

    private bool ShouldReset = false;

    private double cooldownTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnSpawned()
    {
        if(Rounds != null)
        {
            Rounds.RuntimeValue = Rounds.InitialValue;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if(Shoot && cooldownTime <= 0.0f && (Rounds == null || Rounds.RuntimeValue > 0))
        {
            if(ShouldReset)
            {
                Shoot = false;
                ShouldReset = false;
            }
            cooldownTime = 1.0f / RateOfFire; ;
            GameObject bullet = Instantiate(BulletPrefab);
            if (Rounds != null)
            {
                Rounds.RuntimeValue--;
            }
            bullet.transform.position = GunTransform.position;
            bullet.transform.rotation = GunTransform.rotation;
            var flightCtrl = GetComponent<FlightController>();
            var bulletCtrl = bullet.GetComponent<BulletController>();
            if(bulletCtrl != null)
            {
                bulletCtrl.velocity = Vector3.right * (flightCtrl.ThrottlePower + GunVelocity);
            }

        }

        if(cooldownTime > 0.0f)
        {
            cooldownTime -= Time.deltaTime;
        }
        
    }

    void OnShoot(InputValue inputValue)
    {
        Shoot = inputValue.isPressed;
    }

    public void OnFire()
    {
        if (!Shoot)
        {
            Shoot = true;
            ShouldReset = true;
        }
    }


}

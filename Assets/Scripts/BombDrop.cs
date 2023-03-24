using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BombDrop : MonoBehaviour
{
    public GameObject BombPrefab;
    public Transform DropTransform;
    public IntegerValue Bombs;

    private bool BombReady = true;

    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSpawned()
    {
        if(Bombs != null)
        {
            Bombs.RuntimeValue = Bombs.InitialValue;
        }
    }

    void OnBomb(InputValue value)
    {
        if(BombReady && Bombs.RuntimeValue > 0)
        {
            if(value.isPressed)
            {
                BombReady = false;
                GameObject bomb = Instantiate(BombPrefab);
                Bombs.RuntimeValue--;
                bomb.transform.position = DropTransform.position;
                bomb.transform.rotation = transform.rotation;
                var flightCtrl = GetComponent<FlightController>();
                var bombCtrl = bomb.GetComponent<BombController>();
                if(bombCtrl != null)
                {
                    bombCtrl.velocity = Vector3.right * flightCtrl.ThrottlePower;
                }
            }
        } else {
            if(!value.isPressed)
            {
                BombReady = true;
            }
        }
    }
}

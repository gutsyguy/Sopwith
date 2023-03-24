using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombable : MonoBehaviour
{
    public IntegerValue ScoreValue;
    public int ScoreAmount;

    public GameObject DebrisPrefab;
    public GameObject LiveObject;
    public GameObject DeadObject;

    public int DebrisAmount = 30;
    public float DebrisForce = 180.0f;



    // Start is called before the first frame update
    void Start()
    {
        LiveObject.SetActive(true);
        DeadObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnExplode()
    {
        Debug.Log("Kaboom!");
        if(ScoreValue != null)
        {
            ScoreValue.RuntimeValue += ScoreAmount;
        }
        LiveObject.SetActive(false);
        DeadObject.SetActive(true);
        if(DebrisPrefab != null)
        {
            for(int i=0;i<DebrisAmount;i++)
            {
                var obj = Instantiate(DebrisPrefab);
                obj.SendMessage("OnApplyForce", DebrisForce);
            }
        }
    }
}

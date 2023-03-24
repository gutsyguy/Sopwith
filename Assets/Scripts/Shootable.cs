using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{
    public IntegerValue ScoreValue;
    public int ScoreAmount;

    public GameObject LiveObject;
    public GameObject DeadObject;

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

    public void OnHit()
    {
        Debug.Log("Bang!");
        if(ScoreValue != null)
        {
            ScoreValue.RuntimeValue += ScoreAmount;
        }
        LiveObject.SetActive(false);
        DeadObject.SetActive(true);
    }

}

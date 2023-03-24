using UnityEngine;
using System.Collections;

//Respawn a new object after a fixed amount of time
public class Respawn : MonoBehaviour
{
    public float wait = 30.0f;
    public GameObject SpawnPrefab;

    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForSeconds(wait);
        var go = Instantiate(SpawnPrefab);
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;
        Destroy(gameObject);
    }
}

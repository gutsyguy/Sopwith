using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public IntegerValue Ammo;
    public IntegerValue Bombs;
    public IntegerValue Score;


    private TextMeshProUGUI ammo;
    private TextMeshProUGUI bombs;
    private TextMeshProUGUI score;

    private void PlayerSpawned(GameEvent gameEvent, GameObject Player)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<GameEventListener>().Response.AddListener(PlayerSpawned);

        ammo = transform.Find("Ammo").GetComponent<TextMeshProUGUI>();
        bombs = transform.Find("Bombs").GetComponent<TextMeshProUGUI>();
        score = transform.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        bombs.text = "Bombs: " + Bombs.RuntimeValue;
        ammo.text = "Ammo: "   + Ammo.RuntimeValue;
        score.text = "" + Score.RuntimeValue;
    }
}

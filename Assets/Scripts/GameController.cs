using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameObject[] players;
    int prevTurn, curTurn;

    void Init() {
        // prevTurn = 0;
        // curTurn = players.Length - 1;

        foreach (GameObject p in players)
            p.GetComponent<CannonController>().ChangeTurn += ChangeTurn;
        players[0].GetComponent<PlayerController>().CamSetting(new Rect(0.0f, 0.0f, 0.5f, 1.0f));
        players[1].GetComponent<PlayerController>().CamSetting(new Rect(0.5f, 0.0f, 1.0f, 1.0f));

        prevTurn = 0;
        curTurn = 1;
    }

    void Awake() {
        Init();
    }

    // Start is called before the first frame update
    void Start() {
        ChangeTurn();
    }

    // Update is called once per frame
    void Update() {

    }

    void ChangeTurn() {
        prevTurn = curTurn;
        curTurn = (curTurn + 1) % players.Length;

        players[prevTurn].GetComponent<PlayerController>().TurnOff();
        players[curTurn].GetComponent<PlayerController>().TurnOn();
    }
}

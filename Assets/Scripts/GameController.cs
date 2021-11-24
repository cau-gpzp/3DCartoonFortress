using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameObject[] players;
    int prevTurn, curTurn;

    void Init() {
        prevTurn = 0;
        curTurn = players.Length - 1;

        foreach (GameObject p in players)
            p.GetComponent<CannonController>().ChangeTurn += ChangeTurn;
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

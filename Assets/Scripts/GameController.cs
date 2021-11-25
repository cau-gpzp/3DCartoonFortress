using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameObject[] players;
    int prevTurn, curTurn;

    void Init() {
        // prevTurn = 0;
        // curTurn = players.Length - 1;

        for(int i = 0;i < players.Length;++i) {
            GameObject p = players[i];
            p.GetComponent<CannonController>().ChangeTurn += ChangeTurn;
            HealthController h = p.GetComponent<HealthController>();
            h.id = i;
            h.gc = this;
        }
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

    public void Died(int loser) {
        int winner = 1 - loser;

        GameEnd(winner, loser);
    }

    void GameEnd(int winner, int loser) {
        Debug.Log(System.String.Format("Winner: {0}", winner));
        Debug.Log(System.String.Format("Loser: {0}", loser));
        
#if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}

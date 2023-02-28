using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int sumTotal;
    private TurnController tc;

    public enum State { selectToken, inMatch, endMatch, dead };
    public State currState;

    // Start is called before the first frame update
    void Start()
    {
        currState = State.selectToken;
    }

    private void Awake()
    {
        tc = GetComponent<TurnController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currState)
        {
            case State.selectToken:
                break;
            case State.inMatch:
                break;
            case State.endMatch:
                break;
            case State.dead:
                break;
        }
    }
    public void setSumTotal()
    {
        sumTotal = 0;
        foreach (PlayerController player in tc.getPlayers())
        {
            sumTotal += player.num;
        }
    }
}

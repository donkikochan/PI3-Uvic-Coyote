using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{

    private List<PlayerController> players = new List<PlayerController>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<PlayerController> getPlayers()
    {
        return players;
    }
    public void addPlayer(PlayerController newPlayer)
    {
        players.Add(newPlayer);
    }
    public void startGame()
    {
        int randNum = Random.Range(0, players.Count);
        players[randNum].activeTurn = true;
    }
}


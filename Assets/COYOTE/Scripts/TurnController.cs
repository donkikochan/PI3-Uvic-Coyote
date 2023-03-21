using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{

    private List<PlayerController> players = new List<PlayerController>();
    public GameObject turnIndicator;
    public GameManager gm;
    private int actPlayer, prevPlayer;
    // Start is called before the first frame update
    void Start()
    {
        turnIndicator.SetActive(false);
    }

    private void Awake()
    {
        gm = GetComponent<GameManager>();
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
        Debug.Log("addPlayer");
        players.Add(newPlayer);
    }
    public void startGame()
    {
        actPlayer = Random.Range(0, players.Count);
        players[actPlayer].activeTurn = true;
        turnIndicator.SetActive(true);
        rotateTurnIndicator();
    }   

    public void nextTurn()
    {
        players[actPlayer].activeTurn = false;
        prevPlayer = actPlayer;
        actPlayer++;
        actPlayer = actPlayer >= players.Count ? 0 : actPlayer;
        players[actPlayer].activeTurn = true;
        rotateTurnIndicator();
    }
    public void endGame()
    {
        players[actPlayer].activeTurn = false;
        if(players[prevPlayer].getSelectedNum() <= gm.getSumTotal())
        {
            players[actPlayer].addLoss();
        }
        else
        {
            players[prevPlayer].addLoss();
        }
        turnIndicator.SetActive(false);
    }
    void rotateTurnIndicator()
    {
        Vector3 tiLookingPoint = players[actPlayer].transform.position;
        tiLookingPoint.y = turnIndicator.transform.position.y;
        turnIndicator.transform.LookAt(tiLookingPoint);
    }
}


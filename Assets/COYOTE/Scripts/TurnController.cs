using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public static TurnController instance;


    private List<PlayerController> players = new List<PlayerController>();
    public bool playerIsFirst;
    public GameObject turnIndicator;
    public GameManager gm;
    private int actPlayer, prevPlayer;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        turnIndicator.SetActive(false);
    }

    private void Awake()
    {
        instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public PlayerController getActualPlayer()
    {
        return players[actPlayer];
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
        if (playerIsFirst)
        {
            for(int i = 0;i<players.Count;i++)
            {
                if (players[i].isMine)
                {
                    actPlayer = i;
                    break;
                }
            }
        }
        else
        {
            actPlayer = Random.Range(0, players.Count);
        }
        getActualPlayer().activeTurn = true;
        turnIndicator.SetActive(true);
        rotateTurnIndicator();
    }   

    public void nextTurn()
    {
        getActualPlayer().activeTurn = false;
        prevPlayer = actPlayer;
        actPlayer++;
        actPlayer = actPlayer >= players.Count ? 0 : actPlayer;
        getActualPlayer().activeTurn = true;
        rotateTurnIndicator();
    }
    public void endGame()
    {
        getActualPlayer().activeTurn = false;
        if(getActualPlayer().getSelectedNum() <= gm.getSumTotal())
        {
            getActualPlayer().addLoss();
        }
        else
        {
            getActualPlayer().addLoss();
        }
        turnIndicator.SetActive(false);
    }
    void rotateTurnIndicator()
    {
        Vector3 tiLookingPoint = getActualPlayer().transform.position;
        tiLookingPoint.y = turnIndicator.transform.position.y;
        turnIndicator.transform.LookAt(tiLookingPoint);
    }
}


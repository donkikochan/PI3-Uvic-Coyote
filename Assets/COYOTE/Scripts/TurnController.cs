using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public static TurnController instance;


    private List<PlayerController> players = new List<PlayerController>();
    public bool playerIsFirst;
    public int turnNum;
    public float secondsAfterEndGame;
    public GameObject turnIndicator;
    public GameManager gm;
    private int actPlayer;
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
    public PlayerController getPrevPlayer()
    {
        return players[actPlayer - 1 < 0 ? players.Count - 1 : actPlayer-1];
    }

    public List<PlayerController> getPlayers()
    {
        return players;
    }
    public int GetTurnNum()
    {
        return turnNum;
    }
    public void addPlayer(PlayerController newPlayer)
    {
        Debug.Log("addPlayer with name: "+newPlayer.name);
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
        turnNum = 1;
        getActualPlayer().setActiveTurn(true);
        LastNumScreenController.UpdateInfoTexts();
        turnIndicator.SetActive(true);
        rotateTurnIndicator();
    }

    public void nextTurn()
    {
        getActualPlayer().setActiveTurn(false);
        //prevPlayer = actPlayer;
        turnNum++;
        actPlayer++;
        actPlayer = actPlayer >= players.Count ? 0 : actPlayer;
        getActualPlayer().setActiveTurn(true);
        rotateTurnIndicator();
    }
    public void endGame()
    {
        Debug.Log("Ending Game.");
        getActualPlayer().setActiveTurn(false);
        if (getPrevPlayer().getSelectedNum() <= gm.getSumTotal())
        {
            Debug.Log("ActualPlayer Loss: "+ getPrevPlayer().getSelectedNum()+" is less than "+ gm.getSumTotal());
            if (getActualPlayer().addLoss() >= 3)
            {
                StartCoroutine("EndGameTimer");
            }
            else
            {
                StartCoroutine("NextGameTimer");
            }
        }
        else
        {
            Debug.Log("PreviousPlayer Loss: "+ getPrevPlayer().getSelectedNum()+ " is more than "+gm.getSumTotal());
            if (getPrevPlayer().addLoss() >= 3)
            {
                StartCoroutine("EndGameTimer");
            }
            else
            {
                StartCoroutine("NextGameTimer");
            }
        }
        
        
    }
    void rotateTurnIndicator()
    {
        Vector3 tiLookingPoint = getActualPlayer().transform.position;
        tiLookingPoint.y = turnIndicator.transform.position.y;
        turnIndicator.transform.LookAt(tiLookingPoint);
    }
    IEnumerator NextGameTimer()
    {
        turnIndicator.SetActive(false);
        yield return new WaitForSeconds(secondsAfterEndGame);
        foreach(PlayerController pc in players)
        {
            pc.eraseToken();
        }
        GameManager.instance.changeState(GameManager.State.selectToken);
    }
    IEnumerator EndGameTimer()
    {
        turnIndicator.SetActive(false);
        yield return new WaitForSeconds(secondsAfterEndGame);
        foreach (PlayerController pc in players)
        {
            pc.eraseToken();
        }
        GameManager.instance.changeState(GameManager.State.endMatch);
    }
}


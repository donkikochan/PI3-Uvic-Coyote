using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    public List<int> allTokenNums = new List<int>();
    public float maxDistanceToSpawnToken = 1.0f;
    public int tokenPerRotation = 8;
    public GameObject tokenPrefab;
    public Transform tokenSpawner;

    private int sumTotal;
    private TurnController tc;

    public enum State { selectToken, inMatch, endMatch, dead };
    public State currState;

    // Start is called before the first frame update
    void Start()
    {
        currState = State.selectToken;
        spawnTokens();
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
    // Suma total de tots els tokens de la partida
    public void setSumTotal()
    {
        sumTotal = 0;
        foreach (PlayerController player in tc.getPlayers())
        {
            sumTotal += player.tokenNum;
        }
    }
    public int getSumTotal()
    {
        return sumTotal;
    }
    // TODO -- Spawn dels tokens en una àrea circular al voltant del "TokenSpawner"
    public void spawnTokens()
    {
        int i = 0;
        Transform tokenSpawnPos = tokenSpawner.GetChild(0);
        foreach (int tokenNum in allTokenNums)
        {
            GameObject newToken = Instantiate(tokenPrefab,
                new Vector3(tokenSpawnPos.position.x,
                tokenSpawnPos.position.y + (Mathf.Ceil(i/tokenPerRotation)*0.1f),
                tokenSpawnPos.position.z), Quaternion.identity);
            
            newToken.GetComponent<TokenController>().setNum(tokenNum);
            newToken.transform.LookAt(tokenSpawner);
            newToken.transform.Rotate(new Vector3(0, 90, 90));

            tokenSpawner.Rotate(new Vector3(0, (1f / tokenPerRotation) * 360, 0));
            i++;
        }
        
    }
}

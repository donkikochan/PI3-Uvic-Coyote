using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    #region SelectToken State - Attributes
    [Header("SelectToken State")]
    
    [Header("Generaci� de tokens")]
    public List<int> allTokenNums = new List<int>();
    public float distanceFromTokenSpawnerCenter = 1.0f;
    public float addDistanceFromTokenSpawnerCenter = 0.1f;
    public int tokenPerRotation = 8;
    public GameObject tokenPrefab;
    public Transform tokenSpawner;

    List<TokenController> allTokens = new List<TokenController>();
    #endregion



    private int sumTotal;
    private TurnController tc;

    public enum State { selectToken, inMatch, endMatch, dead };
    [Space(20)]
    public State currState;

    // Start is called before the first frame update
    void Start()
    {
        changeState(State.selectToken);
        
        
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
    void changeState(State state)
    {
        switch (state)
        {
            case State.selectToken:
                //Inici de l'spawn dels Tokens
                StartCoroutine(SpawnTokens());
                break;
            case State.inMatch:
                foreach(TokenController token in allTokens)
                {
                    if(!token.isSelected) Destroy(token.gameObject);
                }
                allTokens.Clear();
                tc.startGame();
                break;
            case State.endMatch:
                break;
            case State.dead:
                break;
        }
        Debug.Log("changeState: to " + state);
        currState = state;
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
    private void OnEnable()
    {
        PlayerController.OnPlayerAddedToken += checkStateEnded;
    }
    private void OnDisable()
    {
        PlayerController.OnPlayerAddedToken -= checkStateEnded;
    }
    #region SelectToken State - Methods
    //Spawn dels tokens en una �rea circular al voltant del "TokenSpawner" cada 0.1 segons
    IEnumerator SpawnTokens()
    {
        Transform tokenSpawnPos = tokenSpawner.GetChild(0);
        tokenSpawnPos.localPosition = new Vector3(0, 0, distanceFromTokenSpawnerCenter);
        List<int> selectedTokens = new List<int>();

        for(int i = 0; i < allTokenNums.Count; i++)
        {
            //Gesti� de llistes per generar tots els tokens aleat�riament cada partida
            int randomTokenPos = Random.Range(0, allTokenNums.Count);
            while (selectedTokens.Contains(randomTokenPos)){
            randomTokenPos = Random.Range(0, allTokenNums.Count);
            }
            int selectedTokenNum = allTokenNums[randomTokenPos];
            selectedTokens.Add(randomTokenPos);
            //Generaci� del Token amb el nombre corresponent
            GameObject newToken = Instantiate(tokenPrefab,
                new Vector3(tokenSpawnPos.position.x,
                tokenSpawnPos.position.y + (Mathf.Ceil(i / tokenPerRotation) * 0.1f),
                tokenSpawnPos.position.z), Quaternion.identity);

            allTokens.Add(newToken.GetComponent<TokenController>());
            newToken.GetComponent<TokenController>().setNum(selectedTokenNum);
            newToken.transform.LookAt(tokenSpawner);
            newToken.transform.Rotate(new Vector3(0, 90, 90));

            tokenSpawner.Rotate(new Vector3(0, (1f / tokenPerRotation) * 360, 0));


            //Comprovar si s'ha donat una volta sencera per afegir m�s distancia
            if ((i) % (tokenPerRotation) == 0)
            {
                tokenSpawnPos.localPosition = tokenSpawnPos.localPosition + new Vector3(0, 0, addDistanceFromTokenSpawnerCenter);
            }
            yield return new WaitForSeconds(0.1f);
        }
        updateBots();
        
    }
    void updateBots()
    {
        foreach (PlayerBot bot in FindObjectsOfType<PlayerBot>())
        {
            bot.setAvaiableTokens(allTokens);
        }
    }
    bool haveAllPlayersChoosenToken()
    {
        bool tmp = true;
        for(int i = 0; i < tc.getPlayers().Count; i++)
        {
            if (!tc.getPlayers()[i].hasToken())
            {
                tmp = false;
                break;
            }
        }
        return tmp;
    }
    void checkStateEnded()
    {
        Debug.Log("checkStateEnded");
        switch (currState)
        {
            case State.selectToken:
                if (haveAllPlayersChoosenToken())
                {
                    changeState(State.inMatch);
                }
                break;
            case State.inMatch:
                break;
            case State.endMatch:
                break;
            case State.dead:
                break;
        }
        
    }
    #endregion
    #region InGame State - Methods

    #endregion
}

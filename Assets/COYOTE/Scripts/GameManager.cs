using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [Header("SelectToken State")]
    [Header("Generació de tokens")]
    public List<int> allTokenNums = new List<int>();
    public float distanceFromTokenSpawnerCenter = 1.0f;
    public float addDistanceFromTokenSpawnerCenter = 0.1f;
    public int tokenPerRotation = 8;
    public GameObject tokenPrefab;
    public Transform tokenSpawner;

    List<TokenController> allTokens = new List<TokenController>();

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
                break;
            case State.endMatch:
                break;
            case State.dead:
                break;
        }
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
    #region SelectToken State - Methods
    //Spawn dels tokens en una àrea circular al voltant del "TokenSpawner" cada 0.1 segons
    IEnumerator SpawnTokens()
    {
        Transform tokenSpawnPos = tokenSpawner.GetChild(0);
        tokenSpawnPos.localPosition = new Vector3(0, 0, distanceFromTokenSpawnerCenter);
        List<int> selectedTokens = new List<int>();

        for(int i = 0; i < allTokenNums.Count; i++)
        {
            //Gestió de llistes per generar tots els tokens aleatòriament cada partida
            int randomTokenPos = Random.Range(0, allTokenNums.Count);
            while (selectedTokens.Contains(randomTokenPos)){
            randomTokenPos = Random.Range(0, allTokenNums.Count);
            }
            int selectedTokenNum = allTokenNums[randomTokenPos];
            selectedTokens.Add(randomTokenPos);
            //Generació del Token amb el nombre corresponent
            GameObject newToken = Instantiate(tokenPrefab,
                new Vector3(tokenSpawnPos.position.x,
                tokenSpawnPos.position.y + (Mathf.Ceil(i / tokenPerRotation) * 0.1f),
                tokenSpawnPos.position.z), Quaternion.identity);

            allTokens.Add(newToken.GetComponent<TokenController>());
            newToken.GetComponent<TokenController>().setNum(selectedTokenNum);
            newToken.transform.LookAt(tokenSpawner);
            newToken.transform.Rotate(new Vector3(0, 90, 90));

            tokenSpawner.Rotate(new Vector3(0, (1f / tokenPerRotation) * 360, 0));


            //Comprovar si s'ha donat una volta sencera per afegir més distancia
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
    #endregion
}

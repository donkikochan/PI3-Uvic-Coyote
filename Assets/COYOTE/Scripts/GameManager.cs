using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [Header("Generació de players")]
    public GameObject playerPrefab;
    public Transform playerSpawnPts;
    #region SelectToken State - Attributes
    [Header("SelectToken State")]
    
    [Header("Generació de tokens")]
    public List<int> allTokenNums = new List<int>();
    public float distanceFromTokenSpawnerCenter = 1.0f;
    public float addDistanceFromTokenSpawnerCenter = 0.1f;
    public int tokenPerRotation = 8;
    public GameObject tokenPrefab;
    public Transform tokenSpawner;

    List<TokenController> allTokens = new List<TokenController>();
    #endregion

    public class InfoPanelEvent : UnityEvent<List<string>>
    {
    }
    public InfoPanelEvent onInfoPanelChange = new InfoPanelEvent();

    public int lastNum;
    
    private int sumTotal;
    private TurnController tc;
    private GameObject infoPanel;

    public static GameManager instance;

    public enum State { selectToken, inMatch, endMatch, dead };
    [Space(20)]
    public State currState;
    public GameObject keyboardPrefab;
    public GameObject fullTotemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        tc = TurnController.instance;
        for(int i = 0; i < playerSpawnPts.childCount; i++)
        {
            GameObject newPlayer = Instantiate(playerPrefab, playerSpawnPts.GetChild(i).position, playerSpawnPts.GetChild(i).rotation);
            if (i == 0) {
                newPlayer.GetComponent<PlayerController>().setMine(true);
            } else { newPlayer.GetComponent<PlayerController>().setMine(false); };
        }
        changeState(State.selectToken);
        onInfoPanelChange.AddListener(infoPanelListener);
        showInfoPanel(false);
        
    }

    private void Awake()
    {
        instance = this;

        infoPanel = GameObject.Find("InfoPanel");
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

        if (Input.GetKeyDown("p"))
        {
            showInfoPanel(!infoPanel.activeInHierarchy);
        }
        if (infoPanel.activeInHierarchy) updateInfoPanel();
    }
    void showInfoPanel(bool show)
    {
        infoPanel.SetActive(show);
    }
    void updateInfoPanel()
    {
        List<string> textsInInfoPanel = new List<string>();

        PlayerController myPlayer = FindObjectOfType<PlayerController>();
        foreach (PlayerController pc in FindObjectsOfType<PlayerController>())
        {
            if (pc.isMine)
            {
                myPlayer = pc;
                break;
            }
        }

        textsInInfoPanel.Add("State: " + currState);
        textsInInfoPanel.Add("SumTotal (Player): " + myPlayer.MySumTotal());
        textsInInfoPanel.Add("SumTotal (Game): " + getSumTotal());
        textsInInfoPanel.Add("LastNum: " + lastNum);
        onInfoPanelChange.Invoke(textsInInfoPanel);
    }
    
    void infoPanelListener(List<string> list)
    {
        Transform textsParent = infoPanel.transform.GetChild(0);
        if(textsParent.childCount != list.Count)
        {
            List<GameObject> oldTexts = new List<GameObject>();
            foreach(Transform child in textsParent)
            {
                oldTexts.Add(child.gameObject);
            }
            for(int i = 0; i < list.Count; i++)
            {
                GameObject newText = Instantiate(oldTexts[0]);
                newText.GetComponent<TMP_Text>().text = list[i];
            }
            foreach(GameObject old in oldTexts)
            {
                Destroy(old);
            }
        }
        else
        {
            for (int i = 0; i < list.Count; i++)
            {
                textsParent.GetChild(i).GetComponent<TMP_Text>().text = list[i];
            }
        }
    }
    public void changeState(State state)
    {
        switch (state)
        {
            case State.selectToken:
                //Inici de l'spawn dels Tokens
                LastNumScreenController.UpdateInfoTexts();
                StartCoroutine(SpawnTokens());
                break;
            case State.inMatch:
                tc.startGame();
                setSumTotal();
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
        foreach (PlayerBot bot in FindObjectsOfType<PlayerBot>())
        {
            bot.StartChoosingToken();
        }
        updateBots();
        
    }
    public int lowestNumOnTokens()
    {
        int lowestNumber = allTokenNums[0]; // Inicializamos la variable con el primer elemento del array

        for (int i = 1; i < allTokenNums.Count; i++) // Comenzamos a buscar a partir del segundo elemento
        {
            if (allTokenNums[i] < lowestNumber) // Si encontramos un número menor, lo actualizamos en la variable lowestNumber
            {
                lowestNumber = allTokenNums[i];
            }
        }
        return lowestNumber;
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

    #endregion
    #region InGame State - Methods
    public void SubmitNum(int submitedNum)
    {
        if(submitedNum < lastNum && tc.turnNum != 1)
        {
            Debug.LogWarning("ALERTA: El nombre que has posat és més petit que el lastNum");
            return;
        }
        TurnController.instance.getActualPlayer().setSelectedNum(submitedNum);
        lastNum = submitedNum;
        TurnController.instance.nextTurn();
        LastNumScreenController.UpdateInfoTexts();
    }
    #endregion
    void checkStateEnded()
    {
        Debug.Log("checkStateEnded");
        switch (currState)
        {
            case State.selectToken:
                if (haveAllPlayersChoosenToken())
                {
                    foreach (TokenController token in allTokens)
                    {
                        if (!token.isSelected) Destroy(token.gameObject);
                    }
                    allTokens.Clear();
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


}

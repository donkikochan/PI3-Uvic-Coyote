using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string playerName;

    public delegate void PlayerSetToken();
    public static event PlayerSetToken OnPlayerAddedToken;

    private TokenController actualToken;
    public int tokenNum;
    int selectedNum;
    int loses;
    public bool activeTurn;
    //TODO -- Borrar aquest "isMine" quan s'implementi el multiplayer
    public bool isMine;
    public List<string> names = new List<string>();
    public List<GameObject> skinPrefabs = new List<GameObject>();
    private TurnController _tc;
    private GameObject _keyboard;
    private GameObject _fullTotem;
    /* Fills del _fullTotem:
     * GetChild(0) -> Base
     * GetChild(1) -> Mid
     * GetChild(2) -> Top
     */
    List<int> numRecord = new List<int>();
    void Start()
    {
        //if (newGame)
        _tc = TurnController.instance;
        _tc.addPlayer(this);
        loses = 0;

        _keyboard = createChildObject(GameManager.instance.keyboardPrefab, new Vector3(0f, 0.5f, 1.25f), true);
        _keyboard.SetActive(false);

        _fullTotem = createChildObject(GameManager.instance.fullTotemPrefab, new Vector3(0f, -1.5f, -1f),false);
    }

    private void Awake()
    {
        if (GetComponent<PlayerBot>() != null)
        {
            
        }
        

        
    }
    public void setMine(bool b)
    {
        isMine = b;
        if(!isMine)
        {
            gameObject.AddComponent<PlayerBot>();
            playerName = names[Random.Range(0, names.Count)];
            Instantiate(skinPrefabs[0], transform);
        }
        else
        {
            playerName = "Mi Player";
            Camera.main.GetComponent<CameraController>().setPlayer(transform);
            Instantiate(skinPrefabs[Random.Range(1,skinPrefabs.Count)], transform);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    //Suma total dels tokens dels altres jugadors sense contar el teu token
    public int MySumTotal()
    {
        int sumTotal = 0;
        foreach(PlayerController player in _tc.getPlayers())
        {
            sumTotal += player.tokenNum;
        }
        
        sumTotal -= tokenNum;
        Debug.Log("PlayerController of " + name + ": has MySumTotal at " + sumTotal);
        return sumTotal;
    }
    public void setToken(TokenController tc)
    {
        //Colocar token seleccionat al cap del jugador
        
        actualToken = tc;
        tc.GetComponent<Rigidbody>().isKinematic = true;
        tc.isSelected = true;
        tokenNum = actualToken.getNum();
        tc.transform.position = transform.GetChild(0).position;
        tc.transform.parent = transform.GetChild(0);
        tc.transform.rotation = transform.GetChild(0).rotation;
        tc.transform.Rotate(new Vector3(0, 90, 0));

        if (OnPlayerAddedToken != null)
        {
            OnPlayerAddedToken();
        }
    }
    public void eraseToken()
    {
        Destroy(actualToken.gameObject);
    }
    public void setActiveTurn(bool b)
    {
        activeTurn = b;
        _keyboard.SetActive(activeTurn);
        if (activeTurn && GetComponent<PlayerBot>()!=null)
        {
            GetComponent<PlayerBot>().StartSelectingNumber();
        }
    }
    public bool hasToken()
    {
        return actualToken != null;
    }
    public int getSelectedNum()
    {
        return selectedNum;
    }
    public void setSelectedNum(int num)
    {
        selectedNum = num;
    }
    public int addLoss()
    {
        _fullTotem.transform.GetChild(loses).gameObject.SetActive(true);
        loses++;
        Debug.Log("Loss added to: ["+playerName+"], actual loses: " + loses);
        return loses;
    }
    GameObject createChildObject(GameObject prefab, Vector3 pos, bool isChild)
    {
        GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
        obj.transform.parent = transform;
        obj.transform.localEulerAngles = Vector3.zero;
        obj.transform.Translate(pos, Space.Self);

        obj.transform.LookAt(transform);
        obj.transform.localRotation = new Quaternion(0,obj.transform.localRotation.y, obj.transform.localRotation.z, obj.transform.localRotation.w);
        if(!isChild) obj.transform.parent = null;
        return obj;
    }

}

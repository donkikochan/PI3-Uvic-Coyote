using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void PlayerSetToken();
    public static event PlayerSetToken OnPlayerAddedToken;

    private TokenController actualToken;
    public int tokenNum;
    int selectedNum;
    int loses;
    public bool activeTurn;
    private TurnController _tc;
    List<int> numRecord = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        //if (newGame)
        
        loses = 0;
    }

    private void Awake()
    {
        _tc = GameObject.FindObjectOfType<TurnController>();
        _tc.addPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Suma total dels tokens dels altres jugadors sense contar el teu token
    public int sumTotal()
    {
        int sumTotal = 0;
        foreach(PlayerController player in _tc.getPlayers())
        {
            sumTotal += player.tokenNum;
        }
        sumTotal -= tokenNum;
        return sumTotal;
    }
    public void setToken(TokenController tc)
    {
        //Colocar token seleccionat al cap del jugador
        
        actualToken = tc;
        tc.GetComponent<Rigidbody>().isKinematic = true;
        tc.isSelected = true;
        tokenNum = actualToken.getNum();
        tc.transform.position = transform.position;
        tc.transform.parent = transform;
        tc.transform.rotation = transform.rotation;
        tc.transform.Rotate(new Vector3(0, 90, 0));

        if (OnPlayerAddedToken != null)
        {
            OnPlayerAddedToken();
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
    public void addLoss()
    {
        loses++;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private TokenController actualToken;
    public int tokenNum;
    int selectedNum;
    int loses;
    public bool activeTurn;
    private TurnController tc;
    List<int> numRecord = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        //if (newGame)
        tokenNum = actualToken.getNum();
        loses = 0;
    }

    private void Awake()
    {
        actualToken = GetComponent<TokenController>();
        tc = GameObject.FindObjectOfType<TurnController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Suma total dels tokens dels altres jugadors sense contar el teu token
    public int sumTotal()
    {
        int sumTotal = 0;
        foreach(PlayerController player in tc.getPlayers())
        {
            sumTotal += player.tokenNum;
        }
        sumTotal -= tokenNum;
        return sumTotal;
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

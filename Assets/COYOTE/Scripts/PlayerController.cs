using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private TokenController actualToken;
    public int num;
    public bool activeTurn;
    private TurnController tc;
    // Start is called before the first frame update
    void Start()
    {
        num = actualToken.getNum();
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
            sumTotal += player.num;
        }
        sumTotal -= num;
        return sumTotal;
    }


}

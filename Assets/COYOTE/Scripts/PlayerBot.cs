using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBot : MonoBehaviour
{
    [Header("SelectToken State")]
    public Vector2 randomTimeBeforeChooseToken = new Vector2(2f,5f);
    [Header("inGame State")]
    public Vector2 randomTimeBeforeChooseNumber = new Vector2(2f, 5f);
    public int maxChoosingNum = 10;
    [Range(0, 100)]
    public int probOfEndRound = 10;
    [Range(0, 25)]
    public int increasedProbOfEndRound = 5;
    List<TokenController> avaiableTokens = new List<TokenController>();

    PlayerController _pc;
    // Start is called before the first frame update
    private void Awake()
    {
        _pc = GetComponent<PlayerController>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    #region State - SelectToken
    public void setAvaiableTokens(List<TokenController> aTokens)
    {
        avaiableTokens = aTokens;
    }
    void clearUnavaiableTokens()
    {
        bool isCleared = false;
        while (!isCleared)
        {

            for (int i = 0; i < avaiableTokens.Count; i++)
            {
                if (avaiableTokens[i].isSelected)
                {
                    avaiableTokens.RemoveAt(i);
                    break;
                }
            }
            isCleared = true;
        }
    }
    public void StartChoosingToken()
    {
        StartCoroutine(ChooseToken(Random.Range(randomTimeBeforeChooseToken.x, randomTimeBeforeChooseToken.y)));
    }
    IEnumerator ChooseToken(float timeBeforeChoose)
    {

        yield return new WaitForSeconds(timeBeforeChoose);
        clearUnavaiableTokens();
        _pc.setToken(avaiableTokens[Random.Range(0, avaiableTokens.Count)]);
    }
    #endregion
    #region State - InMatch
    //TODO -- Gestió del bot per triar un número o cap
    public void StartSelectingNumber()
    {
        StartCoroutine(ChooseNumber());
    }
    IEnumerator ChooseNumber()
    {
        yield return new WaitForSeconds(Random.Range(randomTimeBeforeChooseNumber.x,randomTimeBeforeChooseNumber.y));
        int randomPerCent = Random.Range(0, 101);
        if(probOfEndRound >= randomPerCent && TurnController.instance.GetTurnNum() != 1)
        {
            TurnController.instance.endGame();
        }
        else
        {
            int referenceNum = TurnController.instance.GetTurnNum() == 1 ? _pc.MySumTotal() : GameManager.instance.lastNum;
            int selectedNum = Random.Range(referenceNum + 1, referenceNum + maxChoosingNum);
            GameManager.instance.SubmitNum(selectedNum);
            probOfEndRound += increasedProbOfEndRound;
        }
        
    }
    #endregion
}

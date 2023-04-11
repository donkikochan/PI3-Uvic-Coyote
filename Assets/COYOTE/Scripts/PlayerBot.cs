using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBot : MonoBehaviour
{
    public Vector2 randomTimeBeforeChooseToken = new Vector2(2f,5f);
    List<TokenController> avaiableTokens = new List<TokenController>();

    PlayerController _pc;
    // Start is called before the first frame update
    private void Awake()
    {
        _pc = GetComponent<PlayerController>();
    }
    void Start()
    {
        StartCoroutine(ChooseToken(Random.Range(randomTimeBeforeChooseToken.x,randomTimeBeforeChooseToken.y)));
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
    IEnumerator ChooseToken(float timeBeforeChoose)
    {

        yield return new WaitForSeconds(timeBeforeChoose);
        clearUnavaiableTokens();
        _pc.setToken(avaiableTokens[Random.Range(0, avaiableTokens.Count)]);
    }
    #endregion
    #region State - InMatch
    //TODO -- Gestió del bot per triar un número o cap
    void selectNumber()
    {

    }
    IEnumerator ChooseNumber()
    {
        yield return new WaitForSeconds(5f);
    }
    #endregion
}

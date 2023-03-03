using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TokenController : MonoBehaviour
{
    public GameObject redTokenModelPrefab;
    public GameObject greenTokenModelPrefab;
    public GameObject blueTokenModelPrefab;
    private int num;
    private float tokenScale = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        
        GameObject selectedModel;
        switch (num)
        {
            case < 0:
                selectedModel = Instantiate(redTokenModelPrefab);
                break;
            case > 0:
                selectedModel=Instantiate(greenTokenModelPrefab);
                break;
            default:
                selectedModel = Instantiate(blueTokenModelPrefab);
                break;
        }
        selectedModel.transform.parent = this.transform;
        selectedModel.transform.localPosition = new Vector3(0, 4, -0.185f);
        selectedModel.transform.localEulerAngles = new Vector3(0, 90, 90);

        transform.localScale = new Vector3(tokenScale, tokenScale, tokenScale);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getNum()
    {
        return num;
    }

    public void setNum(int num)
    {
        this.num = num;
        GetComponentInChildren<TMP_Text>().text = ""+getNum();
    }
}

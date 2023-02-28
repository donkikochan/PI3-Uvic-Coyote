using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenController : MonoBehaviour
{
    public GameObject redTokenModelPrefab;
    public GameObject greenTokenModelPrefab;
    public GameObject blueTokenModelPrefab;
    private int num;
    // Start is called before the first frame update
    void Start()
    {
        setNum(Random.Range(-20, 20));

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
    }
}

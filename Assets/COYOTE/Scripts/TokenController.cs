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
    public int num;

    private float tokenScale = 0.05f;
    Color _startColor;
    Renderer _renderer;
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

        _renderer = selectedModel.GetComponent<Renderer>();
        _startColor = _renderer.materials[6].color;

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
    private void OnMouseEnter()
    {
        _renderer.materials[6].color = Color.yellow;
    }
    private void OnMouseExit()
    {
        _renderer.materials[6].color = _startColor;
    }
    private void OnMouseDown()
    {
        //TODO -- Assignar aquest token al jugador

        if (!FindObjectOfType<PlayerController>().hasToken())
        {
            FindObjectOfType<PlayerController>().setToken(this);
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}

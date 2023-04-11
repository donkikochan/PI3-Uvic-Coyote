using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DisableButton : MonoBehaviour
{
    public Button myButton;
    public Button myButton1;
    public Button myButton2;
    public Button myButton3;
    // Start is called before the first frame update
    void Start()
    {
        myButton.onClick.AddListener(ButtonClicked);
        myButton1.onClick.AddListener(ButtonClicked1);
        myButton2.onClick.AddListener(ButtonClicked2);
        myButton3.onClick.AddListener(ButtonClicked3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ButtonClicked()
    {
        myButton.interactable = false;
    }
    void ButtonClicked1()
    {
        myButton1.interactable = false;
    }
    void ButtonClicked2()
    {
        myButton2.interactable = false;
    }
    void ButtonClicked3()
    {
        myButton3.interactable = false;
    }
}

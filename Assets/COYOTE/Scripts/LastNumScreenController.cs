using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LastNumScreenController : MonoBehaviour
{
    public Color backgroundColor;
    public Color textColor;
    static List<TMP_Text> infoTexts = new List<TMP_Text>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(TMP_Text text in GetComponentsInChildren<TMP_Text>())
        {
            infoTexts.Add(text);
            text.color = textColor;
        }
        foreach(Image img in GetComponentsInChildren<Image>())
        {
            img.color = backgroundColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void UpdateInfoTexts()
    {
        foreach(TMP_Text text in infoTexts)
        {
            text.text = TurnController.instance.turnNum <= 1 || GameManager.instance.currState != GameManager.State.inMatch ? "-" : ""+GameManager.instance.lastNum;
        }
    }
}

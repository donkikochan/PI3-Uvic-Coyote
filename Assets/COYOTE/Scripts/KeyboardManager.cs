using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyboardManager : MonoBehaviour
{
    public TMP_Text textBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DeleteLetter()
    {
        if (textBox.text.Length != 0)
        {
            textBox.text = textBox.text.Remove(textBox.text.Length - 1, 1);
        }
    }

    public void AddLetter(string letter)
    {
        textBox.text = textBox.text + letter;
    }

    public void SubmitWord()
    {
        TurnController.instance.getActualPlayer().setSelectedNum(int.Parse(textBox.text));
        TurnController.instance.nextTurn();
        textBox.text = "";
        // Debug.Log("Text submitted successfully!");
    }
}
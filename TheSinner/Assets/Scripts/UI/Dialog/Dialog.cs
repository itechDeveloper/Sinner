using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingspeed;
    public GameObject continueButton;

    public GameObject karakter1;
    public int karakterr1=2;
    public int karakterr2=3;
    public GameObject karakter2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Type());
    }
    void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }

        if (karakterr1 % 2 == 0)
        {
            karakter1.gameObject.SetActive(true);
        }
        else
        {
            karakter1.gameObject.SetActive(false);
        }

        if(karakterr2 % 2 == 0)
        {
            karakter2.gameObject.SetActive(true);
        }
        else
        {
            karakter2.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingspeed);
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        if(index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
            karakterr1 += 1;
            karakterr2 += 1;
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
            karakter1.gameObject.SetActive(false);
            karakter2.gameObject.SetActive(false);
        }
    }
}

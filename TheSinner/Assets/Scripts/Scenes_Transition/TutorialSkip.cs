using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TutorialSkip : MonoBehaviour
{

    public void tutorialskip()
    {
        PlayerPrefs.SetInt(("ActiveScene"), 2);
        SceneManager.LoadScene(2);
    }
}

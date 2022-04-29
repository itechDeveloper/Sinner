using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransaction : MonoBehaviour
{
    public int gecilceksahne;

    public void TransactScene()
    {
        PlayerPrefs.SetInt("ActiveScene", gecilceksahne);
        SceneManager.LoadScene(gecilceksahne);
    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadNextPhase()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log(SceneManager.sceneCountInBuildSettings);
        if (nextScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneTransitions.instance.FadeSceneTransitionByIndex(nextScene);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}

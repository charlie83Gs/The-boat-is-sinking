using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFuntions : MonoBehaviour {
    public GameObject SceneTrans;
    public void Play() {
        SceneTransitioner transitioner = SceneTrans.GetComponent<SceneTransitioner>();
        transitioner.SetSceneName("MainGame");
        transitioner.SetTransitionToPlay("End");
        transitioner.StartCoroutine(transitioner.LoadScene());
    }

    public void Help()
    {
        SceneTransitioner transitioner = SceneTrans.GetComponent<SceneTransitioner>();
        transitioner.SetSceneName("Help");
        transitioner.SetTransitionToPlay("End");
        transitioner.StartCoroutine(transitioner.LoadScene());
    }

    public void Credits()
    {
        SceneTransitioner transitioner = SceneTrans.GetComponent<SceneTransitioner>();
        transitioner.SetSceneName("Credits");
        transitioner.SetTransitionToPlay("End");
        transitioner.StartCoroutine(transitioner.LoadScene());
    }

    public void MainMenu()
    {
        SceneTransitioner transitioner = SceneTrans.GetComponent<SceneTransitioner>();
        transitioner.SetSceneName("MainMenu");
        transitioner.SetTransitionToPlay("End");
        transitioner.StartCoroutine(transitioner.LoadScene());
    }

    public void Options()
    {
        SceneTransitioner transitioner = SceneTrans.GetComponent<SceneTransitioner>();
        transitioner.SetSceneName("Options");
        transitioner.SetTransitionToPlay("End");
        transitioner.StartCoroutine(transitioner.LoadScene());
    }

    public void Controls()
    {
        SceneTransitioner transitioner = SceneTrans.GetComponent<SceneTransitioner>();
        transitioner.SetSceneName("Controls");
        transitioner.SetTransitionToPlay("End");
        transitioner.StartCoroutine(transitioner.LoadScene());
    }

    public void Exit()
    {
        Application.Quit();
    }


}

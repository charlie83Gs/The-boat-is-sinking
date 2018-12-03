using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour {
    public GameObject SceneTrans;
    public void GoToMainMenu() {
        
        SceneTransitioner transitioner = SceneTrans.GetComponent<SceneTransitioner>();
        transitioner.SetSceneName("MainMenu");
        transitioner.SetTransitionToPlay("End");
        transitioner.StartCoroutine(transitioner.LoadScene());
    }
}

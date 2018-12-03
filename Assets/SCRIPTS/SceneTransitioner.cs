using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour {
    public GameObject SceneTrans;
    public Animator transitionAnimation;
    private string SceneName;
    private string TransitionToPlay;

    //StartCoroutine(LoadScene());
    public IEnumerator LoadScene()
    {
        transitionAnimation.SetTrigger(name: TransitionToPlay);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneName);
    }

    public void SetSceneName(string pName)
    {
        SceneName = pName;
    }

    public void SetTransitionToPlay(string pTransition)
    {
        TransitionToPlay = pTransition;
    }
}

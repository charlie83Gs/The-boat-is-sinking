using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesAndControls : MonoBehaviour
{
    public string lvlName;
    public GameObject[] UnshownOjbs;
    public GameObject[] ShowObjs;

    public void NextScene()
    {
        SceneManager.LoadScene(lvlName);
    }

    public void ShowmeWhatYouGot()
    {
        foreach (GameObject USO in UnshownOjbs)
        {
            USO.SetActive(false);
        }
        foreach (GameObject SO in ShowObjs)
        {
            SO.SetActive(true);
        }
    }
}

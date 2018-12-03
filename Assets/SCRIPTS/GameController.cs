using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController instance = null;
    private string WhoWon;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void whoLost(PlayerID player)
    {
        if(player.ToString() == "One")
        {
            Debug.Log("player " + player.ToString() + " Died.");
            WhoWon = "Red";
        }
        else
        {
            Debug.Log("player " + player.ToString() + " Died.");
            WhoWon = "Blue";
        }
        SceneManager.LoadScene("Victory");
    }

    public string GetWhoWon()
    {
        return WhoWon;
    }
}

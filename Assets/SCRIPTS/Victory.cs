using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Victory : MonoBehaviour {
    public GameObject obj;
    public GameObject Red;
    public GameObject Blue;
    private TextMeshProUGUI m_text;
	// Use this for initialization
	void Awake () {
        m_text = obj.GetComponent<TextMeshProUGUI>();
        string text = GameController.instance.GetWhoWon() + " Won!";
        string winner = GameController.instance.GetWhoWon();
        if (winner == "Blue")
        {
            Blue.SetActive(true);
        }
        else
        {
            Red.SetActive(true);

        }
        Debug.Log(m_text == null);
        m_text.text = text;
	}
}

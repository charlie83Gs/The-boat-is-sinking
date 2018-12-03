using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FillHealtBart : MonoBehaviour {

    public PlayerController player;
    private Image healthImage;
    private float targetFill= 1;
    public float changeSpeed = 0.1f;
    // Use this for initialization
    void Start () {
        healthImage = GetComponent<Image>();

    }
	
	// Update is called once per frame
	void Update () {
        healthImage.fillAmount = Mathf.Lerp(healthImage.fillAmount,player.getHealtPercentaje(),changeSpeed);
	}
}

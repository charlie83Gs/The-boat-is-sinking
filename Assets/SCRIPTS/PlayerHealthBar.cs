using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour {

    public PlayerController player;
    private Vector3 originalScale;
    public Vector3 ScaleDirection;
    public Vector3 ScaleTranform;
    private Vector3 targetScale;
    public float changeSpeed = 0.1f;
	// Use this for initialization
	void Start () {
        originalScale = transform.localScale;
        ScaleTranform = new Vector3(originalScale.x * ScaleDirection.x, originalScale.y * ScaleDirection.y, originalScale.z * ScaleDirection.z);

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 correction = new Vector3(ScaleTranform.x * ScaleDirection.x, ScaleTranform.y * ScaleDirection.y, ScaleTranform.z*ScaleDirection.z);
        targetScale = originalScale + player.getHealtPercentaje()* ScaleTranform - correction;
        Vector3 newScale = originalScale;
        newScale.x = Mathf.Lerp(transform.localScale.x,targetScale.x,changeSpeed*Time.deltaTime);
        //newScale.y = Mathf.Lerp(transform.localScale.y,targetScale.y,changeSpeed*Time.deltaTime);
        //newScale.z = Mathf.Lerp(transform.localScale.z,targetScale.z,changeSpeed*Time.deltaTime);

        transform.localScale = newScale;
        //Debug.Log(player.getHealtPercentaje());
    }
}

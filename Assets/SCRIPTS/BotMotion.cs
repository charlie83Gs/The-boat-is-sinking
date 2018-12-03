using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMotion : MonoBehaviour {

    public Vector2 motionRange;
    public Vector2 HorizontalmotionRange;
    public int seed = 3042;
    private Random rng;
    public Vector2 state = new Vector2(0,0);
    public Vector2 target = new Vector2(0, 0);
    public float change = 1;
    public Vector2 changeRange;
    private Vector2 Changes;
    public float elapsedTime = 0;
    public Vector2 elapsedTime2 = new Vector2(0,0);
    Quaternion originalRotation;
	// Use this for initialization
	void Start () {
        Changes = new Vector2(change, change);
        originalRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        elapsedTime += Time.deltaTime;
        elapsedTime2.x += Time.deltaTime;
        elapsedTime2.y += Time.deltaTime;
        if (elapsedTime2.x > Changes.x) {
            elapsedTime2.x = 0;
            state.x = target.x;
            target = new Vector2(Random.Range(motionRange.x,motionRange.y),target.y);
            Changes.x = Random.Range(changeRange.x,changeRange.y);
        }
        if (elapsedTime2.y > Changes.y)
        {
            elapsedTime2.y = 0;
            state.y = target.y;
            target = new Vector2( target.x, Random.Range(HorizontalmotionRange.x, HorizontalmotionRange.y));
            Changes.y = Random.Range(changeRange.x, changeRange.y);
        }
        

        transform.rotation = originalRotation  * Quaternion.Euler(Mathf.Lerp(state.x,target.x, elapsedTime2.x / Changes.x), 0,Mathf.Lerp(state.y, target.y, elapsedTime2.y / Changes.y));

    }
}

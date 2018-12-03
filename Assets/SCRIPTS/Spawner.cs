using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Vector2 spawmTime =  new Vector2(1,4);
    private float nextSpawn = 0;
    private float elapsedTime = 0;
    public GameObject target;
    public Vector2 xRange = new Vector2(-10, 10);
    public float Ycoord= 15;


	// Use this for initialization
	void Start () {
        nextSpawn = Random.Range(spawmTime.x, spawmTime.y);
	}
	
	// Update is called once per frame
	void Update () {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > nextSpawn) {
            elapsedTime = 0;
            nextSpawn = Random.Range(spawmTime.x, spawmTime.y);
            Instantiate(target,transform.position + new Vector3(Random.Range(xRange.x,xRange.y),Ycoord,0) , Quaternion.Euler(270,0,0));
        }
	}
}

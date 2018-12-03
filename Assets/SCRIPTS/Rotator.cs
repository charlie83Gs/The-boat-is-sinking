using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
    public Vector3 speed;
    public bool localRotation = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(localRotation)
        transform.Rotate(speed * Time.deltaTime, Space.Self);
        else
        transform.Rotate(speed * Time.deltaTime, Space.World);


    }
}

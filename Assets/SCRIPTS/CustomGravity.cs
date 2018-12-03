using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class CustomGravity : MonoBehaviour {
    public Vector3 Force = new Vector3(0,-35,0);
    public bool affectedByWater = true;
    private Rigidbody body;
	// Use this for initialization
	void Start () {
        body = this.gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        body.AddForce(Force);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Water" && affectedByWater == true) {
            Force = Force / 6;
            body.velocity = body.velocity / 2;
        }
    }

}

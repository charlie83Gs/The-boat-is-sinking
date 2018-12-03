using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;

[RequireComponent(typeof(CapsuleCollider))]
public class HandsArea : MonoBehaviour {
    private bool HandsBussy = false;
    public PlayerController player;
    [SerializeField]
    private PlayerID m_playerID;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerStay(Collider other)
    {
        if (!player.HandsBussy && other.tag == "Object" && InputManager.GetButton(name: "Grab", playerID: m_playerID))
        {
            player.SetGrabbed(other.gameObject);
            //player.HandsBussy = true;
            //Debug.Log("CanGrabObject");
        }
    }
}

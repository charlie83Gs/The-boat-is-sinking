using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour {

    public static Boat instance = null;

    //private int amountOfItems;
    private float sinkingSpeed;
    public float BoatWeight = 1;
    public float WeightFactor = 1;
    public float defaultSinkSpeed = 0.5f;
    private float sinking = 0;
    private Vector3 originalPos;

    private void Awake()
    {

        if(instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        ///BoatWeight = 0;
        //amountOfItems = 1;
        originalPos = transform.position;
    }
    
	 // Update is called once per frame
	 void Update ()
    {
        //sinking -= (float)((defaultSinkSpeed + BoatWeight ) * 0.001);
        //Debug.Log(BoatWeight);
        transform.position = new Vector3(originalPos.x, originalPos.y - sinkingSpeed - WeightFactor*BoatWeight, originalPos.z);
    }
    //cambiar por undimiento extra
    private void FixedUpdate()
    {

    }

    public void AddObjectWeight(float pWeight)
    {
        //Debug.Log("Add weight");
        BoatWeight += pWeight;
        //amountOfItems++;
    }

    public void RemoveObjectWeight(float pWeight)
    {
        BoatWeight -= pWeight;
        //amountOfItems--;
        /*if(transform.position.y <= 4.7)
        {
            transform.position = new Vector3(transform.position.x, (float)(transform.position.y + 0.5), transform.position.z);
        }*/
    }

}

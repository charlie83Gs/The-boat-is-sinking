using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : MonoBehaviour {
    public float minHeight = 10f;
    public AudioClip sound;
    private AudioSource source;
    private GameObject cargo;
    public GameObject[] objects;
    private Vector3 cargoOffset;
    private bool setFree = false;
	// Use this for initialization
	void Start () {
        cargo = Instantiate(objects[(int)Random.Range(0, objects.Length)]);
        cargo.GetComponent<Rigidbody>().freezeRotation = true;
        source = GetComponent<AudioSource>();
        cargoOffset = cargo.GetComponent<Item>().cargoOffset;

    }
	
	// Update is called once per frame
	void Update () {
        
        if (!setFree && transform.position.y < minHeight) {
            cargo.GetComponent<Rigidbody>().velocity = Vector3.zero;
            cargo.GetComponent<Rigidbody>().freezeRotation = false;
            //cargo.GetComponent<Item>().ResetConstraints();
            Destroy(this.gameObject,1.1f);
            setFree = true;
            gameObject.GetComponent<Animator>().SetTrigger("Destroy");
            gameObject.GetComponent<CustomGravity>().enabled = false;
            source.pitch = Random.Range(0.9f, 1.1f);
            source.PlayOneShot(sound, Random.Range(0.1f, 0.3f));
        }

        if (!setFree)
        {
            transform.position = transform.position + new Vector3(0,-6 * Time.deltaTime,0);
            cargo.transform.position = transform.position + cargoOffset;
        }
	}
}

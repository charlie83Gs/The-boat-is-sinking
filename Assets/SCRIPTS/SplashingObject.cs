using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SplashingObject : MonoBehaviour {

    public GameObject Splash;
    public AudioClip sound;
    public Vector3 offset;
    public bool DestroyAfterWater = false;
    public float DestructionDelay = 1;
    private AudioSource source;
    

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water") {
            Instantiate(Splash,transform.position + offset,Quaternion.identity);
            source.pitch = Random.Range(0.9f, 1.1f);
            source.PlayOneShot(sound,Random.Range(0.05f,0.1f));
            Destroy(this.gameObject,DestructionDelay);
        }
    }
}

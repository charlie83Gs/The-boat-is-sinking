using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float weight = 0.1f;
    public bool isDamaging;
    public AudioClip hitSound;
    public RigidbodyConstraints originalConstrains;
    private AudioSource source;
    public Vector3 cargoOffset = new Vector3(0, 0, 0);
    public Vector3 grabOffset = new Vector3(0, 0, 0);
    public float Weight
    {
        get
        {
            return weight;
        }

        set
        {
            weight = value;
        }
    }

    void Start()
    {
        source = GetComponent<AudioSource>();
        originalConstrains = GetComponent<Rigidbody>().constraints;
    }

    public void Create()
    {
        Boat.instance.AddObjectWeight(weight);
    }

    public void GetDestroyed()
    {
        Boat.instance.RemoveObjectWeight(weight);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //isDamaging = false;
        //if(coll)
        StartCoroutine(isNotDamaging(0.05f));
        source.pitch = Random.Range(0.9f, 1.1f);
        source.PlayOneShot(hitSound, Random.Range(0.001f, 0.01f));

    }

    public void SetDamaging(bool newValue) {
        isDamaging = newValue;
    }

    public bool GetDamaging() {
        return isDamaging;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boat")
        {
            Debug.Log("Adding wight");
            Create();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Boat")
        {

            GetDestroyed();
        }
    }

    IEnumerator isNotDamaging(float time)
    {
        yield return new WaitForSeconds(time);

        isDamaging = false;
        // Code to execute after the delay
    }

    public void ResetConstraints()
    {
        GetComponent<Rigidbody>().constraints = originalConstrains;
    }

}

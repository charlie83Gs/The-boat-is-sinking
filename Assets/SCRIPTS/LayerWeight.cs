using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class LayerWeight : MonoBehaviour {
    private Animator anim;
    [Range(0.0f,1.0f)]
    public float weight;

    public int num;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        num = anim.GetLayerIndex("Manos");
        anim.SetLayerWeight(num, weight);
	}
}

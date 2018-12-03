using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour {

    public int PlayerId;
    public PlayerID m_playerID;
    public float gravity;
    //public GameObject SplashEffect;
    public AudioClip JumpSound;
    public AudioClip throwSound;
    public AudioClip GrabSound;
    public AudioClip FallSound;
    public GameObject jumpEffect;
    private GameObject grabbed = null;
    private Rigidbody rb;
    public float speed;
    public float jumpForce;
    private float MoveInput;

    private int isOnGround;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public int maxJumps = 2;
    private int jumpCount = 0;
    public float KnockBack;
    private Vector3 KnockBackInfo;

    private float JumpTimeCounter;
    public float JumpTime;
    private bool isJumping;
    public bool HandsBussy = false;
    private Vector3 GrabOffset;
    public float ThrowForce = 10;
    public float ThrowCooldown = 0.4f;
    public float ThrowElapsed = 0.0f;
    public float maxHealt = 8;
    public float healt;
    public float weightDamage = 10;

    Animator anim;
    public GameObject Char;
    int noOfClicks; //Determines Which Animation Will Play
    bool canClick; //Locks ability to click during animation event
    [Range(0.0f, 1.0f)]
    public float weight;

    public int num;

    private AudioSource source;
    public bool isDamaging = false;

    void Start () 
    {
        anim = Char.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
        healt = maxHealt;
        KnockBackInfo = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        num = anim.GetLayerIndex("Manos");
        anim.SetLayerWeight(num, weight);
        ThrowElapsed += Time.deltaTime;
        isOnGround = 0;
        Collider[] hitColliders = Physics.OverlapSphere(feetPos.position, checkRadius, whatIsGround);
        //Check if the collider overlapin is not a trigger
        for (int colliderIndex = 0; colliderIndex < hitColliders.Length; colliderIndex++) {
            if (!hitColliders[colliderIndex].isTrigger) isOnGround++;
        }


        if (isOnGround > 0 && jumpCount > 0)
        {
            Instantiate(jumpEffect, transform.position - new Vector3(0, 1, 0), Quaternion.identity);
            source.pitch = Random.Range(1.7f, 2.5f);
            source.PlayOneShot(FallSound, Random.Range(0.3f, 0.5f));
        }
        if (isOnGround > 0) jumpCount = 0;
        if (jumpCount < maxJumps  && InputManager.GetButtonDown(name: "Jump", playerID: m_playerID))
        {
            isJumping = true;
            JumpTimeCounter = JumpTime;
            jumpCount++;
            rb.AddForce(transform.up * jumpForce*Time.deltaTime);
            //jump particle effect
            Instantiate(jumpEffect,transform.position- new Vector3(0,1,0), Quaternion.identity);
            //jump sound effect
            source.pitch = Random.Range(0.6f, 0.9f);
            source.PlayOneShot(JumpSound, Random.Range(0.8f, 1));
        }
        


        if (InputManager.GetButton(name: "Jump", playerID: m_playerID) && isJumping == true)
        {
            if(JumpTimeCounter > 0 )
            {
                //rb.AddForce(transform.up * jumpForce * Time.deltaTime);
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                JumpTimeCounter -= Time.deltaTime;
                anim.SetTrigger("Jump");
            } 
            else
            {
                isJumping = false;
            }
            
        }
        if(InputManager.GetButtonUp(name: "Jump", playerID: m_playerID))
        {
            isJumping = false;
           
        }
        
        if (MoveInput == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        } else if (MoveInput == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (HandsBussy==true && ThrowElapsed > ThrowCooldown && InputManager.GetButtonDown(name: "Grab", playerID: m_playerID))
        {
            Throw();
            
            //player.HandsBussy = true;
            //Debug.Log("CanGrabObject");
        }

    }
    private void FixedUpdate()
    {
        if(healt < 1)
        {
            imDead();
        }
        if (weight == 0 && InputManager.GetButtonDown(name: "Attack", playerID: m_playerID )) {
            Char.GetComponent<AnimationEvent>().ComboStarter();
            //isDamaging = true;
        }
        
            MoveInput = InputManager.GetAxisRaw(name: "Horizontal", playerID: m_playerID);
            
            anim.SetFloat("Horizontal", MoveInput);

       
        rb.velocity = new Vector3(x: speed * MoveInput, y: rb.velocity.y, z: rb.velocity.z) + KnockBackInfo;
        rb.AddForce(Vector3.down * gravity);
        KnockBackInfo = Vector3.zero;
        if (HandsBussy) {
            weight = 1;
            anim.SetBool("HoldingObj", true);
            grabbed.transform.position = transform.position + GrabOffset;
            grabbed.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //Debug.Log(transform.position + GrabOffset);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.gameObject.tag == "Water") {
            jumpCount = maxJumps + 1;
            gravity = gravity /6;
            rb.velocity = rb.velocity / 2;
            healt = 0;
        }

        
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerPunch")
        {
            PlayerController otherPlayer = other.gameObject.GetComponentInParent<PlayerController>();
            Transform otherPlayerPos = otherPlayer.transform;
            if (otherPlayer.getIsDamaging()) {
                healt--;
                otherPlayer.setIsDamaging(false);
                transform.position += new Vector3(0, 0.5f, 0);
                if (otherPlayerPos.position.x < transform.position.x)
                {
                    KnockBackInfo = new Vector3(KnockBack, 0, 0);
                }
                else
                {
                    KnockBackInfo = new Vector3(-KnockBack, 0, 0);
                }

            }
            
        }
    }

    public void SetGrabbed(GameObject newGrab)
    {
        grabbed = newGrab;
        GrabOffset = newGrab.GetComponent<Item>().grabOffset;
        newGrab.GetComponent<Collider>().enabled = false;
        HandsBussy = true;
        ThrowElapsed = 0;
        source.pitch = Random.Range(0.7f, 1.1f);
        source.PlayOneShot(GrabSound, Random.Range(0.4f, 0.6f));
    }

    public void Throw() {

        //throw sound effect
        grabbed.transform.position += Vector3.up * 0.7f;
        source.pitch = Random.Range(0.6f, 0.95f);
        source.PlayOneShot(throwSound, Random.Range(0.8f, 1));
        anim.SetBool("ThrowObj", true);
        float objectWeight = grabbed.GetComponent<Item>().weight*10;
        grabbed.GetComponent<Collider>().enabled = true;
        //grabbed.GetComponent<Rigidbody>().velocity = new Vector3(ThrowForce,0,0);
        grabbed.GetComponent<Rigidbody>().velocity =  new Vector3(MoveInput * ThrowForce*0.4f/objectWeight + 0.6f*ThrowForce * transform.localScale.x / objectWeight, ThrowForce*0.5f, 0);
        grabbed.GetComponent<Item>().SetDamaging(true);
        
        HandsBussy = false;
        grabbed = null;
    }

    public float getHeal() {
        return healt;
    }

    public float getHealtPercentaje() {
        return healt / maxHealt;
    }

    
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Object") {
            Item item = collision.gameObject.GetComponent<Item>();
            if (item.isDamaging)
            {
                healt -= item.weight*weightDamage;
                item.SetDamaging(false);
            }
            
        }
    }

    public bool getIsDamaging() {
        return isDamaging;
    }
    public void setIsDamaging(bool value)
    {
        isDamaging = value;
        if(value)
        {
            StartCoroutine(cancelDamaging(0.5f));
        }
    }

    public void imDead()
    {
        GameController.instance.whoLost(m_playerID);
    }

    IEnumerator cancelDamaging(float pMiliSeconds)
    {
        yield return new WaitForSeconds(pMiliSeconds);
        isDamaging = false;
    }
}

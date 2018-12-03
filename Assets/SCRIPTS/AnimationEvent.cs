using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationEvent : MonoBehaviour {
    Animator anim;
    int noOfClicks; //Determines Which Animation Will Play
    bool canClick; //Locks ability to click during animation event
    public GameObject PlayerCtrl;
    public GameObject Char;
    private PlayerController controller;



    void Start()
    {
        anim = Char.GetComponent<Animator>();
        controller = PlayerCtrl.GetComponent<PlayerController>();
    }

    
    public void EndAnim()
    {
        
        anim.SetInteger("animation", 4);
       
        anim.SetBool("Jump", false);
        anim.SetBool("ThrowObj", false);
        anim.SetBool("HoldingObj", false);

        //canClick = true;

        noOfClicks = 0;

    }
    public void EndAnimWeight()
    {
        PlayerCtrl.GetComponent<PlayerController>().weight=0;
        anim.SetInteger("animation", 4);
        
        anim.SetBool("Jump", false);
        anim.SetBool("ThrowObj", false);
        anim.SetBool("HoldingObj", false);



        noOfClicks = 0;

    }

    public void ComboStarter()
    {

        noOfClicks++;
        

        if (noOfClicks == 1)
        {
            anim.SetInteger("animation", 1);
            
        }
    }

    public void ComboCheck()
    {

        

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_01") && noOfClicks == 1)
        {//If the first animation is still playing and only 1 click has happened, return to idle
            anim.SetInteger("animation", 4);
            controller.setIsDamaging(true);
            noOfClicks = 0;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_01") && noOfClicks >= 2)
        {//If the first animation is still playing and at least 2 clicks have happened, continue the combo          
            anim.SetInteger("animation", 2);
            controller.setIsDamaging(true);
            noOfClicks = 0;

        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_02") && noOfClicks == 2)
        {  //If the second animation is still playing and only 2 clicks have happened, return to idle         
            anim.SetInteger("animation", 4);
            controller.setIsDamaging(true);
            noOfClicks = 0;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_02") && noOfClicks >= 3)
        {  //If the second animation is still playing and at least 3 clicks have happened, continue the combo         
            anim.SetInteger("animation", 3);
            controller.setIsDamaging(true);
            noOfClicks = 0;

        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_03"))
        { //Since this is the third and last animation, return to idle          
            anim.SetInteger("animation", 4);
            controller.setIsDamaging(true);
            noOfClicks = 0;
        }

    }
}

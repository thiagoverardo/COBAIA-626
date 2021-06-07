using UnityEngine;
using System.Collections;

public class ClockUse : MonoBehaviour
{
    public PlayerController playerCont;

    public bool timeFreeze = false;

    public HairdryerUse hairdryer;

    // Update is called once per frame

    void OnEnable()
    {
        hairdryer.isReloading = true;
        hairdryer.animator.SetBool("Shot", false);
        hairdryer.animator.SetBool("Reloading", true);
        hairdryer.animator.SetBool("Reloading", false);
        hairdryer.isReloading = false;
    }
    void Update()
    {
        if (hairdryer.shot)
        {
            if (hairdryer.charCont.isGrounded)
            {
                playerCont.playerVelocity = new Vector3(0, 0, 0);
                hairdryer.shot = false;
            }
        }

        if (playerCont.goItem != null)
        {
            if (playerCont.goItem.name == "Clock")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PowerOn();
                }
                if(Input.GetKeyUp(KeyCode.E))
                {
                    PowerOff();
                }
            }
        }
    }

    void PowerOn()
    {
        timeFreeze = true;
    }

    void PowerOff()
    {
        timeFreeze = false;
    }

}

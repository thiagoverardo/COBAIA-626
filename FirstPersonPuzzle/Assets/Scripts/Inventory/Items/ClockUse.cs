using UnityEngine;
using System.Collections;

public class ClockUse : MonoBehaviour
{
    public PlayerController playerCont;

    public bool timeFreeze = false;

    // Update is called once per frame
    void Update()
    {
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

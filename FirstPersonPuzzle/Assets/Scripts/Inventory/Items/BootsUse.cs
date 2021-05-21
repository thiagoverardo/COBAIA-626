using UnityEngine;
using System.Collections;

public class BootsUse : MonoBehaviour
{
    public PlayerController playerCont;

    // Update is called once per frame
    void Update()
    {
        if (playerCont.goItem != null)
        {
            if (playerCont.goItem.name == "flaregun")
            {
                if (Input.GetButtonDown("Fire1") && !GetComponent<Animation>().isPlaying)
                {
                    PowerOn();
                }
            }
        }
    }

    void PowerOn()
    {
        
    }

}

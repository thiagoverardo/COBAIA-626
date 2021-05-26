using UnityEngine;
using System.Collections;

public class BazookaUse : MonoBehaviour
{
    public PlayerController playerCont;
    public CharacterController charCont;
    private float forceSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        if (playerCont.goItem != null)
        {
            if (playerCont.goItem.name == "Bazooka")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Shoot(); 
                }
            }
        }
    }

    void Shoot()
    {
        charCont.Move(transform.forward * forceSpeed);
    }

}

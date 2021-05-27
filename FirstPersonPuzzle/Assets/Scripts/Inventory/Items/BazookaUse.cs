using UnityEngine;
using System.Collections;

public class BazookaUse : MonoBehaviour
{
    public PlayerController playerCont;
    public CharacterController charCont;
    public Camera cam;
    private Vector3 targetDirection;
    private float distance = 100f;
    private float mass;
    public bool shot = false;

    // Update is called once per frame
    void Update()
    {
        if(shot)
        {
            if (charCont.isGrounded)
            {
                playerCont.playerVelocity = new Vector3(0,0, 0);
                shot = false;
            }
        }
        

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

    void LateUpdate()

    {
        RaycastHit hit = cam.GetComponent<ObjectInteraction>().hitInfo;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
        {
            targetDirection = cam.GetComponent<ObjectInteraction>().ray.direction;
        }
    }

    void Shoot()
    {
        playerCont.playerVelocity = -targetDirection * 15;
        shot = true;
    }

}

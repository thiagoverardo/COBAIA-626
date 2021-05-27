using UnityEngine;
using System.Collections;

public class BazookaUse : MonoBehaviour
{
    public PlayerController playerCont;
    public CharacterController charCont;
    public Camera cam;
    private float forceSpeed = 10f;
    private Vector3 targetLocation;
    private Vector3 targetDirection;
    private float distance = 100f;
    private float mass;

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

    void LateUpdate()

    {
        RaycastHit hit = cam.GetComponent<ObjectInteraction>().hitInfo;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
        {
            targetLocation = hit.point;
            targetDirection = cam.GetComponent<ObjectInteraction>().ray.direction;
            targetLocation += new Vector3(0, transform.localScale.y / 2, 0);
        }
    }

    void Shoot()
    {
        // charCont.transform.position = targetLocation;
        Debug.Log(targetDirection);
        playerCont.playerVelocity = -targetDirection * 100;
    }

    void AddImpact(Vector3 dir, float force)
    {
        Vector3 impact = Vector3.zero;
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
        impact += dir.normalized * force / mass;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public ClockUse clock;
    public ObjectInteraction objInt;
    public Vector3 cubeInitPos;
    public PlayerController playerCon;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cubeInitPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerCon.restarting)
        {
            rb.position = cubeInitPos;
            playerCon.restarting = false;
        }
        if (objInt.curBody != null)
        {
            if(clock.timeFreeze && objInt.curBody.name != gameObject.name)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }
            else
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }
        }
        else
        {
            if(clock.timeFreeze)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }
            else
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }
        }
        
    }
}

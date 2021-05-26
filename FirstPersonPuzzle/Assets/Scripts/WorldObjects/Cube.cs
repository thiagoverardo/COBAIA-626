using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public ClockUse clock;
    public ObjectInteraction objInt;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
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

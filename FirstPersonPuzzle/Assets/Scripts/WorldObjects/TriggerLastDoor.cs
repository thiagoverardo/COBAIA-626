using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLastDoor : MonoBehaviour
{
    [SerializeField]
    public GameObject iteractObject;
    public ClockUse clock;
    public TriggerLastDoor otherPressureplate;
    bool isUsed = false;
    bool wasFrozen = false;
    private float i;

    void Start()
    {
        Vector3 intObjFirstPos = iteractObject.transform.position;
    }

    void Update()
    {
        if(iteractObject.transform.position.y < 19.85f)
        {
            iteractObject.transform.position = new Vector3(iteractObject.transform.position.x, 19.85f, iteractObject.transform.position.z);
            i = 0;
        }
        if(iteractObject.transform.position.y > 44.85f)
        {
            iteractObject.transform.position = new Vector3(iteractObject.transform.position.x, 44.85f, iteractObject.transform.position.z);
            i = 25f;
        }

        if(wasFrozen && !clock.timeFreeze)
        {
            isUsed = false;
            wasFrozen = false;
        }

        if(otherPressureplate)
        {
            if(isUsed && otherPressureplate.isUsed && i <= 25f && !clock.timeFreeze)
            {
                iteractObject.transform.position += new Vector3(0, 0.1f, 0);
                i += 0.1f;
            }
            if(!isUsed && i >= 0 && !clock.timeFreeze)
            {
                iteractObject.transform.position += new Vector3(0, -0.1f, 0);
                i -= 0.1f;
            }
        }
        else
        {
            if(isUsed && i <= 25f && !clock.timeFreeze)
            {
                iteractObject.transform.position += new Vector3(0, 0.1f, 0);
                i += 0.1f;
            }
            if(!isUsed && i >= 0 && !clock.timeFreeze)
            {
                iteractObject.transform.position += new Vector3(0, -0.1f, 0);
                i -= 0.1f;
            }
        }

    }
    void OnTriggerEnter(Collider col)
    {
        if(!isUsed && !clock.timeFreeze)
        {
            isUsed = true;
            i = 0;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(isUsed && !clock.timeFreeze)
        {
            isUsed = false;
        }

        if(isUsed && clock.timeFreeze)
        {
            wasFrozen = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    public GameObject iteractObject;
    public ClockUse clock;
    bool isUsed = false;
    bool wasFrozen = false;

    void Start()
    {
        Vector3 intObjFirstPos = iteractObject.transform.position;
    }

    void Update()
    {
        if(wasFrozen && !clock.timeFreeze)
        {
            isUsed = false;
            wasFrozen = false;
            iteractObject.transform.position += new Vector3(0, -4, 0);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if(!isUsed && !clock.timeFreeze)
        {
            isUsed = true;
            iteractObject.transform.position += new Vector3(0, 4, 0);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(isUsed && !clock.timeFreeze)
        {
            iteractObject.transform.position += new Vector3(0, -4, 0);
            isUsed = false;
        }

        if(isUsed && clock.timeFreeze)
        {
            wasFrozen = true;
        }
    }
}

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
    private float i;

    void Start()
    {
        Vector3 intObjFirstPos = iteractObject.transform.position;
    }

    void Update()
    {
        if(iteractObject.transform.position.y < 2.5f)
        {
            iteractObject.transform.position = new Vector3(iteractObject.transform.position.x, 2.5f, iteractObject.transform.position.z);
            i = 0;
        }
        if(iteractObject.transform.position.y > 6.5f)
        {
            iteractObject.transform.position = new Vector3(iteractObject.transform.position.x, 6.5f, iteractObject.transform.position.z);
            i = 4;
        }
        if(isUsed && i <= 4 && !clock.timeFreeze){
            iteractObject.transform.position += new Vector3(0, 0.1f, 0);
            i += 0.1f;
        }
        if(!isUsed && i >= 0 && !clock.timeFreeze)
        {
            iteractObject.transform.position += new Vector3(0, -0.1f, 0);
            i -= 0.1f;
        }
        if(wasFrozen && !clock.timeFreeze)
        {
            isUsed = false;
            wasFrozen = false;
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

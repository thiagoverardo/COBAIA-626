using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    public GameObject iteractObject;
    bool isUsed = false;
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("AAAAAA");
        if(!isUsed)
        {
            isUsed = true;
            iteractObject.transform.position += new Vector3(0, 4, 0);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(isUsed)
        {
            isUsed = false;
            iteractObject.transform.position += new Vector3(0, -4, 0);
        }
    }
}

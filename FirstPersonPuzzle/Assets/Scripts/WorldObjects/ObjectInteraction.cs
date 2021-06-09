using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://github.com/samhogan/PhysicsPickup-Unity/blob/master/PhysicsPickup.cs
public class ObjectInteraction : MonoBehaviour
{
    //distance from the camera the item is carried
    public float dist = 2f;

    //the object being held
    private GameObject curObject;
    public Rigidbody curBody;
    public RaycastHit hitInfo;
    public Ray ray;

    //the rotation of the curObject at pickup relative to the camera
    // private Quaternion relRot;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //on mouse click, either pickup or drop an item
        if (Input.GetMouseButtonDown(0))
        {
            if (curObject == null)
            {
                PickupItem();
            }
            else
            {
                DropItem();
            }
        }
    }

    void LateUpdate()
    {
        ray = new Ray(transform.position, transform.forward);
        if (curObject != null)
        {
            //keep the object in front of the camera
            ReposObject();
            curBody.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    //calculates the new rotation and position of the curObject
    void ReposObject()
    {
        //calculate the target position and rotation of the curbody
        Vector3 targetPos = transform.position + transform.forward * dist;

        curBody.velocity = (targetPos - curBody.position) * 10;
    }

    //attempts to pick up an item straigth ahead
    void PickupItem()
    {
        //raycast to find an item
        Physics.Raycast(transform.position, transform.forward, out hitInfo, 5f);

        if (hitInfo.rigidbody == null)
            return;

        curBody = hitInfo.rigidbody;
        curBody.useGravity = false;
        curObject = hitInfo.rigidbody.gameObject;

        curObject.transform.parent = transform;
        curObject.transform.parent = null;

        curObject.tag = "PickUp";
    }

    //drops the current item
    void DropItem()
    {
        curObject.tag = "Cube";
        curBody.useGravity = true;
        curBody = null;
        curObject = null;
    }
}
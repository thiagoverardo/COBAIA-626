using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCheckpoint1 : MonoBehaviour
{
    public Cube cube;
    public GameObject spawn;

    public GameObject spawn1;

    void Update(){
        if(transform.position.y < -4f){
            cube.rb.position = spawn.transform.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Floor")
        {
            cube.rb.position = spawn.transform.position;
        }

        if (other.name == "Floor (1)")
        {
            cube.rb.position = spawn1.transform.position;
        }
    }
}

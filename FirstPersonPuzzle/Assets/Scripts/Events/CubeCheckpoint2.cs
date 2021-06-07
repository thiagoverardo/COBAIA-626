using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCheckpoint2 : MonoBehaviour
{
    public Cube cube;
    public GameObject spawn;

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Floor")
        {
            cube.cubeInitPos = spawn.transform.position;
        }
    }
}

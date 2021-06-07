using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCheckpoint : MonoBehaviour
{
    private GameMaster gm;
    public Cube cube;

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "CheckPoint 1" || other.name == "CheckPoint2(Hairdryer)")
        {
            cube.cubeInitPos = other.transform.position + new Vector3(1, 0, 1);
        }
    }
}

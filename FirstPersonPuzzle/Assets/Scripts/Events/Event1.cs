using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event1 : MonoBehaviour
{

    private GameObject[] gameObjects;
    private void OnTriggerEnter(Collider other)
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Plataformas1");
        PlayEvent();
    }

    private void PlayEvent()
    {
        for(var i = 0 ; i < gameObjects.Length ; i++)
        {
            Destroy(gameObjects[i]);
        }
    }
}
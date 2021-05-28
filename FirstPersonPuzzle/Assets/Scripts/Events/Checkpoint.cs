using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameMaster gm;
    public AudioSource checkpointSound;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkpointSound.Play();
            gm.lastCheckPoint = transform.position;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Destroy(gameObject);
    }
}

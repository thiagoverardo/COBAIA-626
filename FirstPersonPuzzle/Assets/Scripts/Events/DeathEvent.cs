using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEvent : MonoBehaviour
{
    private GameMaster gm;
    private bool restart = false;
    private GameObject player;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("CAIU");
        restart = true;
    }

    void OnTriggerExit(Collider other)
    {
        player.transform.position = gm.lastCheckPoint;
    }

    void Update()
    {
        if (restart)
        {
            player.transform.position = gm.lastCheckPoint;
            restart = false;
        }
    }
}

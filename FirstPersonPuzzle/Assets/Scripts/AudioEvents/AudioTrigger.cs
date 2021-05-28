using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{

    public AudioSource narrate;
    bool isNarrating = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isNarrating)
        {
            AudioManager.SetNarration(narrate);
            isNarrating = true;
        }

    }
}

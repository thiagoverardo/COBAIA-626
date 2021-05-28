using UnityEngine;


public class AudioManager : MonoBehaviour
{
    private AudioSource currentAudio;
    private static AudioManager _instance;
    void Awake()
    {
        _instance = this;
    }
    public static void SetNarration(AudioSource audioClip)
    {
        if (_instance.currentAudio)
        {
            _instance.currentAudio.Stop();
        }
        _instance.currentAudio = audioClip;
        _instance.currentAudio.Play();
    }
}
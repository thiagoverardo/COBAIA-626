using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public float transitionTime = 1f;
    public int sceneId;
    public Animator transition;

    public void LoadGameOver()
    {
        StartCoroutine(LoadLevel(sceneId));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // animation
        transition.SetTrigger("Start");
        //wait 
        yield return new WaitForSeconds(transitionTime);
        //LoadScene
        SceneManager.LoadScene(levelIndex);
    }
}

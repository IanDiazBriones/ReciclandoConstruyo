using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationLevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    public void LoadNextLevel(string levelname)
    {
    	StartCoroutine(LoadLevel(levelname));
    }

    public void LoadNextLevelTerremoto(string levelname)
    {
        StartCoroutine(LoadLevelTerremoto(levelname));
    }

    IEnumerator LoadLevel(string levelname)
    {
    	transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(levelname);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator LoadLevelTerremoto(string levelname)
    {
        yield return new WaitForSeconds(10f);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        Handheld.Vibrate();
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(levelname);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

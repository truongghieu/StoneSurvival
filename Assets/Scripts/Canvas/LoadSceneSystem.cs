using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadSceneSystem : MonoBehaviour
{
  

    [SerializeField] private Image loadingBar;



    public void LoadSceneAsync(string sceneName){
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }



  IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        // Ensure that the loading bar starts at 0
        loadingBar.fillAmount = 0f;

        // Minimum display time (adjust as needed)
        float minDisplayTime = 1.0f;

        // Wait until the asynchronous operation is done
        while (!asyncOperation.isDone)
        {
            // Update the loading bar fill amount smoothly
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.99f); // 0.9 is used as a normalization factor
            loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, progress, Time.deltaTime * 5f); // Adjust the speed of the fill

            // If the loading is complete, wait for a minimum display time
            if (asyncOperation.progress >= 0.99f)
            {
                yield return new WaitForSeconds(minDisplayTime);
                asyncOperation.allowSceneActivation = true; // Activate the scene after the minimum display time
            }

            yield return null;
        }
    }


}

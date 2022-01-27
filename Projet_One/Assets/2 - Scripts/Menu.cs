using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
  public void LauncheGame()
    {
        StartCoroutine(IWait(2));
    }

    private IEnumerator IWait(float t)
    {
        //TransitionSystem.Instance.OpenCurtain();
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public TextMeshProUGUI tBestScore;

    private void Awake()
    {
        tBestScore.text = $"HIGHEST SCORE <br> {PlayerPrefs.GetInt("BestScore").ToString()}";
    }


    public void LauncheGame()
    {
        StartCoroutine(IWait(2));
    }

    private IEnumerator IWait(float t)
    {
        //TransitionSystem.Instance.OpenCurtain();
        GameObject player = GameObject.Find("Player");
        player.GetComponent<Player_Movement>().enabled = false;
        Destroy(player.transform.GetChild(1).gameObject);
            
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}

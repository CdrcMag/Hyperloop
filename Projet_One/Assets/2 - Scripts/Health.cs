using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public static Health Instance;

    public SpriteRenderer[] healthBar = new SpriteRenderer[3];

    public Transform objects;

    private void Awake()
    {
        Instance = this;
    }

    private int vie = 3;

    public void AddHealth(int val)
    {
        vie += val;
        UpdateHealthBar();
    }

    public void RemoveHealth(int val)
    {
        vie -= val;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (vie == 3)
        {
            healthBar[0].color = Color.green;
            healthBar[1].color = Color.green;
            healthBar[2].color = Color.green;
        }
        if (vie == 2)
        {
            healthBar[0].color = Color.green;
            healthBar[1].color = Color.green;
            healthBar[2].color = Color.red;
        }
        if (vie == 1)
        {
            healthBar[0].color = Color.green;
            healthBar[1].color = Color.red;
            healthBar[2].color = Color.red;
        }
        if (vie == 0)
        {
            healthBar[0].color = Color.red;
            healthBar[1].color = Color.red;
            healthBar[2].color = Color.red;

            GameOver();
        }
    }

    private void GameOver()
    {
        GetComponent<Scene_Manager_1>().spawning = false;
        Transition2.Instance.Augment();

        Destroy(objects.gameObject);
        StartCoroutine(ILoadScene());
        //Charger le menu
    }

    IEnumerator ILoadScene()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }



}

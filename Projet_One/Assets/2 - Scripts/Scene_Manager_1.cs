using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scene_Manager_1 : MonoBehaviour
{
    private Vector2[] spawnPositions = new Vector2[3];
    public Color32[] colors = new Color32[3];

    private float cpt = 0;
    public float timeBetweenSpawns = 1.5f;
    public float fallerSpeed = 5f;

    public Transform objects;

    public GameObject objPrefab;
    public GameObject healerPrefab;

    public bool spawning = true;

    // Start is called before the first frame update
    void Start()
    {
        spawnPositions[0] = new Vector2(-1.5f, 6);
        spawnPositions[1] = new Vector2(0, 6);
        spawnPositions[2] = new Vector2(1.5f, 6);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawning)
            return;

        cpt += Time.deltaTime;
        if(cpt >= timeBetweenSpawns)
        {
            SpawnFaller();
            cpt = 0;
        }
    }

    private void SpawnFaller()
    {
        int rand = Random.Range(0, 20);
        GameObject a;

        if(rand > 1)
        {
            a = Instantiate(objPrefab, spawnPositions[Random.Range(0, 3)], Quaternion.identity, objects);
            a.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, 3)];
            a.GetComponent<Faller>().speed = fallerSpeed;
        }   
        else
        {
            a = Instantiate(healerPrefab, spawnPositions[Random.Range(0, 3)], Quaternion.identity, objects);
        }
            

        
    }

    public Transform[] carres;

    //0 = red, 1 = green, 2 = blue//
    //----------------------A1,A2,A3,B1,B2,B3,C1,C2,C3
    private int[] field_1 = {0,2,1, 1,0,2, 2,1,0};
    private int[] field_2 = {0,0,0, 0,0,0, 0,0,0};

    [ContextMenu("Set Colors")]
    public void SetCarresColors()
    {
        print(carres.Length);
        for (int i = 0; i < carres.Length; i++)
        {
            if (field_1[i] == 0) carres[i].GetComponent<SpriteRenderer>().color = colors[0];
            if (field_1[i] == 1) carres[i].GetComponent<SpriteRenderer>().color = colors[1];
            if (field_1[i] == 2) carres[i].GetComponent<SpriteRenderer>().color = colors[2];
        }
    }

}

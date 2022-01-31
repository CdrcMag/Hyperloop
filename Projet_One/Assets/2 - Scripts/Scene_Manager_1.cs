using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scene_Manager_1 : MonoBehaviour
{
    private Vector2[] spawnPositions = new Vector2[3];
    public Color32[] colors = new Color32[3];

    public Color32 healerColor;

    private float cpt = 0;
    

    [Header("Where to spawn objects")]
    public Transform objects;

    [Header("Prefabs")]
    public GameObject objPrefab;
    public GameObject healerPrefab;

    [Header("Settings")]
    public bool spawning = true;
    public float timeBetweenSpawns = 1.5f;
    public float fallerSpeed = 5f;
    public int chanceToSpawnHealer = 1;

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
        int rand = Random.Range(1, 101);
        GameObject a;

        if(rand > chanceToSpawnHealer)
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

    [Header("Grille de déplacement")]
    public Transform[] carres;

    //0 = red, 1 = green, 2 = blue//
    //----------------------A1,A2,A3,B1,B2,B3,C1,C2,C3
    [Header("Colors sets")]
    public int colorTabID = 0;
    public int[] field_0 = { 0, 2, 1, 1, 0, 2, 2, 1, 0 };
    public int[] field_1 = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] field_2 = { 2, 1, 0, 0, 2, 1, 1, 0, 0 };


    [ContextMenu("Set Colors")]
    public void SetCarresColors()
    {
        List<int[]> fields = new List<int[]>();
        //fields.Add(field_0);
        //fields.Add(field_1);
        //fields.Add(field_2);

        for (int i = 0; i < carres.Length; i++)
        {
            if (fields[colorTabID][i] == 0) carres[i].GetComponent<SpriteRenderer>().color = colors[0];
            if (fields[colorTabID][i] == 1) carres[i].GetComponent<SpriteRenderer>().color = colors[1];
            if (fields[colorTabID][i] == 2) carres[i].GetComponent<SpriteRenderer>().color = colors[2];
        }
    }

}

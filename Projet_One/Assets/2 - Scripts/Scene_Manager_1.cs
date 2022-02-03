using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scene_Manager_1 : MonoBehaviour
{
    private Vector2[] spawnPositions = new Vector2[3];
    public Color32[] colors = new Color32[3];

    [Header("Faller colors")]
    public Color32 healerColor;
    public Color32 colorChangerColor;
    public Color32 colorDestroyer;

    private float cpt = 0;
    

    [Header("Where to spawn objects")]
    public Transform objects;

    [Header("Prefabs")]
    public GameObject objPrefab;
    public GameObject healerPrefab;
    public GameObject colorChangerPrefab;
    public GameObject destroyerPrefab;

    [Header("Settings")]
    public bool spawning = true;
    public float timeBetweenSpawns = 1.5f;
    public float fallerSpeed = 5f;

    [Header("Chances")]
    public int chanceToSpawnHealer = 1;
    public int chanceToColorChanger = 1;
    public int chanceToDestroyer = 1;

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
        //Génère un entier aléatoire
        int rand = Random.Range(1, 101);
        
        //Ajoute toutes les variables de chances d'apparition dans une liste
        List<int> chances = new List<int>();
        chances.Add(chanceToSpawnHealer);
        chances.Add(chanceToColorChanger);
        chances.Add(chanceToDestroyer);
        
        //Ordonne la liste de façon croissante
        chances.Sort();

        //Imbriqué du plus de chance de spawn au moins de chance
        if (rand <= chances[chances.Count-1])
        {
            if(rand <= chances[chances.Count-2])
            {
                if (rand <= chances[chances.Count - 3])
                {
                    if (chances[chances.Count - 3] == chanceToSpawnHealer) Spawn_Healer();
                    if (chances[chances.Count - 3] == chanceToColorChanger) Spawn_ColorChanger();
                    if (chances[chances.Count - 3] == chanceToDestroyer) Spawn_Destroyer();
                }
                
                if (chances[chances.Count - 2] == chanceToSpawnHealer) Spawn_Healer();
                if (chances[chances.Count - 2] == chanceToColorChanger) Spawn_ColorChanger();
                if (chances[chances.Count - 2] == chanceToDestroyer) Spawn_Destroyer();
                
            }
            else
            {
                if (chances[chances.Count - 1] == chanceToSpawnHealer) Spawn_Healer();
                if (chances[chances.Count - 1] == chanceToColorChanger) Spawn_ColorChanger();
                if (chances[chances.Count - 1] == chanceToDestroyer) Spawn_Destroyer();
            }            
        }
        else
        {
            Spawn_Normal();
        }
        /* random = 10; ChanceA = 8; ChanceB = 20;
         * SI(random <= ChanceA) ALORS SpawnA();
         * SI(random <= ChanceB) ALORS SpawnB();
         * 
         * Problème : SI random < ChanceA et ChanceB, lequel faire spawn ? => Premier dans la liste des SI, les chances sont alors faussées
         * 
         * Solution : Imbrication des chances.
         * Organise les chances du plus petit au plus grand, et vérifie toutes les cases de la FIN au DEBUT, et fait apparaitre l'objet correspondant 
         * à la valeur dans la liste.
         * 
         */

        
    }

    private void Spawn_Healer()
    {
        GameObject a;
        a = Instantiate(healerPrefab, spawnPositions[Random.Range(0, 3)], Quaternion.identity, objects);
        //a.GetComponent<Faller>().speed = fallerSpeed * 1.5f;
        a.GetComponent<SpriteRenderer>().color = healerColor;
    }
    private void Spawn_Normal()
    {
        GameObject a;
        a = Instantiate(objPrefab, spawnPositions[Random.Range(0, 3)], Quaternion.identity, objects);
        a.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, 3)];
        a.GetComponent<Faller>().speed = fallerSpeed;
    }
    private void Spawn_ColorChanger()
    {
        GameObject a;

        Vector2[] pos = new Vector2[2];

        pos[0] = new Vector2(-0.75f, 6);
        pos[1] = new Vector2(0.75f, 6);

        a = Instantiate(colorChangerPrefab, pos[Random.Range(0, 2)], Quaternion.identity, objects);
        a.GetComponent<Faller>().speed = fallerSpeed * 0.6f;
        a.GetComponent<SpriteRenderer>().color = colorChangerColor;

    }

    private void Spawn_Destroyer()
    {
        GameObject a;
        a = Instantiate(destroyerPrefab, spawnPositions[Random.Range(0, 3)], Quaternion.identity, objects);
        //a.GetComponent<Faller>().speed = fallerSpeed * 1.5f;
        a.GetComponent<SpriteRenderer>().color = colorDestroyer;
    }


    [Header("Grille de déplacement")]
    public Transform[] carres;

    //0 = red, 1 = green, 2 = blue//
    //----------------------A1,A2,A3,B1,B2,B3,C1,C2,C3
    [Header("Colors sets")]
    public int colorTabID = 0;
    public int[] field_0 = { 0, 2, 1, 1, 0, 2, 2, 1, 0 };
    public int[] field_1 = { 1, 0, 2, 2, 1, 0, 0, 2, 1 };
    public int[] field_2 = { 2, 1, 0, 0, 2, 1, 1, 0, 0 };

    private int previousId = 0;

    [ContextMenu("Set Colors")]
    public void SetCarresColors()
    {
        List<int[]> fields = new List<int[]>();
        fields.Add(field_0);
        fields.Add(field_1);
        fields.Add(field_2);

        //Assigne un index aléatoire
        colorTabID = RandomIndex(fields);

        //Stock le dernier index
        previousId = colorTabID;

        //Assigne les couleurs
        for (int i = 0; i < carres.Length; i++)
        {
            if (fields[colorTabID][i] == 0) carres[i].GetComponent<SpriteRenderer>().color = colors[0];
            if (fields[colorTabID][i] == 1) carres[i].GetComponent<SpriteRenderer>().color = colors[1];
            if (fields[colorTabID][i] == 2) carres[i].GetComponent<SpriteRenderer>().color = colors[2];

            carres[i].GetComponent<Animator>().SetTrigger("Turn");
        }
    }

    private int RandomIndex(List<int[]> f)
    {
        for(int i = 0; i < 10; i++)
        {
            int rand = Random.Range(0, f.Count);
            if (rand != previousId) return rand;
        }

        return 0;
    }

}

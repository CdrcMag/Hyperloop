using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleChanger : MonoBehaviour
{
    private List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();

    private void Awake()
    {
        foreach(Transform t in transform)
        {
            texts.Add(t.GetComponent<TextMeshProUGUI>());
        }

        
    }

    private void Start()
    {
        StartCoroutine(IGOGO());
    }

    IEnumerator IGOGO()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < texts.Count; i++)
        {
            texts[i].GetComponent<Animator>().SetTrigger("Go");
            yield return new WaitForSeconds(0.05f);
        }
    }

    float cpt;

    public Color32[] colors;

    private void Update()
    {
        cpt += Time.deltaTime;
        if (cpt >= 0.5f)
        {
            ChangeColors();
            cpt = 0;
        }
    }

    private void ChangeColors()
    {
        for(int i = 0; i < texts.Count; i++)
        {
            //float r = Random.Range(150, 250);
            //float g = Random.Range(0, 100);
            //float b = Random.Range(0, 100);
            //texts[i].color = new Color(r/255, g/255, b/255, 255);
            texts[i].color = colors[Random.Range(0, colors.Length)];
        }
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public static ColorChanger Instance;

    private float timer = 1;
    private float cpt = 0;

    private SpriteRenderer spriteRenderer;

    public Color[] colors;

    private void Awake()
    {
        Instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Random at start
        ChangeColor();
    }

    private void Update()
    {
        cpt += Time.deltaTime;
        if(cpt >= timer)
        {
            ChangeColor();

            cpt = 0;
        }
    }





    public void ChangeColor()
    {
        int randomColor = Random.Range(0, colors.Length);

        //Resets timer in case the color changes from elsewhere
        cpt = 0;

        spriteRenderer.color = colors[randomColor];
    }
    

}

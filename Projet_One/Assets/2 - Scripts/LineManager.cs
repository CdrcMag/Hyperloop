using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public GameObject JeanFrancoisPrefab;
    public float speed = 100;
    private List<Transform> positions = new List<Transform>();

    private TrailRenderer trail;

    private void Awake()
    {

        foreach(Transform child in transform)
        {
            positions.Add(child);
        }

        StartCoroutine(IMove());
    }

    private IEnumerator IMove()
    {
        int currentTarget = 1;
        
        bool moving = true;

        float val = 0;

        GameObject JeanFrancois = Instantiate(JeanFrancoisPrefab, positions[0].position, Quaternion.identity);

        trail = JeanFrancois.GetComponent<TrailRenderer>();

        while (moving)
        {
            if(Vector2.Distance(JeanFrancois.transform.position, positions[currentTarget].position) > 0.1f)
            {
                JeanFrancois.transform.position = Vector2.Lerp(JeanFrancois.transform.position, positions[currentTarget].position, val += (speed * Time.deltaTime));
            }
            else
            {
                currentTarget = Random.Range(0, positions.Count);
                val = 0;
                SetTrailColor();   
            }

            

            yield return null;
        }
    }

    [Header("Alphas")]
    public float alphaValue;

    [Header("Color")]
    public Color colorA;
    public Color colorB;

    private void SetTrailColor()
    {
        //Color handling
        Gradient grad = new Gradient();
        grad.SetKeys(
            new GradientColorKey[]
              {
                      new GradientColorKey(colorA, 0.0f),
                      new GradientColorKey(colorB, 1.0f) },
              new GradientAlphaKey[] {
                      new GradientAlphaKey(alphaValue, 0.0f),
                      new GradientAlphaKey(alphaValue, 0.5f),
                      new GradientAlphaKey(alphaValue, 1.0f)
              });

        trail.colorGradient = grad;
    }
}

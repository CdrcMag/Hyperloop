using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition2 : MonoBehaviour
{
    public static Transition2 Instance;

    public Transform parent;

    private List<Transform> carres = new List<Transform>();

    public float speed;
    public float ecart;

    public void Augment()
    {
        StartCoroutine(IAugment());
    }

    public void Reduce() => StartCoroutine(IReduce());

    private void Awake()
    {
        Instance = this;

        foreach (Transform t in parent)
        {
            carres.Add(t);
        }

        StartCoroutine(IReduce());

    }

    IEnumerator IAugment()
    {
        SetStates(false);

        yield return new WaitForSeconds(0f);

        for(int i = 0; i < carres.Count; i++)
        {
            yield return new WaitForSeconds(ecart);
            StartCoroutine(Augment(carres[i]));
        }

    }

    IEnumerator IReduce()
    {
        SetStates(true);

        yield return new WaitForSeconds(0f);

        for (int i = 0; i < carres.Count; i++)
        {
            yield return new WaitForSeconds(ecart);
            StartCoroutine(Reduce(carres[i]));
        }

    }

    IEnumerator Reduce(Transform t)
    {
        t.localScale = new Vector2(1, 1);
        t.gameObject.SetActive(true);

        while (t.localScale.x > 0)
        {
            t.localScale = new Vector2(t.localScale.x - speed, t.localScale.y - speed);
            yield return null;
        }

        t.gameObject.SetActive(false);

        //Destroy(t.gameObject);
    }

    IEnumerator Augment(Transform t)
    {
        t.localScale = new Vector2(0, 0);
        t.gameObject.SetActive(true);

        while (t.localScale.x < 2.5f)
        {
            t.localScale = new Vector2(t.localScale.x + speed, t.localScale.y + speed);
            yield return null;
        }

        t.localScale = new Vector2(2.5f, 2.5f);
        t.gameObject.SetActive(true);

        //Destroy(t.gameObject);
    }

    private void SetStates(bool state)
    {
        foreach(Transform t in carres)
        {
            t.gameObject.SetActive(state);
        }
    }



}

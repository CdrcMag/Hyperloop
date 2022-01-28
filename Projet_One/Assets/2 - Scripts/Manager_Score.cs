using System.Collections;
using TMPro;
using UnityEngine;

public class Manager_Score : MonoBehaviour
{
    public TextMeshProUGUI tScore;
    public TextMeshProUGUI tMulti;
    public TextMeshProUGUI tRow;

    public GameObject tPopUpPrefab;
    private Transform player;

    public int score = 0;

    public int inARow = 0;
    public int multiplier = 1;

    [Header("Pop-Up settings")]
    [SerializeField] private Vector2 offset;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    public void AddScore()
    {
        int finalScore = 100 * multiplier;
        score += finalScore;

        PopUp(finalScore);

        StartCoroutine(ISizeText());
        tScore.text = score.ToString();
    }

    public void AddInARow(int val)
    {
        inARow += val;

        if (inARow < 5) multiplier = 1;
        if (inARow >= 6 && inARow < 10) multiplier = 2;
        if (inARow >= 10 && inARow < 15) multiplier = 3;
        if (inARow >= 16) multiplier = 4;

        tMulti.text = $"X{multiplier}";
        tRow.text = inARow.ToString();

    }

    public void ResetMultiplier()
    {
        multiplier = 1;
    }
    public void ResetInRow()
    {
        inARow = 0;
        tRow.text = inARow.ToString();
        tMulti.text = $"X{multiplier}";
    }

    IEnumerator ISizeText()
    {
        float targetSize = 140;
        float originalSize = 70;
        float sizingSpeed = 12;

        while(tScore.fontSize < targetSize)
        {
            tScore.fontSize += sizingSpeed;
            yield return null;
        }

        tScore.fontSize = targetSize;

        while (tScore.fontSize > originalSize)
        {
            tScore.fontSize -= sizingSpeed;
            yield return null;
        }

        tScore.fontSize = originalSize;


    }

    private void PopUp(int val)
    {
        Vector2 pos = (Vector2)player.position + offset;

        //Rotation
        Quaternion rotation = Quaternion.Euler(0, 0, -10);

        GameObject pop = Instantiate(tPopUpPrefab, pos, rotation);

        pop.GetComponentInChildren<TextMeshPro>().text = $"+{val}";
        Destroy(pop, 3f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager_Score : MonoBehaviour
{
    public TextMeshProUGUI tScore;
    public TextMeshProUGUI tMulti;
    public TextMeshProUGUI tRow;
    public int score = 0;

    public int inARow = 0;
    public int multiplier = 1;

    public void AddScore()
    {
        score += 100 * multiplier;

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

}

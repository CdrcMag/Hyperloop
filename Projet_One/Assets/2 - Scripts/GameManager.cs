using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int frameRate;
    private void Awake()
    {
        Application.targetFrameRate = frameRate;
        QualitySettings.vSyncCount = 0;
    }
}

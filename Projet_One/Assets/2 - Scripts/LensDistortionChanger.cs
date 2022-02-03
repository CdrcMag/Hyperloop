using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LensDistortionChanger : MonoBehaviour
{
    public GameObject vol;//Game object with volume
    Volume ppvolume;
    private LensDistortion LD;

    public static LensDistortionChanger Instance;

    [Header("Max Values")]
    public float max;
    public float min;

    [Header("Settings")]
    [Tooltip("Augmentation jusqu'à max")]   public float speed_1;
    [Tooltip("Rétrécissement jusqu'à min")] public float speed_2;
    [Tooltip("Augmentation jusqu'à 0")]     public float speed_3;

    private void Awake()
    {
        Instance = this;

        ppvolume = vol.GetComponent<Volume>();
        LensDistortion tmp;

        if (ppvolume.profile.TryGet(out tmp))
        {
            LD = tmp;
        }

    }

    public void ChangeLens(float intensity) => StartCoroutine(IChangeLens(intensity));

    private IEnumerator IChangeLens(float i)
    {
        i = Mathf.Clamp(i, 1, 5);

        bool moving = true;

        bool state1 = true;
        bool state2 = false;
        bool state3 = false;
        
        //Augmente l'intensité jusqu'à max, puis rétrecit jusqu'à min, pour remonter jusqu'à 0
        while(moving)
        {
            if(state1)
            {
                if (LD.intensity.value < max)
                {
                    LD.intensity.Override(LD.intensity.value + speed_1 * i);
                }
                else
                {
                    state1 = false; state2 = true;
                }
            }

            if(state2)
            {
                if (LD.intensity.value > min)
                {
                    LD.intensity.Override(LD.intensity.value - speed_2 * i);
                }
                else
                {
                    state2 = false; state3 = true;
                }
            }

            if(state3)
            {
                if (LD.intensity.value < -0.1f)
                {
                    LD.intensity.Override(LD.intensity.value + speed_3 * i);
                }
                else
                {
                    state1 = false;
                    state2 = false;
                    state3 = false;
                    moving = false;
                }
            }
            
            yield return null;
        }

        LD.intensity.Override(0);

        yield return null;
    }
}

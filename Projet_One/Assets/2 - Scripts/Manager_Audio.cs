using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//[RequireComponent(typeof(Manager_Audio))]
public class Manager_Audio : MonoBehaviour
{
    public static Manager_Audio Instance;

    private AudioSource _audioSource;

    private void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("Sound_State") == 1) EnableAudio();
        if (PlayerPrefs.GetInt("Sound_State") == 0) MuteAudio();
    }

    public AudioClip[] fx;
    public AudioClip[] fx_movement;

    public void PlayFx(int id, float volume = 1f)
    {
        //_audioSource.pitch = .2f;
        _audioSource.PlayOneShot(fx[id]);
    }

    public void PlayFxMovement(float volume = 1)
    {
        _audioSource.PlayOneShot(fx_movement[Random.Range(0, fx_movement.Length)], volume);
    }

    public void MuteAudio()
    {
        _audioSource.enabled = false;
        PlayerPrefs.SetInt("Sound_State", 0);
        SetStuff(false);
    }

    public void EnableAudio()
    {
        _audioSource.enabled = true;
        PlayerPrefs.SetInt("Sound_State", 1);
        SetStuff(true);
    }

    public TextMeshProUGUI OnText;
    public TextMeshProUGUI OffText;
    public Transform img;

    //0 et -72
    private void SetStuff(bool val)
    {
        if(!val)//false = sound off
        {
            OnText.color = Color.black;
            OffText.color = Color.white;
            StartCoroutine(MoveImg(10));
        }
        if (val)//true = sound on
        {
            OnText.color = Color.white;
            OffText.color = Color.black;
            StartCoroutine(MoveImg(-10));
        }

    }

    private IEnumerator MoveImg(float speed)
    {
        if(speed > 0)
        {
            while (img.localPosition.x <= 14)
            {
                img.localPosition = new Vector2(img.localPosition.x + speed, img.localPosition.y);
                yield return null;
            }

            img.localPosition = new Vector2(14, img.localPosition.y);

        }

        if (speed < 0)
        {
            while (img.localPosition.x >= -72)
            {
                img.localPosition = new Vector2(img.localPosition.x + speed, img.localPosition.y);
                yield return null;
            }

            img.localPosition = new Vector2(-72, img.localPosition.y);

        }

    }



}

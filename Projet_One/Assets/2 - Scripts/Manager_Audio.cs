using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Manager_Audio))]
public class Manager_Audio : MonoBehaviour
{
    public static Manager_Audio Instance;

    private AudioSource _audioSource;

    private void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    public AudioClip[] fx;

    public void PlayFx(int id, float volume = 1f)
    {
        //_audioSource.pitch = .2f;
        _audioSource.PlayOneShot(fx[id]);
    }






}

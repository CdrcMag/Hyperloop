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





}

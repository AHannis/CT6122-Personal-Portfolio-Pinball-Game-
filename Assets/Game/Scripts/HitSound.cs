using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitSound : MonoBehaviour
{
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private float volume = 1f;

    private AudioSource audioSource;

    void Awake()
    {
        
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;   
        audioSource.volume = volume;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Ball"))
        {
            return;
        }

        PlaySound();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            PlaySound();
        }
    }

    private void PlaySound()
    {
        if (hitSound == null)
        {
            return;
        }

        audioSource.PlayOneShot(hitSound, volume);
    }
}

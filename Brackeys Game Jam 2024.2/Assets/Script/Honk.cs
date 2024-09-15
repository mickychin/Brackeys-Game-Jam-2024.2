using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honk : MonoBehaviour
{
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnMouseDown()
    {
        audioSource.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSoundSource : MonoBehaviour
{
    public AudioSource Source;

    private void Awake()
    {
    }

    public void PlayClip()
    {
        Source.Play();   
    }

}

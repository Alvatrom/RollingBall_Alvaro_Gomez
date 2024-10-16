using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceSfx;
    // Start is called before the first frame update
    public void ReproducirSonido(AudioClip clip)
    {
        //Reproducir una vez el sonido
        audioSourceSfx.PlayOneShot(clip);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ParedCubos : MonoBehaviour
{
    private bool IniciarTimer;
    private float timer = 0;
    [SerializeField] private Rigidbody[] rbs;

    //1. crear timer para que empiece a contar una vez iniciado
    //2. hacer que el timer cuente hasta 2
    //una vez el timer haya comtado 2 , volver al TImeScale a 1
    private void Update()
    {
        if (IniciarTimer== true)
        {
            timer += 1 * Time.unscaledDeltaTime;
            if (timer >= 2)
            {
                Time.timeScale = 1;
                for (int i = 0; i < rbs.Length; i++)
                {
                    rbs[i].useGravity = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0.8f;
            IniciarTimer = true;//Para iniciar cuenta para que el tiempo vuelva al 100%

        }
    }
}

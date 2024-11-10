using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFinal : MonoBehaviour
{
    [SerializeField] private new AudioSource audio;
    public Timer timerScript;
    public TMP_Text textoDelTimer;


    void Start()
    {
        timerScript = FindObjectOfType<Timer>();
        audio = GetComponent<AudioSource>();
        Time.timeScale = 1.0f;

        if (timerScript != null)
        {
            // Formatea el valor del timer de timerScript y lo asigna a textoDelTimer.text
            int minutos = Mathf.FloorToInt(timerScript.timer / 60);
            int segundos = Mathf.FloorToInt(timerScript.timer % 60);
            textoDelTimer.text = string.Format("Timer: {0:00}:{1:00}", minutos, segundos);

            Debug.Log("Valor de textoTimer asignado a MenuFinal: " + textoDelTimer.text);
        }
        else
        {
            Debug.LogWarning("No se encontró el script 'Timer' en la escena.");
        }

    }

    public void Salir()
    {
        print("cerrando juego...");
        Application.Quit();
    }


    public void JugarDeNuevo()
    {
        SceneManager.LoadScene("Game");
    }
    public void BuscarPlayer()
    {
        //para encontrar si o si al player
        if (timerScript == null)
        {
            timerScript = FindObjectOfType<Timer>();

            if (timerScript == null)
            {
                Debug.LogWarning("No se encontró ningún objeto de tipo 'Timer' en la escena.");
            }
        }

    }
}

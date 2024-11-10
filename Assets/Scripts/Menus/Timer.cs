using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timer = 0f;

    public TMP_Text textoTimer;
    public Player player;

    public TMP_Text TextoTimer { get => textoTimer; set => textoTimer = value; }

    public static Timer instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            //Suscribirse al evento de escena cargada
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        DetectarEscena();
        BuscarPlayer();
        if(player!= null)
        {
            timer += Time.deltaTime;
            ActualizarContador();
        }
        else
        {
            ActualizarContador();
        }
        
    }
    public void ActualizarContador()
    {
        int minutos = Mathf.FloorToInt(timer / 60);//redondea hacia abajo y convierte de float a int
        int segundos = Mathf.FloorToInt(timer % 60);//el resto, el resto son los segundos
        textoTimer.text = string.Format("Timer: " + "{0:00}:{1:00}", minutos, segundos);
    }
    public void ReiniciarContador()
    {
        timer = 0f;
        ActualizarContador();
        BuscarPlayer();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            ReiniciarContador();
        }
    }

    public void DetectarEscena()
    {
        if (SceneManager.GetActiveScene().name != "Game")
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            BuscarPlayer();
        }
    }
    public void BuscarPlayer()
    {
        //para encontrar si o si al player
        if (player == null)
        {
            player = FindObjectOfType<Player>();

            if (player == null)
            {
                Debug.LogWarning("No se encontró ningún objeto de tipo 'Player' en la escena para el timer.");
            }
            else if( player != null)
            {
                gameObject.SetActive(true);
            }
        }
       
    }
    private void OnDestroy()
    {
        // Desuscribirse del evento cuando el objeto se destruya
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}

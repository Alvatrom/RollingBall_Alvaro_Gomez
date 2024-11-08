using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject menuPausa, canvasMuerte;


    private int vidas = 3;

    public int Vidas1 { get => vidas; }

    public Player player;// lo hacemos publico para poder usarlo luego

    //SINGLETON
    public static GameManager instance;



    void Start()
    {
        //INICIALIZACION SINGLETON
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //FIN INICIALIZACION SINGLETON

        // NO DESTRUIR ENTRE ESCENAS
        DontDestroyOnLoad(gameObject);


        menuPausa.SetActive(false);
        canvasMuerte.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Game" || Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Training")
        {
            if (menuPausa.activeSelf)
            {
                menuPausa.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                menuPausa.SetActive(true);
            }
            menuPausa.SetActive(true);
        }
        if (player.VidasRestantes <= 0 && canvasMuerte != null)
        {
            canvasMuerte.SetActive(true);
        }
    }

    public void PerderVida()
    {
        vidas -= 1;
        AudioManager.instance.PlaySFX("Daño");
        player.DesactivarVida(vidas);
    }

    public void RecuperarVida()
    {
        player.ActivarVida(vidas);
        vidas += 1;
    }

    public void Reanudar()
    {
        Time.timeScale = 1;
        menuPausa.SetActive(false);

    }

    public void ReiniciarPartida()
    {
        // Llamar al método ResetPlayer() del Singleton Player
        if (Player.instance != null)
        {
            Player.instance.ResetPlayer();
        }
        else
        {
            Debug.LogWarning("No se encontró la instancia de Player al reiniciar la partida.");
        }

        menuPausa.SetActive(false);
        canvasMuerte.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Salir()
    {
        print("cerrando juego...");
        Application.Quit();
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Titulo");
        menuPausa.SetActive(false);
    }
}
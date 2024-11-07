using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject MenuPausa;

    private int vidas = 3;

    public int Vidas1 { get => vidas;}

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


        MenuPausa.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Game" || Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Training")
        {
            if (MenuPausa.activeSelf)
            {
                MenuPausa.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                MenuPausa.SetActive(true);
            }
            MenuPausa.SetActive(true);
        }
    }

    public void PerderVida()
    {
        vidas -= 1;
        AudioManager.Instance.PlaySFX("Daño");
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
        MenuPausa.SetActive(false);

    }

    public void ReiniciarPartida()
    {
        //Scene escena = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(escena.name);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        MenuPausa.SetActive(false);
    }

    public void Salir()
    {
        print("cerrando juego...");
        Application.Quit();
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Titulo");
        MenuPausa.SetActive(false);
    }
}

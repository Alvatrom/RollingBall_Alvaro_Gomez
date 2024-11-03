using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject MenuPausa;
    [SerializeField] Toggle silencio;
    [SerializeField] AudioSource audioSource;

    //SINGLETON
    public static GameManager instance;
    


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        DetectarEscenaAudio();


        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Play Mode" || Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Training")
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
    public void SeleccionPersonaje()
    {
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().name == "Play Mode")
        {
            SceneManager.LoadScene("Character Selection");
            Debug.Log("Volviendo a escena" + SceneManager.GetActiveScene().name);
        }
        else if (SceneManager.GetActiveScene().name == "Training")
        {
            SceneManager.LoadScene("Character Selection Train");
            Debug.Log("Volviendo a escena" + SceneManager.GetActiveScene().name);
        }
        MenuPausa.SetActive(false);
    }
    public void Salir()
    {
        print("cerrando juego...");
        Application.Quit();
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Menu Principal");
    }
    public void CambioVolumen(float valor)
    {
        if (valor == 0)
        {
            silencio.isOn = true;
            audioSource.volume = 0;
            print("Silenciado");
        }
        else
        {
            silencio.isOn = false;
            audioSource.volume = valor;
            print("desilenciado");
        }

    }
    public void DetectarEscenaAudio()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "Play Mode")
        {
            silencio.isOn = true;
            audioSource.volume = 0;
            print("Silenciado");
        }

        else
        {
            silencio.isOn = false;
            audioSource.volume = 1;
            print("Desilenciado");
        }
    }
}

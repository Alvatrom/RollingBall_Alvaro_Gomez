using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorEventosEscena : MonoBehaviour
{
    /*void OnEnable()
    {
        // Suscribirse al evento de carga de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Desuscribirse del evento de carga de escena
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Método que se ejecuta cuando se carga una escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verificar si existe una instancia de DialogueManager y resetearla
        if (DialogueManager.instance != null)
        {
            DialogueManager.instance.ResetDialogueManager();
        }

        Player playerinstance = FindObjectOfType<Player>();
        if (Player.instance != null)
        {
            Player.instance.ReiniciarPlayer();
        }
        else
        {
            Debug.LogWarning("No se encontró ninguna instancia de Player en la escena.");
        }
    }*/
}

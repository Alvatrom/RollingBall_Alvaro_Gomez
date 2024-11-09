using UnityEngine;

public class AudioListenerManager : MonoBehaviour//para que cuando se elimine el jugador podamos escuchar el entorno y el sonido de muerte desde la camara
{
    [SerializeField] private AudioListener playerAudioListener;
    [SerializeField] private AudioListener cameraAudioListener;
    [SerializeField] private Player player;

    private void Update()
    {
        BuscarPlayer();
        if (player == null)
        {
            cameraAudioListener.enabled = true;
        }
        else
        {
            cameraAudioListener.enabled = false;
            playerAudioListener.enabled = true;
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
                Debug.LogWarning("No se encontró ningún objeto de tipo 'Player' en la escena.");
            }
        }
    }
}
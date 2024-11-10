using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] Vector3 direccion;
    [SerializeField] private float fuerza = 5, fuerzaDevil = 2, fuerzaSalto = 8f;
    [SerializeField] private float DistanciaRaycast = 3.32f;
    [SerializeField] private Color colorEmissiveDanho = Color.red; // color al recibir daño
    [SerializeField] TMP_Text textoPuntuacion;
    [SerializeField] GameObject textoPortal;
    [SerializeField] private float timerParpadeoTotal = 0, timerParpadeoIntermitente = 0, tiempoParpadeoTotal = 1, tiempoParpadeoIntermitente = 0.15f;
    [SerializeField] GameObject vidasLlenas;
    private bool parpadeando = false;
    public GameObject[] vidas;
    public GameObject portal;
    
    public int indiceVidas = 0;
    private int vidasRestantes;
    private Color colorEmissiveOriginal;
    private Material material;
    MeshRenderer mr;
    Rigidbody rb;
    float h, v;
    int objetos = 0, objetosTotales = 5;

    bool playerVivo = true;

    //public static Player instance;

    public int VidasRestantes { get => vidasRestantes; set => vidasRestantes = value; }
    public bool PlayerVivo { get => playerVivo; set => playerVivo = value; }


    void Start()
    {
        GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
        material = mr.material;
        colorEmissiveOriginal = material.GetColor("_EmissionColor"); //guardar el color original emissive, tenemos que usar ese nombre si o si para poder acceder a la capa
        material.EnableKeyword("_EMISSION");//palabra clave para activar la emission
        vidasRestantes = vidas.Length;//para saber la longitud del array,numero de vidas totales
        vidasLlenas.SetActive(false);
        textoPortal.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (playerVivo)
        {
            Movimiento();
            Salto();
        }
    }

    public void Movimiento()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
    }

    public void Salto()
    {
        if (Input.GetKeyDown(KeyCode.Space) && DetectarSuelo())
        {
            //rb.AddForce(direccion*fuerza,ForceMode.Impulse);
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            AudioManager.instance.PlaySFX("Jump");
            //rb.AddForce(new Vector3(0, 1, 0).normalized * fuerzaSalto, ForceMode.Force);
        }
    }

    public void DesactivarVida(int indiceVidas)
    {
        if (indiceVidas >= 0 && indiceVidas < vidas.Length)//verificar nº de vidas
        {
            vidas[indiceVidas].SetActive(false);
            vidasRestantes--;

            if (vidasRestantes <= 0)
            {
                //muerte
                playerVivo= false;
                AudioManager.instance.PlaySFX("Die");
                Destroy(gameObject);
            }
        }
    }

    public void ActivarVida(int indiceVidas)
    {
        if (indiceVidas >= 0 && indiceVidas < vidas.Length)
        {
            vidas[indiceVidas].SetActive(true);
            vidasRestantes++;
        }
    }
    public void ReiniciarPlayer()
    {
        // Resetear variables al estado inicial
        vidasRestantes = vidas.Length; // todas las vidas activas
        indiceVidas = 0;
        objetos = 0;

        // Restablecer las vidas visualmente
        foreach (GameObject vida in vidas)
        {
            if (vida != null)
            {
                vida.SetActive(true);
            }
        }

        // Restablecer el material a su color original
        if (material != null)
        {
            material.SetColor("_EmissionColor", colorEmissiveOriginal);
        }

        parpadeando = false;
        textoPuntuacion.SetText("Capsulas de energia: " + objetos + "/" + objetosTotales);
    }

    private void FixedUpdate()
    {
        if (DetectarSuelo())
        {
            rb.AddForce(new Vector3(-v, 0, h).normalized * fuerza, ForceMode.Force);
        }
        else
        {
            rb.AddForce(new Vector3(-v, 0, h).normalized * fuerzaDevil, ForceMode.Force);
        }

    }
    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Objetos"))
        {
            objetos += 1;
            Destroy(other.gameObject);
            AudioManager.instance.PlaySFX("CapsulaEnergia");
            textoPuntuacion.SetText("Capsulas de energia: " + objetos + "/" + objetosTotales);
            if(objetos == 5)
            {
                ActivarPortal();
            }
        }
        if (other.CompareTag("Cura") && vidasRestantes < 3)
        {
            Destroy(other.gameObject);
            AudioManager.instance.PlaySFX("Vida");
            GameManager.instance.RecuperarVida();

        }
        else if(other.CompareTag("Cura") && vidasRestantes == 3)
        {
            vidasLlenas.SetActive(true);
            yield return new WaitForSeconds(1f);
            vidasLlenas.SetActive(false);
        }

    }
    private IEnumerator OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstaculo" && !parpadeando)
        {
            StartCoroutine(Parpadeo());
            material.SetColor("_EmissionColor", colorEmissiveDanho); //cambiar al color emissive de daño
            GameManager.instance.PerderVida();
        }
        if (collision.gameObject.tag == "Cubos")
        {
            AudioManager.instance.PlaySFX("GolpeCubo");
        }
        if (collision.gameObject.tag == "Muerte")
        {
            AudioManager.instance.PlaySFX("Die");
            yield return new WaitForSeconds(0.8f);
            playerVivo = false;
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "portal" && portal.activeSelf)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    IEnumerator Parpadeo()
    {
        parpadeando = true;
        float timerParpadeoTotal = tiempoParpadeoTotal;
        float timerParpadeoIntermitente = tiempoParpadeoIntermitente;

        while (timerParpadeoTotal > 0)
        {
            timerParpadeoTotal -= Time.deltaTime;
            timerParpadeoIntermitente -= Time.deltaTime;

            if (timerParpadeoIntermitente <= 0)
            {
                timerParpadeoIntermitente = tiempoParpadeoIntermitente;
                mr.enabled = !mr.enabled; //alternar visibilidad para simular el parpadeo
            }

            yield return null;
        }

        //restaurar el estado original
        material.SetColor("_EmissionColor", colorEmissiveOriginal);
        mr.enabled = true;
        parpadeando = false;
    }
    private bool DetectarSuelo()
    {
        bool resultado = Physics.Raycast(transform.position, new Vector3(0, -3.32f, 0)/*Vector3.down*/, DistanciaRaycast);
        Debug.DrawRay(transform.position, Vector3.down, Color.red, 2f);
        return resultado;
    }
    public void ActivarPortal()
    {
        portal.SetActive(true);
        textoPortal.SetActive(true);
    }
}
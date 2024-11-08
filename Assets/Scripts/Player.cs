using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField] Vector3 direccion;
    [SerializeField] private float fuerza = 5, fuerzaDevil = 2, fuerzaSalto = 8f;
    [SerializeField] private float DistanciaRaycast = 3.32f;
    [SerializeField] private Color colorEmissiveDanho = Color.red; // color al recibir daño
    [SerializeField] TMP_Text textoPuntuacion;
    [SerializeField] private float timerParpadeoTotal = 0, timerParpadeoIntermitente = 0, tiempoParpadeoTotal = 1, tiempoParpadeoIntermitente = 0.15f;
    private bool parpadeando = false;
    public GameObject[] vidas;
    public int indiceVidas = 2;//para sistema de reset de vidas y para desactivar vidas
    public int indiceActivarVidas = 0;// para sistema de activar vidas
    private int vidasRestantes;
    private Color colorEmissiveOriginal;
    private Material material;
    MeshRenderer mr;
    Rigidbody rb;
    float h, v;
    int objetos = 0, objetosTotales = 5;

    public int VidasRestantes { get => vidasRestantes; set => vidasRestantes = value; }

    void Awake()
    {
        // Inicialización del Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // No destruir entre escenas
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destruir duplicados
        }
    }

    void Start()
    {
        GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
        material = mr.material;
        colorEmissiveOriginal = material.GetColor("_EmissionColor"); //guardar el color original emissive, tenemos que usar ese nombre si o si para poder acceder a la capa
        material.EnableKeyword("_EMISSION");//palabra clave para activar la emission
        vidasRestantes = vidas.Length;//para saber la longitud del array,numero de vidas totales

    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && DetectarSuelo())
        {
            //rb.AddForce(direccion*fuerza,ForceMode.Impulse);
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            AudioManager.instance.PlaySFX("Jump");
            //rb.AddForce(new Vector3(0, 1, 0).normalized * fuerzaSalto, ForceMode.Force);
        }


    }
    ////////////////////////////////////////////////////////
    /*public void DesactivarVida()
    {
        if (indiceVidas >= 0 && indiceVidas < vidas.Length)//verificar nº de vidas
        {
            vidas[indiceVidas].SetActive(false);
            vidasRestantes--;

            // Reducir el índice de la vida a desactivar para la próxima vez
            this.indiceVidas--;

            if (vidasRestantes <= 0)
            {
                //muerte
                Destroy(gameObject);
            }
        }
    }

    public void ActivarVida()
    {
        if (indiceActivarVidas >= 0 && indiceActivarVidas < vidas.Length)
        {
            vidas[indiceActivarVidas].SetActive(true);
            vidasRestantes++;

            this.indiceActivarVidas++;
        }
    }*/
    ////////////////////////////////////////////////
    public void DesactivarVida(int indiceVidas)
    {
        if (indiceVidas >= 0 && indiceVidas < vidas.Length)//verificar nº de vidas
        {
            vidas[indiceVidas].SetActive(false);
            vidasRestantes--;

            if (vidasRestantes <= 0)
            {
                //muerte
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

    public void ResetPlayer()
    {
        indiceVidas = vidas.Length - 1;// Reiniciar al último índice del array de vidas(sacas el valor real del indice)
        indiceActivarVidas = 0; //Comenzar activación desde el primer índice

        vidasRestantes = vidas.Length; // Reiniciar vidasRestantes al número total de vidas
        objetos = 0; // Reiniciar el número de objetos recogidos

        // Activar todas las vidas en el array
        foreach (GameObject vida in vidas)
        {
            if (vida != null)
            {
                vida.SetActive(true); // Asegurar que todas las vidas estén activas
            }
        }

        // Actualizar el texto de puntuación
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Objetos"))
        {
            objetos += 1;
            Destroy(other.gameObject);
            AudioManager.instance.PlaySFX("CapsulaEnergia");
            textoPuntuacion.SetText("Capsulas de energia: " + objetos + "/" + objetosTotales);
        }

        if (other.CompareTag("Cura"))
        {
            Destroy(other.gameObject);
            AudioManager.instance.PlaySFX("Vida");
            GameManager.instance.RecuperarVida();

        }
    }
    private void OnCollisionEnter(Collision collision)
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
}
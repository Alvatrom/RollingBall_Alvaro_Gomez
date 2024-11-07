using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Vector3 direccion;
    [SerializeField] private float fuerza = 5, fuerzaDevil = 2, fuerzaSalto = 8f;
    [SerializeField] private float DistanciaRaycast = 3.32f;
    [SerializeField] private Color colorEmissiveDanho = Color.red; // color al recibir daño
    [SerializeField] TMP_Text textoPuntuacion;
    [SerializeField] private float timerParpadeoTotal = 0, timerParpadeoIntermitente = 0,tiempoParpadeoTotal = 1, tiempoParpadeoIntermitente = 0.15f;
    private bool parpadeando = false;
    public GameObject[] vidas;
    private int vidasRestantes;
    public GameObject canvasMuerte;
    private Color colorEmissiveOriginal;
    private Material material;
    MeshRenderer mr;
    Rigidbody rb;
    float h, v;
    int objetos = 0, objetosTotales = 5;


    void Start()
    {
        GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
        material = mr.material;
        colorEmissiveOriginal = material.GetColor("_EmissionColor"); //guardar el color original emissive, tenemos que usar ese nombre si o si para poder acceder a la capa
        material.EnableKeyword("_EMISSION");//palabra clave para activar la emission
        canvasMuerte.SetActive(false);
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
            rb.AddForce(Vector3.up*fuerzaSalto,ForceMode.Impulse);
            AudioManager.Instance.PlaySFX("Jump");
            //rb.AddForce(new Vector3(0, 1, 0).normalized * fuerzaSalto, ForceMode.Force);
        }

        
    }
    public void DesactivarVida(int indice)
    {
        if (indice >= 0 && indice < vidas.Length)//verificar nº de vidas
        {
            vidas[indice].SetActive(false);
            vidasRestantes--;

            if (vidasRestantes <= 0)
            {
                //muerte
                Destroy(gameObject);
                canvasMuerte.SetActive(true);
            }
        }
    }

    public void ActivarVida(int indice)
    {
        if (indice >= 0 && indice < vidas.Length)
        {
            vidas[indice].SetActive(true);
            vidasRestantes++;
        }
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
            AudioManager.Instance.PlaySFX("CapsulaEnergia");
            textoPuntuacion.SetText("Capsulas de energia: " + objetos+ "/" + objetosTotales);
        }

        if (other.CompareTag("Cura") )
        {
            Destroy(other.gameObject);
            AudioManager.Instance.PlaySFX("Vida");
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
            AudioManager.Instance.PlaySFX("GolpeCubo");
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
        bool resultado = Physics.Raycast(transform.position,new Vector3(0,-3.32f, 0)/*Vector3.down*/, DistanciaRaycast);
        Debug.DrawRay(transform.position, Vector3.down, Color.red, 2f);
        return resultado;
    }
}

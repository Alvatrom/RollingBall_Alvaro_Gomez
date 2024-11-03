using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Vector3 direccion;
    [SerializeField] private int vida = 5;
    [SerializeField] private float fuerza = 5, fuerzaSalto = 8f;
    [SerializeField] private float DistanciaRaycast = 3.32f;
    [SerializeField] private float timerParpadeoTotal = 0, timerParpadeoIntermitente = 0,tiempoParpadeoTotal = 1, tiempoParpadeoIntermitente = 0.15f;
    MeshRenderer mr;
    Rigidbody rb;
    float h, v;
    int objetos = 0, objetosTotales = 5;
    [SerializeField] TMP_Text textoPuntuacion;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
        
    }
    
    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Parpadeo();

        if (Input.GetKeyDown(KeyCode.Space) && DetectarSuelo())
        {
            //rb.AddForce(direccion*fuerza,ForceMode.Impulse);
            rb.AddForce(Vector3.up*fuerzaSalto,ForceMode.Impulse);
            AudioManager.Instance.PlaySFX("Jump");
            //rb.AddForce(new Vector3(0, 1, 0).normalized * fuerzaSalto, ForceMode.Force);
        }

        
    }
    private void FixedUpdate()
    {
        if (DetectarSuelo())
        {
            rb.AddForce(new Vector3(-v, 0, h).normalized * fuerza, ForceMode.Force);
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
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstaculo")
        {
            Parpadeo();
            vida--;
        }

    }
    void Parpadeo()
    {
        if (timerParpadeoTotal > 0)
        {
            timerParpadeoTotal -= Time.deltaTime;

            timerParpadeoIntermitente -= Time.deltaTime;
            if (timerParpadeoIntermitente < 0)
            {
                timerParpadeoIntermitente = tiempoParpadeoIntermitente;
            }
            //invertir booleano enabled
            mr.enabled = !mr.enabled;
            if (mr.enabled == true)
            {
                mr.enabled = false;
            }
            else
            {
                mr.enabled = true;
            }
        }
        if (timerParpadeoTotal <= 0)
        {
            mr.enabled = true;
        }
    }

    private bool DetectarSuelo()
    {
        bool resultado = Physics.Raycast(transform.position,new Vector3(0,-3.32f, 0)/*Vector3.down*/, DistanciaRaycast);
        Debug.DrawRay(transform.position, Vector3.down, Color.red, 2f);
        return resultado;
    }
}

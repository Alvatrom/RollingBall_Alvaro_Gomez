using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Vector3 direccion;
    [SerializeField] private float fuerza = 5, fuerzaSalto = 8f;
    [SerializeField] private float DistanciaRaycast = 3.32f;
    Rigidbody rb;
    float h, v;
    int puntuacion;
    [SerializeField] TMP_Text textoPuntuacion;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        
    }
    
    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && DetectarSuelo())
        {
            rb.AddForce(direccion*fuerza,ForceMode.Impulse);
            //rb.AddForce(new Vector3(0, 1, 0).normalized * fuerzaSalto, ForceMode.Force);
        }

        
    }
    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(-v, 0, h).normalized * fuerza, ForceMode.Force);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstaculo"))
        {
            puntuacion += 10;
            //textoPuntuacion.text = "Puntuacion: " + puntuacion;
            //textoPuntuacion.SetText =("Puntuacion: " + puntuacion);
            textoPuntuacion.SetText("Puntuacion: " + puntuacion);
        }
        
    }
    private bool DetectarSuelo()
    {
        bool resultado = Physics.Raycast(transform.position,new Vector3(0,-3.32f, 0)/*Vector3.down*/, DistanciaRaycast);
        Debug.DrawRay(transform.position, Vector3.down, Color.red, 2f);
        return resultado;
    }
}

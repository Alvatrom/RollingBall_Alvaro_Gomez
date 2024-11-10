using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Sierra : MonoBehaviour
{
    [SerializeField] Vector3 direccion;
    [SerializeField] int velocidad;
    [SerializeField] float retardoInicial = 0f;
    float timer = 0;
    bool movimientoIniciado = false;


    void Start()
    {
        StartCoroutine(IniciarMovimientoConRetardo());
    }

    IEnumerator IniciarMovimientoConRetardo()
    {
        yield return new WaitForSeconds(retardoInicial);
        movimientoIniciado = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (movimientoIniciado)
        {
            transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);
            timer += 1 * Time.deltaTime;
            if (timer >= 5f)
            {
                direccion = direccion * -1;
                timer = 0;
            }
        }
    }
}

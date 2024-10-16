using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Coleccionable : MonoBehaviour
{
    [SerializeField] private AudioClip sonidoCling;

    [SerializeField] private AudioManager miManager;



    [SerializeField] Vector3 direccion;
    [SerializeField] Vector3 direccion2;
    //[SerializeField] Quaternion rotation;
    [SerializeField] int velocidad;
    float timer = 0;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;
        transform.Rotate(direccion * velocidad * Time.deltaTime, Space.World);
        transform.Translate(direccion2 * velocidad * Time.deltaTime, Space.World);
        if (timer >= 5f)
        {
            direccion2 = direccion2 * -1;
            timer = 0;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            miManager.ReproducirSonido(sonidoCling);
        }
    }
}

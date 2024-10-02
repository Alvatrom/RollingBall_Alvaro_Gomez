using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour
{
    [SerializeField] Vector3 direccion;
    [SerializeField] int velocidad;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime);
        timer += 1 * Time.deltaTime;
        if(timer >= 5f)
        {
            direccion = direccion * -1;
            timer = 0;
        }
        
        
    }
}

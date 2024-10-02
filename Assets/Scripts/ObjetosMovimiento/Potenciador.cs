using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potenciador : MonoBehaviour
{
    [SerializeField] Vector3 direccion;
    //[SerializeField] Quaternion rotation;
    [SerializeField] int velocidad;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;
        transform.Rotate(direccion * velocidad * Time.deltaTime, Space.World);
        transform.Translate(direccion * velocidad * Time.deltaTime);
        if (timer >= 5f)
        {
            direccion = direccion * -1;
            timer = 0;
        }

    }
}

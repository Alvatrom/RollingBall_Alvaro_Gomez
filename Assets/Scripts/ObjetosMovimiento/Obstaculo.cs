using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    [SerializeField] Vector3 direccion;
    [SerializeField] float fuerza;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddTorque(direccion * fuerza,ForceMode.VelocityChange);
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

    }
}

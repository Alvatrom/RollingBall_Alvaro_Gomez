using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Vector3 direccion;
    [SerializeField] float fuerza = 5;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(direccion*fuerza,ForceMode.Impulse);
        }

        
    }
}

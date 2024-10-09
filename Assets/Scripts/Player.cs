using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Vector3 direccion;
    [SerializeField] float fuerza = 5, fuerzaSalto = 8f;
    Rigidbody rb;
    float h, v;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(direccion*fuerza,ForceMode.Impulse);
            //rb.AddForce(new Vector3(0, 1, 0).normalized * fuerzaSalto, ForceMode.Force);
        }

        
    }
    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(-v, 0, h).normalized * fuerza, ForceMode.Force);
        
    }
}

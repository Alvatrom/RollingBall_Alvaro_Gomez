using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjemploVectores : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //this.transform.position = new Vector3(5,3,0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.position = transform.position + new Vector3(1,0,0)*3* Time.deltaTime;
        }
        
    }
}

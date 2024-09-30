using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionUmbrela : MonoBehaviour
{
    [SerializeField] Vector3 direccion;
    //[SerializeField] Quaternion rotation;
    [SerializeField]  int velocidad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(direccion * velocidad * Time.deltaTime, Space.World);

    }
}

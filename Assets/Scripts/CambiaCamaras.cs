using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiaCamaras : MonoBehaviour
{
    [SerializeField] private GameObject camAApagar;
    [SerializeField] private GameObject camAEncender;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(camAApagar.activeSelf)
                //camApagar ponerla en off.
                camAApagar.SetActive(false);

            //camAEncender ponerla a true
            camAEncender.SetActive(true);
        }
    }
}

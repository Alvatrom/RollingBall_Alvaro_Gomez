using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Cartel : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] string[] contenido;
    [SerializeField] Canvas teclaE;
    [SerializeField] float distanciaLectura;

    private void Start()
    {
        if (teclaE == null)
        {
            teclaE = GameObject.Find("CanvasEstatua")?.GetComponent<Canvas>();
        }
        //Canvas teclaE = GameObject.Find("CanvasEstatua")?.GetComponent<Canvas>();// la interrogacion es para , en el caso que no encuentre el canvas y devuelva un null, no bisque el componente y devuelva un null references
        if (teclaE != null)
        {
            teclaE.enabled = false;
        }
        else
        {
            Debug.LogWarning("No se encontr� un Canvas llamado 'CanvasEstatua'.");
        }
        //teclaE.enabled = false;
    }

    private void Update()
    {
        if (player != null)
        {
            //calcular la distancia
            float distancia = Vector3.Distance(transform.position, player.position);
            teclaE.enabled = distancia < distanciaLectura;

            if (teclaE.enabled && Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.instance.MostrarTexto(contenido);
            }
            else if (!teclaE.enabled)
            {
                DialogueManager.instance.OcultarTexto();
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position + Vector3.up * 2.5f, "Tecla E.gif", true);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, distanciaLectura);
    }
#endif
}

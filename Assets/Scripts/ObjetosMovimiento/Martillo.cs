using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour
{
    [SerializeField] float rotacionAngulo = 77f, rotacionIda = 2f, rotacionVuelta = 2f, tiempoEspera = 2f;
  

    private void Start()
    {
        StartCoroutine(RotateCycle());
    }

    private IEnumerator RotateCycle()//corrutina que permite ejecutar codigo en multiples frames
    {
        while (true)//para qie se ejecute indefinidamente
        {
            // Rotar al angulo deseado en el tiempo estipulado
            yield return StartCoroutine(RotarAlAngulo(rotacionAngulo, rotacionIda));

            yield return new WaitForSeconds(tiempoEspera);

            yield return StartCoroutine(RotarAlAngulo(0, rotacionVuelta));

            yield return new WaitForSeconds(tiempoEspera);
        }
    }

    private IEnumerator RotarAlAngulo(float targetAngle, float duration)
    {
        float startAngle = transform.eulerAngles.x;
        float elapsedTime = 0f;

        while (elapsedTime < duration)//lo hace solo durante el tiempo exacto estipulado
        {
            elapsedTime += Time.deltaTime;
            float newAngle = Mathf.Lerp(startAngle, targetAngle, elapsedTime / duration);//interpola linealmente entre startAngle y targetAngle basándose en el tiempo transcurrido, creando una rotacion suave
            transform.eulerAngles = new Vector3(newAngle, transform.eulerAngles.y, transform.eulerAngles.z);
            yield return null;
        }

        // Asegurar que la rotación finalice exactamente en el ángulo deseado
        transform.eulerAngles = new Vector3(targetAngle, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}

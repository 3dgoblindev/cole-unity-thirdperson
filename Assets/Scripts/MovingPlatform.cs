using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform puntoA;
    [SerializeField] Transform puntoB;
    [SerializeField] float velocidad = 2f;
    [SerializeField] float espera = 1f;

    void Start()
    {
        StartCoroutine(MoveLoop());
    }

    IEnumerator MoveLoop()
    {
        while (true)
        {
            yield return StartCoroutine(MoveTo(puntoA.position));
            yield return new WaitForSeconds(espera);

            yield return StartCoroutine(MoveTo(puntoB.position));
            yield return new WaitForSeconds(espera);
        }
    }

    IEnumerator MoveTo(Vector3 destino)
    {
        while (Vector3.Distance(transform.position, destino) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                destino,
                velocidad * Time.deltaTime
            );

            yield return null;
        }

        transform.position = destino;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
            print("Player on platform");
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
            print("Player off platform");
        }
    }
}
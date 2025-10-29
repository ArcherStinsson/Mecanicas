using UnityEngine;

public class Plataformamovil : MonoBehaviour
{
    public Transform puntoA;
    public Transform puntoB;

    public float velocidad = 1.0f;

    private bool yendoAlPuntoB = true;

    void Update()
    {
        if (yendoAlPuntoB == true)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                puntoB.position,
                velocidad * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, puntoB.position) < 0.1f)
            {
                yendoAlPuntoB = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards( transform.position, puntoA.position, velocidad * Time.deltaTime);

        
            if (Vector3.Distance(transform.position, puntoA.position) < 0.1f)
            {
                yendoAlPuntoB = true;
            }
        }
    }
}
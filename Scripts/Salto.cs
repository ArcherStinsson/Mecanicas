using System.Collections;
using UnityEngine;
using TMPro;

public class Salto : MonoBehaviour
{
    public float thrust = 10.0f;
    private Rigidbody2D rb2D;
    private bool isJumping = false;
    private int noCollisLayer;
    private int platInvLayer;

    public TextMeshProUGUI textoPuntuacion;
    public int puntuacionParaMejora = 50;
    public float aumentoDeSalto = 5.0f;

    private int puntuacionActual = 0;
    private bool mejoraAplicada = false;


    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        noCollisLayer = LayerMask.NameToLayer("NoCollis");
        platInvLayer = LayerMask.NameToLayer("PlatInv");

        ActualizarUI();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb2D.AddForce(transform.up * thrust, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    public void AñadirPuntos(int puntos)
    {
        puntuacionActual += puntos;
        ActualizarUI();

        if (!mejoraAplicada && puntuacionActual >= puntuacionParaMejora)
        {
            mejoraAplicada = true;
            thrust += aumentoDeSalto;
            Debug.Log("POWER UP! Nueva fuerza: " + thrust);
        }
    }

    void ActualizarUI()
    {
        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = "Puntos: " + puntuacionActual;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("suelo") || other.gameObject.CompareTag("Plataforma"))
        {
            isJumping = false;
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, 0f);
        }

        if (other.gameObject.CompareTag("Plataforma"))
        {
            transform.SetParent(other.transform);
        }

        if (other.gameObject.layer == platInvLayer)
        {
            SpriteRenderer sr = other.gameObject.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.enabled = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (!this.isActiveAndEnabled)
        {
            return;
        }

        if (other.gameObject.layer != noCollisLayer)
        {
            Debug.Log("No toca " + other.gameObject.name + "no es 'NoCollis').");

            if (other.gameObject.CompareTag("Plataforma"))
            {
                Debug.Log("voy a desemparentar.");
                StartCoroutine(DelayedUnparent());
            }
        }
        else
        {
            Debug.Log("No toca " + other.gameObject.name + ".'NoCollis', IGNORAR.");
        }
    }

    private IEnumerator DelayedUnparent()
    {
        yield return new WaitForEndOfFrame();
        transform.SetParent(null);
    }
}
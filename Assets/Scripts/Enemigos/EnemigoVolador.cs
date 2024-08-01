using UnityEngine;
using UnityEngine.Events;

public class EnemigoVolador : MonoBehaviour, IDa�o, IRecolectable
{
    public int vida = 3; // Vida del enemigo
    public Da�oEvento onRecibirDa�o; // Evento para recibir da�o
    public RecolectarEvento onRecolectar; // Evento para recolectar (suma de puntos)
    public float velocidadMovimiento = 2.0f; // Velocidad del movimiento de lado a lado
    public float tiempoDeVida = 10.0f; // Tiempo despu�s del cual el enemigo se destruye

    private Rigidbody2D rb;
    private Vector2 direccionMovimiento = Vector2.left; // Direcci�n inicial del movimiento

    private void Start()
    {
        if (onRecibirDa�o == null)
            onRecibirDa�o = new Da�oEvento();

        if (onRecolectar == null)
            onRecolectar = new RecolectarEvento();

        onRecibirDa�o.AddListener(RecibirDa�o);
        onRecolectar.AddListener(Recolectar);

        rb = GetComponent<Rigidbody2D>();

        // Iniciar la destrucci�n despu�s de un tiempo
        Invoke("DestruirEnemigo", tiempoDeVida);
    }

    private void Update()
    {
        // Movimiento horizontal
        rb.velocity = new Vector2(direccionMovimiento.x * velocidadMovimiento, rb.velocity.y);
    }

    // Implementaci�n de IDa�o
    public void RecibirDa�o(int cantidad)
    {
        vida -= cantidad;
        if (vida <= 0)
        {
            // Muerte del enemigo
            Debug.Log("Enemigo volador ha sido derrotado");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Proyectil"))
        {
            onRecibirDa�o.Invoke(1); // Recibir da�o de proyectiles
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemigo volador ha colisionado con el Player, se ha destruido y da�ado al jugador!");
            onRecolectar.Invoke(); // Invocar evento de recolectar al colisionar con el jugador
            Destroy(gameObject); // Destruye el enemigo al colisionar con el jugador
        }
    }

    private void OnBecameInvisible()
    {
        // Si el enemigo sale de la pantalla, cambia la direcci�n
        direccionMovimiento = -direccionMovimiento;
    }

    private void DestruirEnemigo()
    {
        Destroy(gameObject);
    }

    // Implementaci�n de IRecolectable
    public void Recolectar()
    {
        // L�gica para recolectar el enemigo (suma de puntos)
        Debug.Log("Enemigo volador recolectado, se suma un punto.");
        // Aqu� podr�as aumentar el puntaje del jugador o invocar alg�n evento relacionado
    }
}

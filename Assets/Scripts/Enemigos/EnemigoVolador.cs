using UnityEngine;
using UnityEngine.Events;

public class EnemigoVolador : MonoBehaviour, IDaño, IRecolectable
{
    public int vida = 3; // Vida del enemigo
    public DañoEvento onRecibirDaño; // Evento para recibir daño
    public RecolectarEvento onRecolectar; // Evento para recolectar (suma de puntos)
    public float velocidadMovimiento = 2.0f; // Velocidad del movimiento de lado a lado
    public float tiempoDeVida = 10.0f; // Tiempo después del cual el enemigo se destruye

    private Rigidbody2D rb;
    private Vector2 direccionMovimiento = Vector2.left; // Dirección inicial del movimiento

    private void Start()
    {
        if (onRecibirDaño == null)
            onRecibirDaño = new DañoEvento();

        if (onRecolectar == null)
            onRecolectar = new RecolectarEvento();

        onRecibirDaño.AddListener(RecibirDaño);
        onRecolectar.AddListener(Recolectar);

        rb = GetComponent<Rigidbody2D>();

        // Iniciar la destrucción después de un tiempo
        Invoke("DestruirEnemigo", tiempoDeVida);
    }

    private void Update()
    {
        // Movimiento horizontal
        rb.velocity = new Vector2(direccionMovimiento.x * velocidadMovimiento, rb.velocity.y);
    }

    // Implementación de IDaño
    public void RecibirDaño(int cantidad)
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
            onRecibirDaño.Invoke(1); // Recibir daño de proyectiles
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemigo volador ha colisionado con el Player, se ha destruido y dañado al jugador!");
            onRecolectar.Invoke(); // Invocar evento de recolectar al colisionar con el jugador
            Destroy(gameObject); // Destruye el enemigo al colisionar con el jugador
        }
    }

    private void OnBecameInvisible()
    {
        // Si el enemigo sale de la pantalla, cambia la dirección
        direccionMovimiento = -direccionMovimiento;
    }

    private void DestruirEnemigo()
    {
        Destroy(gameObject);
    }

    // Implementación de IRecolectable
    public void Recolectar()
    {
        // Lógica para recolectar el enemigo (suma de puntos)
        Debug.Log("Enemigo volador recolectado, se suma un punto.");
        // Aquí podrías aumentar el puntaje del jugador o invocar algún evento relacionado
    }
}

using UnityEngine;

public class Enemigo : MonoBehaviour, IDaño
{
    public int life = 3;
    public DañoEvento onRecibirDaño;

    private void Start()
    {
        if (onRecibirDaño == null)
            onRecibirDaño = new DañoEvento();

        onRecibirDaño.AddListener(RecibirDaño);
    }

    public void RecibirDaño(int cantidad)
    {
        life -= cantidad;
        if (life <= 0)
        {
            // Muerte del enemigo
            Debug.Log("Enemigo ha muerto");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Proyectil"))
        {
            onRecibirDaño.Invoke(1);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemigo ha colisionado con el Player, se ha destruido y dañado al jugador!");
            Destroy(gameObject); // Destruye el enemigo al colisionar con el jugador
        }
    }
}

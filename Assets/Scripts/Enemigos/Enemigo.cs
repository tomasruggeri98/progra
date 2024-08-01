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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Proyectil"))
        {
            onRecibirDaño.Invoke(1);
        }
    }
}

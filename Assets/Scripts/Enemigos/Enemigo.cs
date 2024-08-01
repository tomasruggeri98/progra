using UnityEngine;

public class Enemigo : MonoBehaviour, IDa�o
{
    public int life = 3;
    public Da�oEvento onRecibirDa�o;

    private void Start()
    {
        if (onRecibirDa�o == null)
            onRecibirDa�o = new Da�oEvento();

        onRecibirDa�o.AddListener(RecibirDa�o);
    }

    public void RecibirDa�o(int cantidad)
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
            onRecibirDa�o.Invoke(1);
        }
    }
}

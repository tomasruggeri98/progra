using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float speed = 10f; // Velocidad del proyectil
    private Vector2 direction; // Direcci�n del proyectil

    // M�todo para establecer la direcci�n del proyectil
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }

    private void Update()
    {
        // Mover el proyectil en la direcci�n establecida
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el proyectil ha chocado con un objeto que tenga la etiqueta "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject); // Destruir el proyectil al chocar con un enemigo
        }
    }
}

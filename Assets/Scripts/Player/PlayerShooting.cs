using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform shootPoint;        // Punto de disparo
    public float projectileSpeed = 10f; // Velocidad del proyectil

    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer del jugador

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Manejar el disparo
        if (Input.GetButtonDown("Fire1")) // "Fire1" es el bot�n izquierdo del mouse por defecto
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instanciar el proyectil en el punto de disparo
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

        // Obtener el Rigidbody2D del proyectil para aplicar la velocidad
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Establecer la velocidad del proyectil en la direcci�n en la que est� mirando el jugador
        // Utilizamos la direcci�n del spriteRenderer para determinar hacia d�nde disparar
        Vector2 direction = spriteRenderer.flipX ? Vector2.left : Vector2.right;
        rb.velocity = direction * projectileSpeed;
    }
}

using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform shootPoint;        // Punto de disparo
    public float projectileSpeed = 10f; // Velocidad del proyectil
    public float shootInterval = 0.3f;  // Intervalo de tiempo entre disparos
    public float projectileLifetime = 3f; // Tiempo de vida del proyectil

    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer del jugador
    private float shootTimer; // Temporizador para el intervalo de disparo

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        shootTimer = 0f; // Inicializar el temporizador de disparo
    }

    void Update()
    {
        // Actualizar el temporizador de disparo
        shootTimer -= Time.deltaTime;

        // Manejar el disparo continuo mientras se mantiene presionado el botón de disparo
        if (Input.GetButton("Fire1")) // "Fire1" es el botón izquierdo del mouse por defecto
        {
            if (shootTimer <= 0f)
            {
                Shoot();
                shootTimer = shootInterval; // Reiniciar el temporizador de disparo
            }
        }
    }

    void Shoot()
    {
        // Instanciar el proyectil en el punto de disparo
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

        // Obtener el Rigidbody2D del proyectil para aplicar la velocidad
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Establecer la velocidad del proyectil en la dirección en la que está mirando el jugador
        // Utilizamos la dirección del spriteRenderer para determinar hacia dónde disparar
        Vector2 direction = spriteRenderer.flipX ? Vector2.left : Vector2.right;
        rb.velocity = direction * projectileSpeed;

        // Destruir el proyectil después de un tiempo
        Destroy(projectile, projectileLifetime);
    }
}

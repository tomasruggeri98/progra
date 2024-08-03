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

        // Manejar el disparo continuo mientras se mantiene presionado el bot�n de disparo
        if (Input.GetButton("Fire1")) // "Fire1" es el bot�n izquierdo del mouse por defecto
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

        // Establecer la velocidad del proyectil en la direcci�n en la que est� mirando el jugador
        // Utilizamos la direcci�n del spriteRenderer para determinar hacia d�nde disparar
        Vector2 direction = spriteRenderer.flipX ? Vector2.left : Vector2.right;
        rb.velocity = direction * projectileSpeed;

        // Destruir el proyectil despu�s de un tiempo
        Destroy(projectile, projectileLifetime);
    }
}

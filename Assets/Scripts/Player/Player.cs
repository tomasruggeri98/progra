using UnityEngine;

public class Player : MonoBehaviour, IDaño
{
    public float speed;
    public float jumpForce;
    public int life = 3;
    public int score = 0;

    public DañoEvento onRecibirDaño;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (onRecibirDaño == null)
            onRecibirDaño = new DañoEvento();

        onRecibirDaño.AddListener(RecibirDaño);
    }

    void Update()
    {
        // Movimiento del jugador
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    public void RecibirDaño(int cantidad)
    {
        life -= cantidad;
        if (life <= 0)
        {
            // Muerte del jugador
            Debug.Log("Player ha muerto");
            // Añade aquí la lógica para la muerte del jugador, como reiniciar el nivel o volver al menú
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IRecolectable>(out IRecolectable recolectable))
        {
            recolectable.Recolectar();
            score++;
        }
        else if (collision.gameObject.TryGetComponent<IDaño>(out IDaño daño))
        {
            onRecibirDaño.Invoke(1);
        }
    }
}

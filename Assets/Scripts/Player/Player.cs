using UnityEngine;

public class Player : MonoBehaviour, IDaño
{
    public float speed;
    public float jumpForce;
    public int life = 3;
    public int score = 0;

    public DañoEvento onRecibirDaño;

    private Rigidbody2D rb;
    private bool isGrounded;
    public Transform groundCheck;        // Punto de chequeo para el suelo
    public float checkRadius = 0.2f;     // Radio del área de chequeo del suelo
    public LayerMask groundLayer;        // Capa del suelo

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (onRecibirDaño == null)
            onRecibirDaño = new DañoEvento();

        onRecibirDaño.AddListener(RecibirDaño);
    }

    private void Update()
    {
        // Comprobar si el jugador está en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        // Movimiento del jugador
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = true;
        }

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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = false;
        }
    }
}

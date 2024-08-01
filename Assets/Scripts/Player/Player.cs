using UnityEngine;

public class Player : MonoBehaviour, IDa�o
{
    public float speed;
    public float jumpForce;
    public int life = 3;
    public int score = 0;

    public Da�oEvento onRecibirDa�o;

    private Rigidbody2D rb;
    private bool isGrounded;
    public Transform groundCheck;        // Punto de chequeo para el suelo
    public float checkRadius = 0.2f;     // Radio del �rea de chequeo del suelo
    public LayerMask groundLayer;        // Capa del suelo

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (onRecibirDa�o == null)
            onRecibirDa�o = new Da�oEvento();

        onRecibirDa�o.AddListener(RecibirDa�o);
    }

    private void Update()
    {
        // Comprobar si el jugador est� en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        // Movimiento del jugador
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    public void RecibirDa�o(int cantidad)
    {
        life -= cantidad;
        Debug.Log($"Player ha recibido da�o. Vida actual: {life}"); // Mensaje en la consola

        if (life <= 0)
        {
            // Muerte del jugador
            Debug.Log("Player ha muerto");
            Destroy(gameObject); // Destruye el jugador
            // A�ade aqu� la l�gica para la muerte del jugador, como reiniciar el nivel o volver al men�
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Moneda"))
        {
            Moneda moneda = collision.gameObject.GetComponent<Moneda>();
            if (moneda != null)
            {
                moneda.Recolectar(); // La moneda se recolecta y se destruye
                IncrementarPuntaje(); // Incrementar el puntaje
            }
        }
        else if (collision.gameObject.TryGetComponent<IDa�o>(out IDa�o da�o))
        {
            onRecibirDa�o.Invoke(1);
        }
        else if (collision.gameObject.TryGetComponent<IRecolectable>(out IRecolectable recolectable))
        {
            recolectable.Recolectar(); // Maneja la recolecci�n
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = false;
        }
    }

    private void IncrementarPuntaje()
    {
        score++;
        Debug.Log($"Puntaje: {score}"); // Feedback en la consola
    }
}

using UnityEngine;

public class Player : MonoBehaviour, IDa�o
{
    public float speed;
    public float jumpForce;
    public int life = 3;
    public int score = 0;

    public Da�oEvento onRecibirDa�o;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (onRecibirDa�o == null)
            onRecibirDa�o = new Da�oEvento();

        onRecibirDa�o.AddListener(RecibirDa�o);
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

    public void RecibirDa�o(int cantidad)
    {
        life -= cantidad;
        if (life <= 0)
        {
            // Muerte del jugador
            Debug.Log("Player ha muerto");
            // A�ade aqu� la l�gica para la muerte del jugador, como reiniciar el nivel o volver al men�
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IRecolectable>(out IRecolectable recolectable))
        {
            recolectable.Recolectar();
            score++;
        }
        else if (collision.gameObject.TryGetComponent<IDa�o>(out IDa�o da�o))
        {
            onRecibirDa�o.Invoke(1);
        }
    }
}

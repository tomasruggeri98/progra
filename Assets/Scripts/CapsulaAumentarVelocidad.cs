using UnityEngine;

public class CapsulaAumentarVelocidad : MonoBehaviour, IRecolectable
{
    public float velocidadExtra = 2.0f; // Cantidad de velocidad que se añadirá

    public RecolectarEvento onRecolectar;

    private bool recolectada = false; // Variable para evitar recolección múltiple

    private void Start()
    {
        if (onRecolectar == null)
            onRecolectar = new RecolectarEvento();
    }

    public void Recolectar()
    {
        if (!recolectada)
        {
            Debug.Log("Capsula de velocidad recolectada. Velocidad aumentada.");
            recolectada = true; // Marca la cápsula como recolectada
            onRecolectar.Invoke(); // Llama a los suscriptores del evento
            ApplyEffect(); // Aplica el efecto de la cápsula
            Destroy(gameObject);  // Destruye la cápsula
        }
    }

    private void ApplyEffect()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.speed += velocidadExtra; // Aumenta la velocidad del jugador
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Recolectar();
        }
    }
}

using UnityEngine;

public class CapsulaReducirGravedad : MonoBehaviour, IRecolectable
{
    public float nuevoGravityScale = 0.7f; // Nuevo gravity scale que se aplicará

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
            Debug.Log("Capsula de gravedad recolectada. Gravedad reducida.");
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
            player.GetComponent<Rigidbody2D>().gravityScale = nuevoGravityScale; // Cambia el gravity scale del jugador
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

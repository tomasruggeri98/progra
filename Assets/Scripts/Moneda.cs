using UnityEngine;

public class Moneda : MonoBehaviour, IRecolectable
{
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
            recolectada = true; // Marca la moneda como recolectada
            onRecolectar.Invoke(); // Llama a los suscriptores del evento
            Debug.Log("Moneda recolectada +1 punto"); // Feedback en la consola
            Destroy(gameObject);  // Destruye la moneda
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Recolectar(); // Llama a Recolectar cuando colisiona con el jugador
        }
    }
}

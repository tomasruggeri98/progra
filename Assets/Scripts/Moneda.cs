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
            Debug.Log("Moneda recolectada +1 punto");
            recolectada = true; // Marca la moneda como recolectada
            onRecolectar.Invoke(); // Llama a los suscriptores del evento
            Destroy(gameObject);  // Destruye la moneda
        }
    }
}

using UnityEngine;

public class BallRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; // Velocidad de rotación en grados por segundo
    public float moveSpeed = 2f;       // Velocidad de movimiento en unidades por segundo
    public float moveDistance = 5f;    // Distancia máxima de movimiento a la izquierda y derecha

    private float initialX;            // Posición X inicial
    private bool movingRight = true;   // Dirección del movimiento

    void Start()
    {
        // Guardar la posición inicial en el eje X
        initialX = transform.position.x;
    }

    void Update()
    {
        // Rotar el objeto alrededor del eje Z a una velocidad constante
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        // Mover el objeto en el eje X de izquierda a derecha
        Vector2 position = transform.position;

        if (movingRight)
        {
            position.x += moveSpeed * Time.deltaTime;
            if (position.x >= initialX + moveDistance)
            {
                movingRight = false;
            }
        }
        else
        {
            position.x -= moveSpeed * Time.deltaTime;
            if (position.x <= initialX - moveDistance)
            {
                movingRight = true;
            }
        }

        transform.position = position;
    }
}

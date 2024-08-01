using UnityEngine;

public class BallRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; // Velocidad de rotación en grados por segundo

    void Update()
    {
        // Rotar el objeto alrededor del eje Z a una velocidad constante
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}

using UnityEngine;

public class Oscilator : MonoBehaviour
{
    public float amplitude = 1f; // Amplitud de la oscilación
    public float frequency = 1f; // Frecuencia de la oscilación

    private float initialZ;

    void Start()
    {
        initialZ = transform.position.z;
    }

    void Update()
    {
        // Oscilar el objeto en el eje Z
        float z = initialZ + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }
}

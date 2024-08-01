using UnityEngine;

public class Proyectil : MonoBehaviour
{
    

    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}

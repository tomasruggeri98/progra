using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Obtener la velocidad del jugador
        float move = Input.GetAxis("Horizontal");

        // Configurar las animaciones seg�n la velocidad
        if (Mathf.Abs(move) > 0.1f)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // Girar el sprite seg�n la direcci�n del movimiento
        if (move > 0)
        {
            spriteRenderer.flipX = false; // Mirando a la derecha
        }
        else if (move < 0)
        {
            spriteRenderer.flipX = true;  // Mirando a la izquierda
        }
    }
}

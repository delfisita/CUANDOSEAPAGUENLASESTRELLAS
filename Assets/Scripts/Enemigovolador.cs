using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;       // Velocidad horizontal
    public float moveHeight = 1.5f;    // Amplitud del movimiento vertical
    public float verticalSpeed = 2f;   // Velocidad del movimiento vertical
    public bool moveRight = false;     // 🔹 Empieza moviéndose hacia la IZQUIERDA

    [Header("Damage Settings")]
    public int damage = 1;             // Daño que hace al jugador

    private Vector3 startPos;
    private float verticalOffset;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        MovePattern();
    }

    private void MovePattern()
    {
        // Movimiento horizontal
        float horizontal = (moveRight ? 1 : -1) * moveSpeed * Time.deltaTime;
        transform.Translate(horizontal, 0, 0);

        // Movimiento vertical tipo “ola”
        verticalOffset += Time.deltaTime * verticalSpeed;
        float newY = startPos.y + Mathf.Sin(verticalOffset) * moveHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si toca al jugador, le causa daño
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si choca con una pared, cambia de dirección
        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Obstacle"))
        {
            moveRight = !moveRight;
            Flip();
        }
    }

    private void Flip()
    {
        // Voltea el sprite cuando cambia de dirección
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}

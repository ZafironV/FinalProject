using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public GameObject projectilePrefab;
    public Transform shootPoint;
    private float lastShootTime;
    public float shootCooldown = 1f;
    public float projectileSpeed = 10f;
    public GameManager gameManager;
    public float projectileDestroyDelay = 5f;
    public Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastShootTime >= shootCooldown)
        {
            Shoot();
            lastShootTime = Time.time;
        }

        anim.SetFloat("horizontal", movement.x);
        anim.SetFloat("vertical", movement.y);
    }

    void FixedUpdate()
    {
        // Normalización del movimiento
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Shoot()
    {
        // Normalización de la dirección de disparo
        Vector2 direction = movement.normalized;

        if (direction.magnitude > 0)
        {
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = direction * projectileSpeed;
            Destroy(projectile, projectileDestroyDelay);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.EnemyTouchLink();
            Destroy(collision.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            gameManager.EnemyTouchLink();
            Destroy(other.gameObject);
        }
    }
}

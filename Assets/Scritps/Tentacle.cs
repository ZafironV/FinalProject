using UnityEngine;

public class Tentacle : Enemy
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        Vector2 movement = direction * speed * Time.deltaTime * 1.5f;
        transform.Translate(movement, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
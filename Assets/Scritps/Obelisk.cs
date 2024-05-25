using UnityEngine;

public class Obelisk : Enemy
{
    public enum ObeliskState
    {
        Idle,
        Moving,
        Attacking
    }

    public float projectileSpeed;
    public GameObject projectilePrefab;
    private float lastShootTime;
    public float shootCooldown = 1f;
    public float projectileDestroyDelay = 5f;
    public float projectileRange = 5f;

    protected override void Update()
    {
        base.Update();

        if (IsPlayerInRange() && Time.time - lastShootTime >= shootCooldown)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    bool IsPlayerInRange()
    {
        return Vector2.Distance(transform.position, player.position) <= projectileRange;
    }

    void Shoot()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;
        Destroy(projectile, projectileDestroyDelay);
    }
}

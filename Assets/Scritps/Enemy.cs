using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    protected Transform player;
    public float detectionRange;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        MoveTowardsPlayer();
    }

    protected virtual void MoveTowardsPlayer()
    {
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

}
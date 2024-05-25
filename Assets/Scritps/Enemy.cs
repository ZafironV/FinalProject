using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Velocidad del enemigo
    public float speed;
    // Rango de detección del jugador
    protected Transform player;
    // Rango de detección para moverse hacia el jugador
    public float detectionRange;

    // Inicialización
    protected virtual void Start()
    {
        // Encuentra al jugador al inicio
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Actualización
    protected virtual void Update()
    {
        // Movimiento hacia el jugador
        MoveTowardsPlayer();
    }

    // Movimiento hacia el jugador
    protected virtual void MoveTowardsPlayer()
    {
        // Si el jugador está dentro del rango de detección, mueve hacia él
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}
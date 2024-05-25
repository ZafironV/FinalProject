using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Velocidad del enemigo
    public float speed;
    // Rango de detecci�n del jugador
    protected Transform player;
    // Rango de detecci�n para moverse hacia el jugador
    public float detectionRange;

    // Inicializaci�n
    protected virtual void Start()
    {
        // Encuentra al jugador al inicio
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Actualizaci�n
    protected virtual void Update()
    {
        // Movimiento hacia el jugador
        MoveTowardsPlayer();
    }

    // Movimiento hacia el jugador
    protected virtual void MoveTowardsPlayer()
    {
        // Si el jugador est� dentro del rango de detecci�n, mueve hacia �l
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}
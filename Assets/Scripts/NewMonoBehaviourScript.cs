using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyChase : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float stoppingDistance = 1.5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Importante: Si no asignas el player manual, lo busca por Tag
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void FixedUpdate() // Usamos FixedUpdate para físicas
    {
        if (player == null) return;

        // Calcular dirección
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Evitar que intente volar o hundirse

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Moverse si está lejos
        if (distanceToPlayer > stoppingDistance)
        {
            // MovePosition es la forma correcta de mover un Rigidbody Kinematic o normal sin teletransportarlo
            Vector3 newPos = rb.position + direction * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPos);
        }

        // Rotar hacia el jugador
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            rb.rotation = Quaternion.RotateTowards(rb.rotation, toRotation, 360 * Time.fixedDeltaTime);
        }
    }
}



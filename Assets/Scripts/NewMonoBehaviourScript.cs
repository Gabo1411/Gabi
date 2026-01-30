using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float stoppingDistance = 1.5f;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > stoppingDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Rotar para mirar al jugador
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}


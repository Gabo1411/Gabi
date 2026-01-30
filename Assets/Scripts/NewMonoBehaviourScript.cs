using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float stoppingDistance = 1.5f;

    [Header("Separation")]
    public float separationRadius = 1.2f;
    public float separationStrength = 2f;

    void Update()
    {
        if (player == null) return;

        // === PERSECUCIÓN (solo X/Z) ===
        Vector3 targetPosition = new Vector3(
            player.position.x,
            transform.position.y,
            player.position.z
        );

        Vector3 moveDirection = Vector3.zero;

        float distanceToPlayer = Vector3.Distance(transform.position, targetPosition);
        if (distanceToPlayer > stoppingDistance)
        {
            moveDirection = (targetPosition - transform.position).normalized;
        }

        // === SEPARACIÓN ENTRE ENEMIGOS ===
        Collider[] nearby = Physics.OverlapSphere(transform.position, separationRadius);
        Vector3 separation = Vector3.zero;

        foreach (Collider col in nearby)
        {
            if (col.gameObject == gameObject) continue;
            if (!col.CompareTag("Enemy")) continue;

            Vector3 diff = transform.position - col.transform.position;
            diff.y = 0;

            float dist = diff.magnitude;
            if (dist > 0.001f)
            {
                separation += diff.normalized / dist;
            }
        }

        // === MOVIMIENTO FINAL ===
        Vector3 finalDirection = moveDirection + separation * separationStrength;
        finalDirection.y = 0;

        if (finalDirection != Vector3.zero)
        {
            transform.position += finalDirection.normalized * speed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(finalDirection);
        }
    }
}




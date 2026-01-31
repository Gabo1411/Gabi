using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // CAMBIO: Usamos FindFirstObjectByType en lugar de FindObjectOfType
        GameManager manager = FindFirstObjectByType<GameManager>();

        if (manager != null)
        {
            manager.EnemyDefeated();
        }

        Destroy(gameObject);
    }
}

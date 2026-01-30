using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public int damage = 1;

    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}


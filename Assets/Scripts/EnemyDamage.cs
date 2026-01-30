using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 10;
    public float damageCooldown = 1f;

    private float nextDamageTime = 0f;

    private void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (Time.time >= nextDamageTime)
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

            if (player != null)
            {
                player.Health -= damage;
                player.Health = Mathf.Max(player.Health, 0);
            }

            nextDamageTime = Time.time + damageCooldown;
        }
    }
}

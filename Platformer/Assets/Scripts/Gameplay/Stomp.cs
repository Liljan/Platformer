using UnityEngine;
using System.Collections;

public class Stomp : MonoBehaviour
{
    public int damage;

    private Player player;

    public void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !player.GetIsGrounded())
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
            player.Jump();
        }
    }

}

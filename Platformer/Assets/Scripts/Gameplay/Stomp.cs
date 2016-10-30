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
        Debug.Log(2);
        if (other.CompareTag("Enemy") && !player.GetIsGrounded())
        {
            Debug.Log(3);
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
            player.Jump();
        }
    }

}

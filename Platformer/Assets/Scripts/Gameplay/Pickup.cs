using UnityEngine;
using System.Collections;

public abstract class Pickup : MonoBehaviour
{
    public abstract void AddEffect();
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AddEffect();
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}

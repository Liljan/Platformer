using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int health = 1;

	void Start () {
	
	}

    public void TakeDamage(int d)
    {
        health -= d;

        if (health <= 0)
            Destroy(gameObject);
    }
}

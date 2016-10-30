using UnityEngine;
using System.Collections;

public abstract class PlayerTrigger : MonoBehaviour {

    protected LevelManager levelManager;

    public void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Trigger();
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public abstract void Trigger();
}

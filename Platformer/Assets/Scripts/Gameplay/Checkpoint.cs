using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    private LevelManager levelManager;

    public void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider2D>().enabled = false;
            levelManager.SetCheckpoint(transform);
        }
    }
}

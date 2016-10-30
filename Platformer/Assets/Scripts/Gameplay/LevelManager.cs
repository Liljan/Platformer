using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    // spawning
    public GameObject PlayerPrefab;
    private GameObject player;
    private Transform currentCheckpoint;
    public Transform startPoint;

    // lives & score
    public int lives = 3;

    public void Awake()
    {
        currentCheckpoint = startPoint;
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        player = Instantiate(PlayerPrefab, currentCheckpoint.position, currentCheckpoint.rotation) as GameObject;
    }

    public void SetCheckpoint(Transform t)
    {
        currentCheckpoint = t;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            SpawnPlayer();
            --lives;
        }
    }
}

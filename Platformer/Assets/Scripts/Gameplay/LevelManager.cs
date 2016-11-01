using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    // spawning
    public GameObject PlayerPrefab;
    private GameObject player;
    private Transform currentCheckpoint;
    public Transform startPoint;

    // score
    private int score = 0;

    // private CameraFollow cmr;

    // lives & score
    public int lives = 3;

    public void Start()
    {
        currentCheckpoint = startPoint;
        SpawnPlayer();
        //  cmr = GameObject.FindObjectOfType<CameraFollow>();
    }

    public void SpawnPlayer()
    {
        player = Instantiate(PlayerPrefab, currentCheckpoint.position, currentCheckpoint.rotation) as GameObject;
        //  cmr.SetPlayer(player.transform);
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

    public void AddScore(int s) { score += s; }
    public void RemoveScore(int s) { score -= s; }
}

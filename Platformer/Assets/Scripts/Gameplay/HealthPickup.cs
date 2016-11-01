using UnityEngine;
using System.Collections;

public class HealthPickup : Pickup {

    private Player player;

    public int health = 1;

    public override void AddEffect()
    {
        player = FindObjectOfType<Player>();
        player.AddHealth(health);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

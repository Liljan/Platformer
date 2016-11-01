using UnityEngine;
using System.Collections;
using System;

public class ScorePickup : Pickup {

    private LevelManager levelManager;

    public int score = 100;

    public override void AddEffect()
    {
        levelManager = FindObjectOfType<LevelManager>();
        levelManager.AddScore(score);
    }

    // Use this for initialization
    void Start () {
	
	}
}

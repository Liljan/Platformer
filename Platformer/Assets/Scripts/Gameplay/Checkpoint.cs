using UnityEngine;
using System.Collections;
using System;

public class Checkpoint : PlayerTrigger {
    public override void Trigger()
    {
        levelManager.SetCheckpoint(transform);

    }

}

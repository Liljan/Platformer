using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Finish : PlayerTrigger {

    public string nextScene;

    public override void Trigger()
    {
        try
        {
            SceneManager.LoadScene(nextScene);
        }
        catch (Exception)
        {

            throw;
        }
        
    }
}

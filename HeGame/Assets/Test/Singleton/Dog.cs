using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HeGame.Hierarchy("Root/Dog", "Dog001")]
public class Dog : HeGame.MonoSingleton<Dog>
{

    // Use this for initialization
    void Start()
    {
        HeGame.Log.Debug("test", "cyan");
        HeGame.Log.Debug("test");
        HeGame.Log.Info("test");
        HeGame.Log.Warning("test");
        HeGame.Log.Error("test");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Say(string message)
    {
        HeGame.Log.Info(message);
    }
}

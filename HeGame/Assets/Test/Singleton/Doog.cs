using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HeGame.Hierarchy("Root/Dog", "Dog002")]
public class Doog : HeGame.MonoSingleton<Doog>
{

    // Use this for initialization
    void Start()
    {

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

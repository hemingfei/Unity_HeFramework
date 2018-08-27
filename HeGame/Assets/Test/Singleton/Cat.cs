using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : HeGame.Singleton<Cat>
{
    private Cat()
    {

    }
    public void Say(string message)
    {
        HeGame.Log.Info(message);
    }
}

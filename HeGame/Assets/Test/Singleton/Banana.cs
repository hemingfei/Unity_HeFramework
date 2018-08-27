using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Cat.Instance.Say("banana let cat say");
        Dog.Instance.Say("banana let mono dog say");
        Doog.Instance.Say("banana let mono doog say");
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Monster
{
    // Start is called before the first frame update
    void Start()
    {
        atkTime = 1f;
        power = 20;
        hp = 150;
    }
}

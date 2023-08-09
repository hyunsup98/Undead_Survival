using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Monster
{
    // Start is called before the first frame update
    void Start()
    {
        atkTime = 1f;
        power = 30;
        hp = 200;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Monster
{
    // Start is called before the first frame update
    void Start()
    {
        atkTime = 1f;
        power = 15;
        hp = 120;
    }
}

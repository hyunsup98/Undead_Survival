using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Monster
{
    // Start is called before the first frame update
    void Start()
    {
        monsterType = 1;
        atkTime = 1f;
        power = 15;
        hp = 120;
        maxHp = hp;
    }
}

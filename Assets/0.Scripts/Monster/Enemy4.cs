using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : Monster
{
    // Start is called before the first frame update
    void Start()
    {
        monsterType = 4;
        atkTime = 1f;
        power = 30;
        hp = 500;
        maxHp = hp;
    }
}

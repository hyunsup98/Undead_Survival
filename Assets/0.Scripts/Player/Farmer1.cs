using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer1 : Player
{
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        farmerName = "Barley";

        HP = MaxHP = 150;
        shieldSpeed = 150;

        Speed = 5.5f;
        BulletFireDelayTime = 1f;       // ÃÑ¾Ë ¹ß»ç µô·¹ÀÌ

        exp = 0;
        maxExp = 100;

        level = 0;
        UI.instance.Level = level + 1;

        gunDamage = 35;

        GunLevel = 0;
        MachineGunLv = 0;
        BombLevel = 0;
        PlusBombDamage = 0;
    }
}

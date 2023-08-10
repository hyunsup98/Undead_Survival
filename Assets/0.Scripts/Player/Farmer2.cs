using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer2 : Player
{
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        farmerName = "Wheat";

        HP = MaxHP = 50;
        shieldSpeed = 50;

        Speed = 2f;
        BulletFireDelayTime = 7f;       // ÃÑ¾Ë ¹ß»ç µô·¹ÀÌ

        exp = 0;
        maxExp = 100;

        level = 0;
        UI.Instance.Level = level + 1;

        gunDamage = 100;

        GunLevel = 0;
        MachineGunLv = 0;
        BombLevel = 1;
        PlusBombDamage = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer0 : Player
{
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        farmerName = "Rice";

        HP = MaxHP = 100;
        shieldSpeed = 100;

        Speed = 3f;
        BulletFireDelayTime = 0.2f;       // ÃÑ¾Ë ¹ß»ç µô·¹ÀÌ

        exp = 0;
        maxExp = 100;

        level = 0;
        UI.Instance.Level = level + 1;

        gunDamage = 35;

        GunLevel = 0;
        MachineGunLv = 0;
        BombLevel = 0;
        PlusBombDamage = 0;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill_Bomb : MonoBehaviour
{
    [SerializeField] GameObject bomb;
    [SerializeField] Animator bombAnim;
    [SerializeField] Player p;

    [SerializeField] Animator exploAnim;
    [SerializeField] GameObject explo;
    [SerializeField] AudioSource exploSound;

    float exploTime = 0;

    protected int bombDmg;

    // Update is called once per frame
    void Update()
    {
        Explosion();
    }

    public void Explosion()
    {
        exploTime += Time.deltaTime;

        if (exploTime > 2)
        {
            exploTime = 0;

            Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, 1.5f);
            foreach (Collider2D coll in colls)
            {
                if (coll.GetComponent<Monster>())
                {
                    coll.GetComponent<Monster>().Hit(0.2f, bombDmg * p.BombLevel + p.PlusBombDamage);
                }
            }
            bombAnim.SetTrigger("explo");
            exploSound.Play();
            ExploAnim();
            Destroy(gameObject, 1f);
        }
    }
    void ExploAnim()
    {
        explo.SetActive(true);
        exploAnim.SetTrigger("explo");
    }
    public void SetP(Player p)
    {
        this.p = p;
    }
}

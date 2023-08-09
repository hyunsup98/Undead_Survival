using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    Player p;

    [SerializeField] SpriteRenderer sr;
    [SerializeField] Animator animator;
    [SerializeField] GameObject[] expPrefab;
    [SerializeField] Transform dmgParent;
    [SerializeField] Damage dmgPrefab;
    [SerializeField] GameObject box;

    protected float atkTime;
    protected int power;
    [HideInInspector] public int hp;

    private float atkTimer = 0f;

    private float hitFreezeTimer;

    // Update is called once per frame
    void Update()
    {
        if (UI.instance.gamestate != GameState.Play)
            return;

        if (p == null || hp <= 0)
            return;

        if (hitFreezeTimer > 0)
        {
            hitFreezeTimer -= Time.deltaTime;
            animator.SetTrigger("hit");
            return;
        }
        else
            animator.SetTrigger("run");

        float x = p.transform.position.x - transform.position.x;

        sr.flipX = (x < 0) ? true : (x == 0) ? true : false;

        float distance = Vector2.Distance(p.transform.position, transform.position);

        if(distance <= 1)
        {
            // 공격
            atkTimer += Time.deltaTime;

            if (atkTimer > atkTime)
            {
                atkTimer = 0;
                p.Hit(power);
            }
        }
        else
        {
            // 이동
            Vector2 v1 = (p.transform.position - transform.position).normalized * Time.deltaTime * 1.5f;
            transform.Translate(v1);
        }
        animator.SetTrigger("run");
    }
    public void SetPlayer(Player p)
    {
        this.p = p;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Shield>())
        {
            Hit(0.5f, 30);
        }else if(collision.GetComponent<Bullet>())
        {
            if(collision.GetComponent<Bullet>().level > 0)
            {
                collision.GetComponent<Bullet>().level--;
            }
            else
            {
                //Destroy(collision.gameObject);
                BulletPool.Instance.TakeBullet(collision.GetComponent<Bullet>());
            }
            Hit(1f, p.gunDamage);
        }
    }
    public void Hit(float freezeTime, int damage)
    {
        hitFreezeTimer = freezeTime;
        hp -= damage;
        AudioManager.instance.Play("hit1");

        Damage d = Instantiate(dmgPrefab, dmgParent);
        d.damage = damage;

        if (hp <= 0)
        {
            Destroy(GetComponent<Rigidbody2D>());
            GetComponent<CapsuleCollider2D>().enabled = false;
            animator.SetTrigger("dead");
            StartCoroutine("CDropExp");

            int rand = Random.Range(0, 100);
            if(rand < 2)        //몬스터 죽일 시 2프로 확률로 템 상자 드랍
            {
                Instantiate(box, gameObject.transform).transform.SetParent(null);
            }
        }
    }

    IEnumerator CDropExp()
    {
        UI.instance.KillCount++;
        int rand = Random.Range(0, 101);

        if(rand < 70)
            Instantiate(expPrefab[0], transform.position, Quaternion.identity);
        else if(rand < 90)
            Instantiate(expPrefab[1], transform.position, Quaternion.identity);
        else
            Instantiate(expPrefab[2], transform.position, Quaternion.identity);

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}


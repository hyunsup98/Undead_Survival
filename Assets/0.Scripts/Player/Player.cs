using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform ShieldPrefab;
    [SerializeField] Transform shieldParent;

    [SerializeField] Transform firePos;
    [SerializeField] Bullet bullet;

    [SerializeField] Skill_Bomb bomb;
    [SerializeField] Animator bombAnim;

    [HideInInspector] public int gunDamage, PlusBombDamage;

    protected Animator animator;
    protected SpriteRenderer sr;

    [HideInInspector] public string farmerName;

    List<Transform> Shields = new List<Transform>();

    private float x, y;
    private int shieldCount;

    protected int shieldSpeed, level;
    protected float exp, maxExp, bulletTimer;

    public int HP { get; set; }
    public int MaxHP { get; set; }
    public float Speed { get; set; }
    public float BulletFireDelayTime { get; set; }
    public int GunLevel { get; set; }
    public int BombLevel { get; set; }
    public int MachineGunLv { get; set; }
    public float Exp
    {
        get { return exp; }
        set
        {
            exp = value;
            UI.instance.SetExp(ref exp, ref maxExp, ref level);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (UI.instance.gamestate != GameState.Play)
            return;

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(x, y, 0f) * Time.deltaTime * Speed);

        if ((x == 0 && y == 0) && HP != 0)
        {
            animator.SetTrigger("Idle");
        }
        else if (HP != 0)
        {
            animator.SetTrigger("Run");
        }

        if (x != 0)
        {
            sr.flipX = (x < 0) ? true : false;  // flip => 스프라이트 이미지를 X축 방향으로 반대로 돌리는 것, y는 y축 방향
        }

        shieldParent.Rotate(Vector3.back * Time.deltaTime * shieldSpeed);

        Monster[] monsters = FindObjectsOfType<Monster>();
        Box[] boxes = FindObjectsOfType<Box>();

        bulletTimer += Time.deltaTime;
        if (bulletTimer > BulletFireDelayTime)
        {
            // 박스를 우선적으로 타격
            if (boxes.Length > 0)
            {
                //박스가 있을 경우 박스 타격
                if (isShortDistanceBox(boxes))
                {
                    BoxAttack(boxes);
                }
                //박스의 거리가 멀리 있을 경우 몬스터 타격
                else
                {
                    ShortDistanceAttackMonster(monsters);
                }
            }
            else
            {
                ShortDistanceAttackMonster(monsters);
            }
            bulletTimer = 0f;
        }
    }
    public void Hit(int damage)
    {
        if (UI.instance.gamestate != GameState.Play)
            return;

        HP -= damage;
        UI.instance.SetHp(HP, MaxHP);

        if (HP <= 0)
        {
            UI.instance.gamestate = GameState.Stop;
            animator.SetTrigger("Dead");

            Invoke("Dead", 2f);
        }
    }
    void ShortDistanceAttackMonster(Monster[] monsters)
    {
        if (monsters.Length > 0)
        {
            float minDistance = 6f;
            Monster monster = null;
            foreach (Monster m in monsters)
            {
                float distance = Vector3.Distance(transform.position, m.transform.position);

                if (minDistance > distance && m.hp > 0)
                {
                    minDistance = distance;
                    monster = m;
                    // atkMonsterList.Add(m);
                }
            }
            /*
             * 범위 안에 있는 적을 타격
            if (atkMonsterList.Count > 0)
            {
                Monster m = atkMonsterList[Random.Range(0, atkMonsterList.Count)];

                Vector2 vec = transform.position - m.transform.position;
                float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
                firePos.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);

                Bullet b = Instantiate(bullet, firePos);
                b.transform.SetParent(null);        // firePos의 각도를 가져오기위해 부모로 두었다 뺌
                b.level = GunLevel;
            }
            */
            // 거리가 가장 가까운 적을 먼저 타격
            if (monster != null)
            {
                Vector2 vec = transform.position - monster.transform.position;
                float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
                firePos.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);

                BulletPool.Instance.Create(transform.position, firePos);
                /*
                Bullet b = Instantiate(bullet, firePos);
                b.transform.SetParent(null);        // firePos의 각도를 가져오기위해 부모로 두었다 뺌
                b.level = GunLevel;
                */
            }
        }
    }

    bool isShortDistanceBox(Box[] boxes)
    {
        float minDistance = 2f;
        Box box = null;
        foreach (var item in boxes)
        {
            float distance = Vector3.Distance(transform.position, item.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                box = item;
            }
        }

        return box == null ? false : true;
    }
    void BoxAttack(Box[] boxes)
    {
        float minDistance = 2f;
        Box box = null;
        foreach (var item in boxes)
        {
            float distance = Vector3.Distance(transform.position, item.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                box = item;
            }
        }
        Vector2 vec = transform.position - box.transform.position;
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        firePos.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);

        BulletPool.Instance.Create(transform.position, firePos);
        /*
        Bullet b = Instantiate(bullet, firePos);
        b.transform.SetParent(null);        // firePos의 각도를 가져오기위해 부모로 두었다 뺌
        b.level = 0;
        */
    }
    void Dead()
    {
        UI.instance.ShowDeadPopUp(level + 1);
    }
    public void Shield()
    {
        // 보호삽 스킬 전부 끔
        for (int i = 0; i < Shields.Count; i++)
        {
            Shields[i].gameObject.SetActive(false);
        }

        float z = 360 / shieldCount;
        for (int i = 0; i < shieldCount; i++)
        {
            Shields[i].gameObject.SetActive(true);

            Shields[i].rotation = Quaternion.Euler(0, 0, z * i);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Item>())
        {
            collision.GetComponent<Item>().isPickUp = true;
        }
    }

    public void AddShield()
    {
        Shields.Add(Instantiate(ShieldPrefab, shieldParent));
        shieldCount++;
        Shield();
    }
    
    public void LevelUpAbility(string name)
    {
        switch(name)
        {
            case "Rice":
                gunDamage++;
                break;
            case "Barley":
                Speed += Speed * 0.05f;
                break;
            case "Wheat":
                PlusBombDamage += 10;
                break;
            case "Corn":
                bulletTimer -= bulletTimer * 0.05f;
                break;
        }
    }
}

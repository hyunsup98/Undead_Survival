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
            sr.flipX = (x < 0) ? true : false;  // flip => ��������Ʈ �̹����� X�� �������� �ݴ�� ������ ��, y�� y�� ����
        }

        shieldParent.Rotate(Vector3.back * Time.deltaTime * shieldSpeed);

        Monster[] monsters = FindObjectsOfType<Monster>();
        Box[] boxes = FindObjectsOfType<Box>();

        bulletTimer += Time.deltaTime;
        if (bulletTimer > BulletFireDelayTime)
        {
            // �ڽ��� �켱������ Ÿ��
            if (boxes.Length > 0)
            {
                //�ڽ��� ���� ��� �ڽ� Ÿ��
                if (isShortDistanceBox(boxes))
                {
                    BoxAttack(boxes);
                }
                //�ڽ��� �Ÿ��� �ָ� ���� ��� ���� Ÿ��
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
             * ���� �ȿ� �ִ� ���� Ÿ��
            if (atkMonsterList.Count > 0)
            {
                Monster m = atkMonsterList[Random.Range(0, atkMonsterList.Count)];

                Vector2 vec = transform.position - m.transform.position;
                float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
                firePos.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);

                Bullet b = Instantiate(bullet, firePos);
                b.transform.SetParent(null);        // firePos�� ������ ������������ �θ�� �ξ��� ��
                b.level = GunLevel;
            }
            */
            // �Ÿ��� ���� ����� ���� ���� Ÿ��
            if (monster != null)
            {
                Vector2 vec = transform.position - monster.transform.position;
                float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
                firePos.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);

                BulletPool.Instance.Create(transform.position, firePos);
                /*
                Bullet b = Instantiate(bullet, firePos);
                b.transform.SetParent(null);        // firePos�� ������ ������������ �θ�� �ξ��� ��
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
        b.transform.SetParent(null);        // firePos�� ������ ������������ �θ�� �ξ��� ��
        b.level = 0;
        */
    }
    void Dead()
    {
        UI.instance.ShowDeadPopUp(level + 1);
    }
    public void Shield()
    {
        // ��ȣ�� ��ų ���� ��
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

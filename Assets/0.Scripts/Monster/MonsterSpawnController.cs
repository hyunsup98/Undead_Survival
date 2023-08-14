using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterSpawnController : Singleton<MonsterSpawnController>
{
    [SerializeField] Monster[] monsters;
    [SerializeField] Transform parent;

    [SerializeField] MonsterSpawn[] monsterSpawns;

    [SerializeField] Box box;

    private Queue<Monster> m = new Queue<Monster>();

    Player p;

    int rand_M;

    private void Start()
    {
        foreach(var mon in monsterSpawns)
        {
            mon.SetData(monsters, parent);
        }

        InvokeRepeating("SpawnBox", 5f, 25f);

        p = GameManager.Instance.p;
    }

    public void Clear()
    {
        m.Clear();
    }

    public void Create(Vector2 v)
    {
        Monster mon = null;
        SetMonster();
        if (m.Count == 0)
        {
            mon = Instantiate(monsters[rand_M]);
        }
        else
        {
            mon = m.Dequeue();
            mon.gameObject.SetActive(true);
            Debug.Log(mon.hp);
        }
        mon.transform.SetParent(parent);
        mon.transform.position = v;
        mon.SetPlayer(p);
    }

    public void TakeMonster(Monster monster)
    {
        monster.AddComponent<Rigidbody2D>().gravityScale = 0;
        monster.GetComponent<CapsuleCollider2D>().enabled = true;
        monster.hp = monster.maxHp;
        monster.gameObject.SetActive(false);
        m.Enqueue(monster);
    }

    void SetMonster()
    {
        int randSpawnCount = 0;
        if (monsters.Length > (UI.Instance.KillCount / 100))
        {
            randSpawnCount = (UI.Instance.KillCount / 100);
        }
        else
        {
            randSpawnCount = monsters.Length;
        }
        rand_M = Random.Range(0, randSpawnCount);
    }

    void SpawnBox()
    {
        int rand = Random.Range(0, monsterSpawns.Length);
        monsterSpawns[rand].SetBox(box);
    }
}

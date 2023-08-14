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

    Queue<Monster> m = new Queue<Monster>();

    List<Queue<Monster>> listM = new List<Queue<Monster>>();

    Player p;

    int rand_M;

    private void Start()
    {
        foreach(var mon in monsterSpawns)
        {
            mon.SetData(monsters, parent);
        }

        foreach(var item in monsters)
        {
            Queue<Monster> m = new Queue<Monster>();
            listM.Add(m);
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
        if (listM[rand_M].Count == 0)
        {
            mon = Instantiate(monsters[rand_M]);
        }
        else
        {
            mon = listM[rand_M].Dequeue();
            mon.gameObject.SetActive(true);
        }
        mon.transform.SetParent(parent);
        mon.transform.position = v;
        mon.SetPlayer(p);
    }

    public void TakeMonster(Monster monster, int monsterType)
    {
        monster.AddComponent<Rigidbody2D>().gravityScale = 0;
        monster.GetComponent<CapsuleCollider2D>().enabled = true;
        monster.hp = monster.maxHp;
        monster.gameObject.SetActive(false);

        listM[monster.monsterType].Enqueue(monster);
    }

    void SetMonster()
    {
        int randSpawnCount = 0;
        if (monsters.Length > (UI.Instance.KillCount / 10))
        {
            randSpawnCount = (UI.Instance.KillCount / 10);
        }
        else
        {
            randSpawnCount = monsters.Length;
        }
        rand_M = Random.Range(0, randSpawnCount + 1);
    }

    void SpawnBox()
    {
        int rand = Random.Range(0, monsterSpawns.Length);
        monsterSpawns[rand].SetBox(box);
    }
}

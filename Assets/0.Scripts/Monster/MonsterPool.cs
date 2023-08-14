using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterPool : Singleton<MonsterPool>
{
    [SerializeField] Monster monster;
    [SerializeField] Transform parent;

    private Queue<Monster> m = new Queue<Monster>();

    public void Clear()
    {
        m.Clear();
    }

    public void Create(Vector2 v)
    {
        Monster mon = null;
        if(m.Count == 0)
        {
            mon = Instantiate(monster);
        }
        else
        {
            mon = m.Dequeue();
            mon.gameObject.SetActive(true);
        }
        mon.transform.SetParent(parent);
        mon.transform.position = v;
    }

    public void TakeMonster(Monster monster)
    {
        monster.gameObject.SetActive(false);
        m.Enqueue(monster);
    }
}

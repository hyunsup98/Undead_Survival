using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : Singleton<BulletPool>
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform parent;

    Player p;
    private Queue<Bullet> q = new Queue<Bullet>();

    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.p;
    }

    public void Clear()
    {
        q.Clear();
    }

    public void Create(Vector3 startPos, Transform firePos)
    {
        Bullet b = null;
        if (q.Count == 0)
        {
            b = Instantiate(bullet);
        }
        else
        {
            b = q.Dequeue();
            b.gameObject.SetActive(true);
        }
        b.transform.rotation = firePos.rotation;
        b.transform.position = startPos;
        b.level = p.GunLevel;
        b.transform.SetParent(parent);
    }

    public void TakeBullet(Bullet b)
    {
        b.CancelInvoke("End");
        b.gameObject.SetActive(false);
        q.Enqueue(b);
    }
}

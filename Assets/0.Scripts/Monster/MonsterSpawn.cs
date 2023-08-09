using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    Monster[] monsters;
    BoxCollider2D boxCollider;
    Player p;
    Transform parent;

    Box box;

    float spawnTimer;
    float spawnDelayTime;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UI.instance.gamestate != GameState.Play)
            return;

        if (p == null && GameManager.Instance != null)
        {
            p = GameManager.Instance.p;
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnDelayTime)
        {
            spawnTimer = 0;
            CreateMonster();

            spawnDelayTime = 7f;
            spawnDelayTime -= UI.instance.gameLevel;
        }

        if(box != null)
        {
            Vector2 v = RandomPosition(boxCollider);

            Instantiate(box, v, Quaternion.identity);
            transform.SetParent(null);

            box = null;
        }
    }
    public void SetData(Monster[] monsters, Transform parent)
    {
        this.monsters = monsters;
        this.parent = parent;
        spawnDelayTime = 0f;
        spawnTimer = float.MaxValue;
    }
    void CreateMonster()
    {
        Vector2 v = RandomPosition(boxCollider);

        int randSpawnCount = 0;
        if (monsters.Length > (UI.instance.KillCount / 100))
        {
            randSpawnCount = (UI.instance.KillCount / 100);
        }
        else
        {
            randSpawnCount = monsters.Length;
        }
        int rand_M = Random.Range(0, randSpawnCount);
        Monster m = Instantiate(monsters[rand_M], v, Quaternion.identity);
        m.SetPlayer(p);
        m.transform.SetParent(parent);
    }

    Vector2 RandomPosition(BoxCollider2D boxColl)
    {
        Vector2 pos = boxColl.transform.position;

        Vector2 range = new Vector2(boxColl.bounds.size.x, boxColl.bounds.size.y);

        range.x = Random.Range((range.x / 2) * -1, range.x / 2);
        range.y = Random.Range((range.y / 2) * -1, range.y / 2);

        return pos + range;
    }

    public void SetBox(Box box)
    {
        this.box = box;
    }
}

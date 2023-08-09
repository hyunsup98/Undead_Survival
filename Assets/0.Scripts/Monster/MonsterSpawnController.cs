using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnController : MonoBehaviour
{

    [SerializeField] Monster[] monsters;
    [SerializeField] Transform parent;

    [SerializeField] MonsterSpawn[] monsterSpawns;

    [SerializeField] Box box;

    private void Start()
    {
        foreach(var mon in monsterSpawns)
        {
            mon.SetData(monsters, parent);
        }

        InvokeRepeating("SpawnBox", 5f, 25f);
    }
    void SpawnBox()
    {
        int rand = Random.Range(0, monsterSpawns.Length);
        monsterSpawns[rand].SetBox(box);
    }
}

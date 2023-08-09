using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCreate : MonoBehaviour
{
    [SerializeField] Transform SpawnPos;
    [SerializeField] Skill_Bomb bomb;
    [SerializeField] Player p;

    float SpawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        SpawnTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (p == null && GameManager.Instance != null)
        {
            p = GameManager.Instance.p;
            SpawnPos = p.transform;
        }

        if (p.BombLevel == 0)
            return;

        SpawnTimer += Time.deltaTime;
        if(SpawnTimer > 5)
        {
            SpawnTimer = 0;

            Skill_Bomb b = Instantiate(bomb, SpawnPos);
            b.SetP(p);
            b.transform.SetParent(null);
        }
    }
}

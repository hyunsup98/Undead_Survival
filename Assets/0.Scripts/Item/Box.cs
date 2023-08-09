using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] GameObject[] items;

    public float HP { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        HP = 50;
    }
    public void Hit(float damage)
    {
        HP -= damage;

        if(HP <= 0)
        {
            int rand = Random.Range(0, items.Length);
            Instantiate(items[rand], transform.position, Quaternion.identity);  // 내가 있는 자리에 생성하는 것
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Bullet>())
        {
            Hit(15);
            Destroy(collision.gameObject);
        }
    }
}

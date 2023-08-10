using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public int level;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("End", 3f);
        //Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (UI.Instance.gamestate != GameState.Play)
            return;

        transform.Translate(Vector3.right * Time.deltaTime * 10f);
    }
    public void End()
    {
        BulletPool.Instance.TakeBullet(this);
    }
}

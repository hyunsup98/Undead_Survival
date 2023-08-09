using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] Transform target;

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            //Vector3 vec3 = new Vector3(target.position.x, target.position.y, -10f);
            Vector3 v1 = target.position;
            v1.z = -10f;
            transform.position = Vector3.Lerp(transform.position, v1, Time.deltaTime * 50f);
        }
        else
        {
            if(GameManager.Instance != null)
            {
                target = GameManager.Instance.p.transform;
            }
        }
    }
}

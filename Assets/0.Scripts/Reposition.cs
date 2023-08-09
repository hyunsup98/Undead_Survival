using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.Instance.p.transform.position;
        Vector3 myPos = transform.position;

        float diffX = playerPos.x - myPos.x;
        float diffY = playerPos.y - myPos.y;
        float dirX = diffX < 0 ? -1 : 1;
        float dirY = diffY < 0 ? -1 : 1;
        diffX = Mathf.Round(Mathf.Abs(diffX));
        diffY = Mathf.Round(Mathf.Abs(diffY));

        if (diffX > diffY)
            transform.Translate(dirX * 40, 0, 0);
        else if (diffY > diffX)
            transform.Translate(0, dirY * 40, 0);
        else
            transform.Translate(dirX * 40, dirY * 40, 0);
    }
}

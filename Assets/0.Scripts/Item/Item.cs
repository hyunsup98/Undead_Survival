using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //�� ������ ��ũ��Ʈ���� Ÿ�Ը� �����ָ� �� ��ũ��Ʈ�� �������� �Լ��� �� ������.
    public enum Type
    {
        Exp,
        Mag,
        Health
    }

    public bool isPickUp = false;
    public Player p;
    public Transform target;

    protected Type type = Type.Exp;
    protected float exp = 10;

    // Update is called once per frame
    void Update()
    {
        if (UI.instance.gamestate != GameState.Play)
            return;

        if (p == null)
            p = GameManager.Instance.p;
        if (isPickUp)
        {
            float distance = Vector3.Distance(transform.position, p.transform.position);
            Vector3 vec = Vector3.zero;
            transform.position = Vector3.SmoothDamp(transform.position, p.transform.position, ref vec, Time.deltaTime * 10f); // Lerp(�� ������, �̵��� Ÿ�� ������, ���ǵ�)
            switch (type)
            {
                case Type.Exp:
                    Exp(distance);
                    break;
                case Type.Health:
                    Health(distance);
                    break;
                case Type.Mag:
                    Magnet(distance);
                    break;
            }
        }
    }
    void Exp(float distance)
    {
        if (distance < 1f)
        {
            p.Exp += exp;
            Destroy(gameObject);
        }
    }
    void Health(float distance)
    {
        if (distance < 1f)
        {
            p.HP = p.MaxHP;
            p.Hit(0);
            Destroy(gameObject);
        }
    }
    void Magnet(float distance)
    {
        if (distance < 1f)
        {
            Item[] items = FindObjectsOfType<Item>();
            foreach (var item in items)
            {
                item.isPickUp = true;
            }
            Destroy(gameObject);
        }
    }
}

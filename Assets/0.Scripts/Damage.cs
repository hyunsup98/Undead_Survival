using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] TMP_Text damageText;
    Color alpha;

    float txtSpeed;             // 데미지 텍스트 출력시 속도
    float alphaSpeed;
    public int damage { get; set; }          // 출력할 데미지 값

    // Start is called before the first frame update
    void Start()
    {
        txtSpeed = 1.3f;
        alphaSpeed = 2f;

        damageText.text = damage.ToString();

        alpha = damageText.color;

        Invoke("DestroyObject", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, txtSpeed * Time.deltaTime, 0));

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        damageText.color = alpha;
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}

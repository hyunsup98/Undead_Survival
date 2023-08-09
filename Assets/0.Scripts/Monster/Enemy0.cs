using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy0 : Monster
{
    // Start is called before the first frame update
    void Start()
    {
        atkTime = 1f;
        power = 10;
        hp = 100;
    }
    // 상속받는 쪽은 start 함수에서 데이터 정해주고
    // 상속하는 쪽은 update 함수에서 실행 내용을 정해준다
}

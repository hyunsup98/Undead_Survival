using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//싱글톤을 해주고 싶은 클래스에서 MonoBehaviour 자리에 Singleton<클래스 이름>을 해주면 싱글톤 패턴으로 설정된다
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour   //T는 모든 자료형을 뜻함함, 많이 쓰면 부하가 심함
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
            }
            return instance;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [System.Serializable]
    public class MyClip
    {
        public string name;
        public AudioClip audioClip;
    }

    [SerializeField] List<MyClip> myClips;      // 딕셔너리는 인스펙터에 노출이 안돼 리스트를 사용함

    [SerializeField] AudioSource audioSource;
    void Awake()
    {
        instance = this;
    }

    public void Play(string name)
    {
        foreach(var item in myClips)
        {
            if(item.name == name)
            {
                audioSource.clip = item.audioClip;
                audioSource.Play();
                break;
            }
        }
    }
}

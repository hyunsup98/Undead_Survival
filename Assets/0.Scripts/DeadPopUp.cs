using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeadPopUp : MonoBehaviour
{
    [SerializeField] TMP_Text monsterCountTxt;
    [SerializeField] TMP_Text levelTxt;
    [SerializeField] TMP_Text timeTxt;

    public void SetUI(int monsterCnt, int level, System.TimeSpan timespan)
    {
        monsterCountTxt.text = $"���� ���� ��: {monsterCnt} ����";
        levelTxt.text = $"����: {level}";

        string str = $"��� �ð�: ";
        if (timespan.Hours > 0)
            str += $"{timespan.Hours}�� ";

        if(timespan.Minutes > 0)
            str += $"{timespan.Minutes}�� ";

        str += $"{timespan.Seconds}�� ";     // ��, ��, �ʸ� �־��ִ� �ڵ�

        timeTxt.text = str;
    }

    public void OnOk()
    {
        SceneManager.LoadScene("Main");
    }
}

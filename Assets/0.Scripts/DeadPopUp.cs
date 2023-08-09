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
        monsterCountTxt.text = $"잡은 몬스터 수: {monsterCnt} 마리";
        levelTxt.text = $"레벨: {level}";

        string str = $"경과 시간: ";
        if (timespan.Hours > 0)
            str += $"{timespan.Hours}시 ";

        if(timespan.Minutes > 0)
            str += $"{timespan.Minutes}분 ";

        str += $"{timespan.Seconds}초 ";     // 시, 분, 초를 넣어주는 코드

        timeTxt.text = str;
    }

    public void OnOk()
    {
        SceneManager.LoadScene("Main");
    }
}

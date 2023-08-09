using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Option : MonoBehaviour
{
    public static Option instance;

    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource fxSource;
    [SerializeField] TMP_Dropdown dropDown;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider fxSlider;
    [SerializeField] TMP_Text bgmTxt;
    [SerializeField] TMP_Text fxTxt;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        bgmSource.volume = bgmSlider.value;
        fxSource.volume = fxSlider.value;

        string[] strSize = { "1920x1080", "1440x1080", "1280x720", "1176x664", "720x576", "720x480"};
        List<TMP_Dropdown.OptionData> odList = new List<TMP_Dropdown.OptionData>();
        foreach (string item in strSize)
        {
            TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData();
            data.text = item;
            odList.Add(data);
        }
        dropDown.options = odList;
    }

    public void OnEnable() // 게임 오브젝트 켜질 때 자동으로 작동
    {
        UI.instance.gamestate = GameState.Pause;
    }

    public void OnDisable() // 게임 오브젝트 꺼질 때 자동으로 작동
    {
        UI.instance.gamestate = GameState.Play;
    }

    public void OnBGMValueChange(Slider slider)
    {
        bgmSource.volume = slider.value;
        bgmTxt.text = $"배경음:{(int)(slider.value * 100)}%";
    }

    public void OnFxValueChange(Slider slider)
    {
        fxSource.volume = slider.value;
        fxTxt.text = $"효과음:{(int)(slider.value * 100)}%";
    }

    public void OnDropdownChange(TMP_Dropdown dd)
    {
        string sizeTxt = dropDown.options[dd.value].text;
        string[] size = sizeTxt.Split('x');
        Screen.SetResolution(int.Parse(size[0]), int.Parse(size[1]), false);
    }
}

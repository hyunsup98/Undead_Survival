using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum GameState
{
    Play,
    Pause,
    Stop,
}

[System.Serializable]
public class UpgradeData
{
    public Sprite sprite;
    public string title;
    public string desc1;
    public string desc2;
}

[System.Serializable]
public class UpgradeUI
{
    public Image icon;
    public TMP_Text levelTxt;
    public TMP_Text title;
    public TMP_Text desc1;
    public TMP_Text desc2;
}
public class UI : Singleton<UI>
{
    [HideInInspector] public GameState gamestate = GameState.Stop;

    [SerializeField] UpgradeData[] upData;
    [SerializeField] UpgradeUI[] upUI;
    [SerializeField] public DeadPopUp deadPopUp;
    [SerializeField] Bullet b;
    [SerializeField] Skill_Bomb bomb;
    [SerializeField] RectTransform canvas;    // 0 위 / 1 아래 / 2 왼쪽 / 3 오른쪽
    [SerializeField] Transform levelupPopup;
    [SerializeField] BoxCollider2D[] boxColls;
    [SerializeField] Slider sliderExp;
    [SerializeField] Text txtTime;
    [SerializeField] Text txtKillcount;
    [SerializeField] Text txtLv;
    [SerializeField] Image hpImg;

    Player p;
    List<UpgradeData> upgradeDatas = new List<UpgradeData>();

    float timer;

    public int gameLevel;

    int killCount = 0;


    public void SetExp(ref float exp, ref float maxExp, ref int level)
    {
        sliderExp.value = exp / maxExp;

        if (exp > maxExp)
        {
            SetUpgradeData();
            gamestate = GameState.Pause;
            levelupPopup.gameObject.SetActive(true);
            Level = (++level) + 1;
            AudioManager.instance.Play("levelup");
            maxExp += 150;
            sliderExp.value = 0f;
            exp = 0;
            p.LevelUpAbility(p.farmerName);
        }
    }
    public int KillCount
    {
        get { return killCount; }
        set
        {
            killCount = value;
            txtKillcount.text = string.Format("{0:000}", KillCount);    // 킬카운트를 001, 002 이런 식으로 표기하는 법 => 1. $"{KillCount.ToString("000")}"; 2. {0:000} = {0:D3} 도 같은 코드
        }
    }
    public int Level
    {
        set
        {
            txtLv.text = $"Lv.{value}";
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        OnGameStart();

        sliderExp.value = 0f;

        for (int i = 0; i < boxColls.Length; i++)
        {
            Vector2 v1 = canvas.sizeDelta;

            if (i < 2)
                v1.y = 5;
            else
                v1.x = 5;
            boxColls[i].size = v1;
        }

        levelupPopup.gameObject.SetActive(false);
    }
    void Update()
    {
        if(p == null && GameManager.Instance != null)
        {
            p = GameManager.Instance.p;
        }

        if(Input.GetKeyDown(KeyCode.F5))
        {
            gamestate = GameState.Play;
        }

        if (gamestate != GameState.Play)
            return;

        timer += Time.deltaTime;
        System.TimeSpan ts = System.TimeSpan.FromSeconds(timer);
        txtTime.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);

        gameLevel = Mathf.Min(Mathf.FloorToInt(timer / 60), 5);
    }
    public void SetHp(int HP, int maxHP)
    {
        hpImg.fillAmount = (float)HP / maxHP;
    }

    public void OnGameStart()
    {
        gamestate = GameState.Play;
    }
    void SetUpgradeData()
    {
        List<UpgradeData> datas = new List<UpgradeData>();
        for (int i = 0; i < upData.Length; i++)
        {
            UpgradeData ud = new UpgradeData();
            ud.sprite = upData[i].sprite;
            ud.title = upData[i].title;
            ud.desc1 = upData[i].desc1;
            ud.desc2 = upData[i].desc2;
            datas.Add(ud);
        }

        upgradeDatas = new List<UpgradeData>();
        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, datas.Count);
            UpgradeData ud = new UpgradeData();
            ud.sprite = datas[rand].sprite;
            ud.title = datas[rand].title;
            ud.desc1 = datas[rand].desc1;
            ud.desc2 = datas[rand].desc2;
            upgradeDatas.Add(ud);
            datas.RemoveAt(rand);
        }

        for (int i = 0; i < upgradeDatas.Count; i++)
        {
            upUI[i].icon.sprite = upgradeDatas[i].sprite;
            upUI[i].title.text = upgradeDatas[i].title;
            upUI[i].desc1.text = upgradeDatas[i].desc1;
            upUI[i].desc2.text = upgradeDatas[i].desc2;
        }
    }
    public void OnUpgrade(int index)
    {
        switch(upgradeDatas[index].sprite.name)
        {
            case "Select 0":
                p.AddShield();
                break;
            case "Select 3":
                p.GunLevel++;
                break;
            case "Select 6":
                p.BulletFireDelayTime -= p.BulletFireDelayTime * 0.1f;
                break;
            case "Select 7":
                p.Speed += 1f;
                break;
            case "Select 8":
                p.HP = p.MaxHP;
                SetHp(p.HP, p.MaxHP);
                break;
            case "Weapon 4":
                p.MachineGunLv++;
                break;
            case "Box 0":
                p.BombLevel++;
                break;
        }
    }
    public void ShowDeadPopUp(int level)
    {
        deadPopUp.gameObject.SetActive(true);
        System.TimeSpan ts = System.TimeSpan.FromSeconds(timer);
        deadPopUp.SetUI(KillCount, level, ts);
    }
}

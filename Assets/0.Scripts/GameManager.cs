using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] Player[] farmers;
    [HideInInspector] public int playerIndex;

    public Player p;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void CreateFarmer()
    {
        p = Instantiate(farmers[playerIndex]);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterChoice : MonoBehaviour
{
    public void OnChoice(int index)
    {
        GameManager.Instance.playerIndex = index;

        SceneManager.LoadScene("Game");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour
{
    private void Awake()
    {
        if(GameManager.Instance == null)
        {
            SceneManager.LoadScene("Main");
            return;
        }

        GameManager.Instance.CreateFarmer();
    }
}

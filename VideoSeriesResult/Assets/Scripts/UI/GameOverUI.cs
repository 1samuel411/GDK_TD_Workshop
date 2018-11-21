using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{

    public static GameOverUI instance;

    void Awake()
    {
        instance = this;
    }

    public void Show()
    {
        transform.position = new Vector3(Screen.width/2, Screen.height/2, 0);
    }

    public void Close()
    {
        transform.position = Vector3.up * 5000;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanelUI : MonoBehaviour
{

    public static GameOverPanelUI instance;

    private void Awake()
    {
        instance = this;
        Exit();
    }

    public void Show()
    {
        AudioManager.instance.PlayAudio(AudioManager.PlayType.Effects, "UIOpen");
        transform.position = new Vector2(Screen.width/2, Screen.height/2);
    }

    public void Exit()
    {
        transform.position = Vector3.up * 1000;
    }

    public void Restart()
    {
        // Delete BaseManager
        Destroy(BaseManager.instance.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

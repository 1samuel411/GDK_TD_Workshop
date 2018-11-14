using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelUI : MonoBehaviour
{

    // Static instance
    public static InfoPanelUI instance;

    // Public Variables
    public Text typeText;
    public Text timerText;
    public Image typeImage;
    public Sprite dmgIcon, moneyIcon;

    // Private Variables
    private TurretUI curTurret;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        // ---------------- Added this second part
        if(curTurret == null || BuildManager.instance.editMode)
        {
            // move off the screen
            transform.position = Vector3.up * 10000;
            return;
        }

        // Move to the object
        transform.position = curTurret.transform.position;
    }

    void UpdateUI()
    {
        switch(curTurret.turret.type)
        {
            case TurretType.Damage:
                typeImage.sprite = dmgIcon;
                break;
            case TurretType.MoneyBoost:
                typeImage.sprite = moneyIcon;
                break;
        }

        typeText.text = curTurret.turret.value.ToString();
        timerText.text = curTurret.turret.rate.ToString() + "s";
    }

    public void Show(TurretUI turret)
    {
        curTurret = turret;
        AudioManager.instance.PlayAudio(AudioManager.PlayType.Effects, "UIHover");
        UpdateUI();
    }

    public void Exit()
    {
        curTurret = null;
    }

}

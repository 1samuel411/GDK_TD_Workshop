using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretPanelUI : MonoBehaviour
{

    // Static instance
    public static TurretPanelUI instance;

    // Public Variables
    public Text headerText;
    public Text upgradeTimerText;
    public Text upgradeTypeText;
    public Text timerText;
    public Text typeText;
    public Image typeImage;
    public Sprite damageSprite;
    public Sprite moneySprite;
    public Text destroyText;
    public Text upgradeText;

    [HideInInspector]
    public Turret turret;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (turret == null)
            return;

        UpdateUI();
    }

    void UpdateUI()
    {
        switch(turret.type)
        {
            case TurretType.Damage:
                typeImage.sprite = damageSprite;
                break;
            case TurretType.MoneyBoost:
                typeImage.sprite = moneySprite;
                break;
        }
        transform.position = Camera.main.WorldToScreenPoint(turret.transform.position);

        headerText.text = turret.name;
        timerText.text = turret.rate + "s";
        typeText.text = turret.value.ToString();

        if (turret.curUpgrade + 1 <= turret.upgrades.Length - 1)
        {
            upgradeTimerText.text = turret.upgrades[turret.curUpgrade + 1].newRate.ToString();
            upgradeTypeText.text = turret.upgrades[turret.curUpgrade + 1].newValue.ToString();
        }
        else
        {
            upgradeTimerText.text = "";
            upgradeTypeText.text = "";
        }
        destroyText.text = "Destroy ($" + turret.GetDestructionValue() + ")";
        upgradeText.text = "Upgrade ($" + turret.GetUpgradeCost() + ")";
    }

    public void Upgrade()
    {
        turret.Upgrade();
    }

    public void Destroy()
    {
        turret.Destroy();
        turret = null;
        transform.position = Vector3.up * 1000;
    }

    public void Exit()
    {
        turret = null;
        transform.position = Vector3.up * 1000;
        AudioManager.instance.PlayAudio(AudioManager.PlayType.Effects, "UIClose");
    }

    public void Show(Turret newTurret)
    {
        turret = newTurret;
        AudioManager.instance.PlayAudio(AudioManager.PlayType.Effects, "UIOpen");
    }
}

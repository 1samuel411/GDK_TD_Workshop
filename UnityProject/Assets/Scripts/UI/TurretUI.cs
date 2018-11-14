using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretUI : MonoBehaviour
{

    // Public Variables
    public Image icon;
    public new Text name;
    public Text price;

    public Turret turret;

    private void Update()
    {
        if (turret != null)
            UpdateUI();
    }

    void UpdateUI()
    {
        icon.sprite = turret.icon;
        name.text = turret.name;
        price.text = turret.price.ToString();
    }

    public void OnPointerEnter()
    {
        InfoPanelUI.instance.Show(this);
    }

    public void OnPointerExit()
    {
        InfoPanelUI.instance.Exit();
    }

    public void OnPointerClick()
    {
        if (PlayerManager.instance.CanAfford(turret.price) == false)
        {
            AudioManager.instance.PlayAudio(AudioManager.PlayType.Effects, "CantAfford");
            return;
        }
        InfoPanelUI.instance.Exit();
        BuildManager.instance.EnterBuildMode(turret);
    }

}

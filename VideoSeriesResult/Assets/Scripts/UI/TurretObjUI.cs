using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretObjUI : MonoBehaviour
{

    public Text nameText;
    public Text priceText;
    public Image iconImage;

    [HideInInspector]
    public Turret turret;

    private void Update()
    {
        nameText.text = turret.turretName;
        priceText.text = turret.price.ToString();
        iconImage.sprite = turret.icon;
    }

    public void OnClick()
    {
        BuildManager.instance.BuildTurret(turret);
    }

    public void OnHover()
    {
        TurretInfoUI.instance.Show(this);
    }

    public void OnExitHover()
    {
        TurretInfoUI.instance.Close();
    }

}

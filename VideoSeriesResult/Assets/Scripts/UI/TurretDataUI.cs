using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretDataUI : MonoBehaviour
{

    public static TurretDataUI instance;

    public Text nameText;
    public Text radiusText;
    public Text radiusUpgradeText;
    public Text valueText;
    public Text valueUpgradeText;
    public Text rateText;
    public Text rateUpgradeText;

    public Text destroyButton;
    public Text upgradeButton;

    [HideInInspector]
    public Turret turret;

    private GameObject disk;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (turret == null)
        {
            if (disk != null)
                disk.gameObject.SetActive(false);

            transform.position = Vector3.up * 5000;
            return;
        }
        disk = turret.disk;

        if (turret.disk != null)
            turret.disk.gameObject.SetActive(true);

        nameText.text = turret.turretName;
        radiusText.text = turret.dist.ToString();
        valueText.text = turret.value.ToString();
        rateText.text = turret.rate.ToString();
        rateUpgradeText.text = "";

        destroyButton.text = "Destroy (" + turret.GetTurretPrice() + ")";

        if (turret.curUpgrade > turret.upgrades.Length - 2)
        {
            radiusUpgradeText.text = "";
            valueUpgradeText.text = "";
            rateUpgradeText.text = "";
            upgradeButton.text = "Upgrade (Max)";
        }
        else
        {
            Turret.UpgradeStruct nextUpgrade = turret.upgrades[turret.curUpgrade + 1];
            radiusUpgradeText.text = nextUpgrade.newDist.ToString();
            valueUpgradeText.text = nextUpgrade.newValue.ToString();
            rateUpgradeText.text = nextUpgrade.newRate.ToString();
            upgradeButton.text = "Upgrade (" + nextUpgrade.price + ")";
        }
        transform.position = Camera.main.WorldToScreenPoint(turret.transform.position);
    }

    public void Destroy()
    {
        turret.Destroy();
    }

    public void Upgrade()
    {
        turret.Upgrade();
    }

    public void Show(Turret newTurret)
    {
        turret = newTurret;
        transform.position = Camera.main.WorldToScreenPoint(newTurret.transform.position);
        AudioManager.instance.PlayClip(AudioManager.Effects.effects, "UIShow");
    }

    public void Close()
    {
        turret = null;
        transform.position = Vector3.up * 5000;
        AudioManager.instance.PlayClip(AudioManager.Effects.effects, "UIClose");
    }

}

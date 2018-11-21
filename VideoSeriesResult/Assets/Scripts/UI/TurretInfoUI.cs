using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretInfoUI : MonoBehaviour
{

    public static TurretInfoUI instance;

    private void Awake()
    {
        instance = this;
    }

    public Text radiusText;
    public Text valueText;
    public Text rateText;

    [HideInInspector]
    public Turret turret;

    private void Update()
    {
        if (turret == null)
            return;

        radiusText.text = turret.radius.ToString();
        valueText.text = turret.value.ToString();
        rateText.text = turret.rate.ToString();
    }

    public void Show(TurretObjUI obj)
    {
        transform.position = obj.transform.position;
        turret = obj.turret;
        AudioManager.instance.PlayClip(AudioManager.Effects.quiet, "UIShow");
    }

    public void Close()
    {
        turret = null;
        transform.position = Vector3.up * 5000;
        AudioManager.instance.PlayClip(AudioManager.Effects.quiet, "UIClose");
    }

}

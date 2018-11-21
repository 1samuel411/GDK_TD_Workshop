using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelUI : MonoBehaviour
{

    public Text moneyText;
    public Text waveText;

    private void Update()
    {
        moneyText.text = "$" + PlayerManager.instance.money.ToString();
        waveText.text = "Wave: " + WaveManager.instance.wave.ToString();
    }

}

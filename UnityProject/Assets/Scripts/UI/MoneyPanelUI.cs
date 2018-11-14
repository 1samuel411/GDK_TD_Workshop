using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyPanelUI : MonoBehaviour
{

    public Text moneyText;
    public Text waveText;

    private void Update()
    {
        moneyText.text = "$" + PlayerManager.instance.GetMoney().ToString();

        waveText.text = "Wave: " + Spawner.instance.wave;
    }

}

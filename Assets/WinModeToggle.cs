using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinModeToggle : MonoBehaviour
{
    [SerializeField] private WinTypes winType = WinTypes.P1WIN;
    [SerializeField] private HUDController hud;

    public void OnValueChanged(Toggle toggle)
    {
        if(toggle.isOn == false) return;

        hud.SetWinMode(winType);
    }
}

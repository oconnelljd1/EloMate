using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum WinTypes {P1WIN, DRAW, P2WIN}
public class HUDController : MonoBehaviour
{

    [SerializeField] private TMP_Text _p1In;
    [SerializeField] private TMP_Text _p2In;
    [SerializeField] private TMP_Text _kFactor;
    [SerializeField] private TMP_Text _p1Out;
    [SerializeField] private TMP_Text _p2Out;

    private WinTypes winType;

    void Start()
    {
        _p1In.text = "1000";
        _p2In.text = "1100";
        _kFactor.text = "32";

        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        // int p1In, p2In, kFactor;
        int goodValues = 0;
        if(int.TryParse(_p1In.text, out int p1In)) goodValues++;
        if(int.TryParse(_p2In.text, out int p2In)) goodValues++;
        if(int.TryParse(_kFactor.text, out int kFactor)) goodValues++;
        if(goodValues != 3)
        {
            Debug.Log($"<color=orange>goodValues: {goodValues}</color>");
            Debug.Log($"<color=orange>_p1In: {_p1In.text}, _p2In: {_p2In.text}, _kFactor: {_kFactor.text}</color>");
            Debug.Log($"<color=orange>p1In: {p1In}, p2In: {p2In}, kFactor: {kFactor}</color>");
            BadValues();
            return;
        }

        (int p1, int p2) outValues;
        switch(winType)
        {
            case WinTypes.P1WIN:
                outValues = EloMate.CalculateWin(p1In, p2In, kFactor);
                break;
            case WinTypes.DRAW:
                outValues = EloMate.CalculateDraw(p1In, p2In, kFactor);
                break;
            case WinTypes.P2WIN:
                outValues = EloMate.CalculateWin(p2In, p1In, kFactor);
                break;
            default:
                Debug.Log($"<color=orange>Unexpected WinType {winType}</color>");
                BadValues();
                return;
        }

        _p1Out.text = $"{outValues.p1}";
        _p2Out.text = $"{outValues.p2}";
    }

    private void BadValues()
    {
        _p1Out.text = "N/A";
        _p2Out.text = "N/A";
    }

    public void SetWinMode(WinTypes type)
    {
        winType = type;
        UpdateDisplay();
    }
}
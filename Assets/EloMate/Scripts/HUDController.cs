using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static RedHeadToolz.Utils.EloMate;

namespace EloMate
{
    public enum WinTypes { P1WIN, DRAW, P2WIN }

    public class HUDController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _p1In;
        [SerializeField] private TMP_InputField _p2In;
        [SerializeField] private TMP_InputField _kFactor;
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
            int goodValues = 0;
            if (int.TryParse(_p1In.text, out int p1In)) goodValues++;
            if (int.TryParse(_p2In.text, out int p2In)) goodValues++;
            if (int.TryParse(_kFactor.text, out int kFactor)) goodValues++;
            if (goodValues != 3)
            {
                BadValues();
                return;
            }

            (int p1, int p2) outValues;
            switch (winType)
            {
                case WinTypes.P1WIN:
                    outValues = CalculateWin(p1In, p2In, kFactor);
                    _p1Out.text = $"{outValues.p1}";
                    _p2Out.text = $"{outValues.p2}";
                    break;
                case WinTypes.DRAW:
                    outValues = CalculateDraw(p1In, p2In, kFactor);
                    _p1Out.text = $"{outValues.p1}";
                    _p2Out.text = $"{outValues.p2}";
                    break;
                case WinTypes.P2WIN:
                    outValues = CalculateWin(p2In, p1In, kFactor);
                    // important to flip outvalues
                    _p1Out.text = $"{outValues.p2}";
                    _p2Out.text = $"{outValues.p1}";
                    break;
                default:
                    Debug.Log($"<color=orange>Unexpected WinType {winType}</color>");
                    BadValues();
                    return;
            }
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
}
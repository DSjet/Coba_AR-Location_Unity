using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapsUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _subText;
    [SerializeField] TextMeshProUGUI _mainText;

    private bool _isRouting = false;

    public void UpdateCanvasRouting(string subText, string mainText)
    {
        _subText.text = subText;
        _mainText.text = mainText;
    }
}

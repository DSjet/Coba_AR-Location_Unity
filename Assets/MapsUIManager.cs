using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapsUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _subText;
    [SerializeField] TextMeshProUGUI _mainText;
    [SerializeField] TextMeshProUGUI _distanceText;

    private bool _isRouting = false;

    private void Update()
    {
        if (_isRouting)
        {
            
        }
    }

    public void UpdateCanvasRouting(string subText, string mainText)
    {
        _subText.text = subText;
        _mainText.text = mainText;
        _isRouting = true;
    }


}

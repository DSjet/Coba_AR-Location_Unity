using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocationInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _locationName;
    [SerializeField] private TextMeshProUGUI _locationDescription;
    [SerializeField] private Image _locationImage;
    
    
    public void UpdateInfoUI(LocationPointScriptableObject locationPoint)
    {
        _locationName.text = locationPoint.locationName;
        _locationDescription.text = locationPoint.locationDescription;
        _locationImage.sprite = locationPoint.locationImage; 
    }
}

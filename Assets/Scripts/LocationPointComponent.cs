using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocationPointComponent : Button
{
    [SerializeField] TextMeshProUGUI _locationName;
    [SerializeField] TextMeshProUGUI _locationDescription;
    [SerializeField] TextMeshProUGUI _locationType;

    public void SetLocationPointComponent(LocationPointScriptableObject _locationPoint)
    {
        _locationName.text = _locationPoint.locationName;
        _locationDescription.text = _locationPoint.locationDescription;
        _locationType.text = _locationPoint.locationType;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocationPointComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _locationName;
    [SerializeField] private TextMeshProUGUI _locationType;
    // [SerializeField] private TextMeshProUGUI _locationDescription;

    public void SetLocationPointComponent(LocationPointScriptableObject _locationPoint)
    {
        _locationName.text = _locationPoint.locationName;
        _locationType.text = _locationPoint.locationType;
        // _locationDescription.text = _locationPoint.locationDescription;
    }
}

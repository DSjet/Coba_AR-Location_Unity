using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LocationData", menuName = "ScriptableObjects/LocationPointScriptableObject", order = 1)]
public class LocationPointScriptableObject : ScriptableObject
{
    public string locationName;
    public string locationQuery;
    public string locationType;
    public string locationDescription;
}

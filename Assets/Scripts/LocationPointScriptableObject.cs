using ARLocation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "LocationData", menuName = "LocationPoints/LocationPointScriptableObject", order = 1)]
public class LocationPointScriptableObject : ScriptableObject
{
    public string locationName;
    public string locationQuery;
    public string locationType;
    public string locationDescription;
    public Location location;
    public Sprite locationImage;
}

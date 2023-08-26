using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public struct CanvasStates
    {
        public const string _onSelectLocation = "Select_Location";
        public const string _onNavigation = "On_Navigation";
        public const string _onAbout = "On_About";
    }

    internal CanvasStates canvasState;

    [Header("Canvas")]
    [Tooltip("Place the select location canvas here")]
    public GameObject selectLocation;

    [Tooltip("Place the navigation canvas here")]
    public GameObject navigation;

    [Tooltip("Place the about canvas here")]
    public GameObject about;


    public void ChangeCanvasState(string canvas)
    {
        selectLocation.SetActive(canvas == CanvasStates._onSelectLocation);
        navigation.SetActive(canvas == CanvasStates._onNavigation);
        about.SetActive(canvas == CanvasStates._onAbout);
    }
}

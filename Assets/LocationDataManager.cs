using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.ARFoundation;
using Unity.VisualScripting;
using static UnityEngine.XR.ARSubsystems.XRCpuImage;

namespace ARLocation.MapboxRoutes
{
    public class LocationDataManager : MonoBehaviour
    {
        [SerializeField] MapboxRoute _mapboxRoute;
        [SerializeField] GameObject _locationContainer; // location select
        [SerializeField] List<LocationPointScriptableObject> _locationPoints;
        [SerializeField] GameObject _arSession;
        [SerializeField] GameObject _arSessionOrigin;
        [SerializeField] GameObject _camera;
        [SerializeField] string _mapboxToken;
        [SerializeField] GameObject _routeContainer;
        [SerializeField] LocationPointComponent _locationPointPrefab;
        [SerializeField] Transform _locationPointContainer; // grid container 
        [SerializeField] GameObject _mapsUI;
        [SerializeField] LocationInfoUI _locationInfoUI;

        Location _placeOfInterest;

        private List<GeocodingFeature> _geocodingFeatureResult;

        // -7.770427972195095, 110.37760708835364

        void Start()
        {
            foreach (var locationPoint in _locationPoints)
            {
                LocationPointComponent locationPointComponent = Instantiate(_locationPointPrefab, _locationPointContainer);
                locationPointComponent.SetLocationPointComponent(locationPoint);
                //StartCoroutine(LocationQuery(locationPoint.locationQuery));
                //locationPoint.location = _placeOfInterest;
                locationPointComponent.GetComponent<Button>().onClick.AddListener(() => StartRoute(locationPoint.location));
                locationPointComponent.GetComponent<Button>().onClick.AddListener(() => _locationInfoUI.UpdateInfoUI(locationPoint));
            }
        }

        public void StartRoute(Location destPoint)
        {
            _placeOfInterest = destPoint;
            if (ARLocationProvider.Instance.IsEnabled)
            {
                LoadRoute(_placeOfInterest);
            }
            else
            {
                ARLocationProvider.Instance.OnEnabled.AddListener(LoadRoute);
            }
        }

        public void EndRoute()
        {
            ARLocationProvider.Instance.OnEnabled.RemoveListener(LoadRoute);
            _mapsUI.SetActive(false);
            _arSession.SetActive(false);
            _arSessionOrigin.SetActive(false);
            _routeContainer.SetActive(false);
            _camera.gameObject.SetActive(true);
        }

        private void LoadRoute(Location _)
        {
            if (_placeOfInterest != null)
            {
                // Building Route
                var lang = _mapboxRoute.Settings.Language;
                var api = new MapboxApi(_mapboxToken, lang);
                var loader = new RouteLoader(api, true);
                StartCoroutine(
                        loader.LoadRoute(
                            new RouteWaypoint { Type = RouteWaypointType.UserLocation },
                            new RouteWaypoint { Type = RouteWaypointType.Location, Location = _placeOfInterest },
                            (err, res) =>
                            {
                                if (err != null)
                                {
                                    Debug.LogError(err);
                                    return;
                                }
                                _locationContainer.SetActive(false);
                                _arSession.SetActive(true);
                                _arSessionOrigin.SetActive(true);
                                _routeContainer.SetActive(true);
                                _camera.gameObject.SetActive(false);
                                _mapsUI.SetActive(true);
                                _mapboxRoute.BuildRoute(res);
                            }));
            }
        }
    }
}


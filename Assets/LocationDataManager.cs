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
            }
        }

        private IEnumerator LocationQuery(string queryRequest)
        {
            MapboxApi mapboxApi = new MapboxApi(_mapboxToken);
            yield return mapboxApi.QueryLocal(queryRequest, true);
            Debug.Log("Clicked");

            if (mapboxApi.ErrorMessage != null)
            {
                Debug.LogError(mapboxApi.ErrorMessage);
                _geocodingFeatureResult = new List<GeocodingFeature>();
            }
            else
            {
                _geocodingFeatureResult = mapboxApi.QueryLocalResult.features;
                foreach (var feature in _geocodingFeatureResult)
                {
                    Debug.Log(feature.geometry.coordinates[0]);
                }
                _placeOfInterest = _geocodingFeatureResult[0].geometry.coordinates[0];
            }
        }

        public void StartRoute(Location dest)
        {
            _placeOfInterest = dest;
            if (ARLocationProvider.Instance.IsEnabled)
            {
                _mapsUI.SetActive(true);
                //gameObject.SetActive(false);
                LoadRoute(dest);
                // ARLocationProvider.Instance.CurrentLocation.ToLocation(),
            }
            else
            {
                ARLocationProvider.Instance.OnEnabled.AddListener(LoadRoute);
            }
        }

        public void EndRoute()
        {
            //ARLocationProvider.Instance.OnEnabled.RemoveListener(LoadRoute);
            _arSession.SetActive(false);
            _arSessionOrigin.SetActive(false);
            //RouteContainer.SetActive(false);
            _camera.gameObject.SetActive(true);
            //s.View = View.SearchMenu;
        }

        private void LoadRoute(Location _)
        {
            if (_placeOfInterest != null)
            {
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
                                    // s.Results = new List<GeocodingFeature>();
                                    return;
                                }
                                _locationContainer.SetActive(false);
                                _arSession.SetActive(true);
                                _arSessionOrigin.SetActive(true);
                                _routeContainer.SetActive(true);
                                _camera.gameObject.SetActive(false);
                                //s.View = View.Route;
                                _mapboxRoute.BuildRoute(res);
                            }));
            }
        }
    }
}


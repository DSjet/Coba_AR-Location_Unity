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
        [SerializeField] List<LocationPointScriptableObject> _locationPoints;
        [SerializeField] GameObject _arSession;
        [SerializeField] GameObject _arSessionOrigin;
        [SerializeField] GameObject _camera;
        [SerializeField] Location _destination;
        [SerializeField] string _mapboxToken;

        [SerializeField] LocationPointComponent _locationPointPrefab;
        [SerializeField] Transform _locationPointContainer;
        [SerializeField] GameObject _mapsUI;

        private List<GeocodingFeature> _geocodingFeatureResult;

        void Start()
        {
            foreach (var locationPoint in _locationPoints)
            {
                LocationPointComponent locationPointComponent = Instantiate(_locationPointPrefab, _locationPointContainer);
                locationPointComponent.SetLocationPointComponent(locationPoint);
                locationPointComponent.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(StartRouting(locationPoint.locationQuery)));
            }
        }

        private IEnumerator StartRouting(string queryRequest)
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
                Location _placeOfInterest = _geocodingFeatureResult[0].geometry.coordinates[0];
                StartRoute(_placeOfInterest);
                _mapsUI.SetActive(true);
                gameObject.SetActive(false);
            }
        }

        public void StartRoute(Location dest)
        {
            if (ARLocationProvider.Instance.IsEnabled)
            {
                loadRoute(dest);
                // ARLocationProvider.Instance.CurrentLocation.ToLocation(),
            }
            //else
            //{
            //    ARLocationProvider.Instance.OnEnabled.AddListener(loadRoute());
            //}
        }

        public void EndRoute()
        {
            //ARLocationProvider.Instance.OnEnabled.RemoveListener(loadRoute);
            _arSession.SetActive(false);
            _arSessionOrigin.SetActive(false);
            //RouteContainer.SetActive(false);
            _camera.gameObject.SetActive(true);
            //s.View = View.SearchMenu;
        }

        private void loadRoute(Location destination)
        {
            if (destination != null)
            {
                var lang = _mapboxRoute.Settings.Language;
                var api = new MapboxApi(_mapboxToken, lang);
                var loader = new RouteLoader(api, true);
                StartCoroutine(
                        loader.LoadRoute(
                            new RouteWaypoint { Type = RouteWaypointType.UserLocation },
                            new RouteWaypoint { Type = RouteWaypointType.Location, Location = destination },
                            (err, res) =>
                            {
                                if (err != null)
                                {
                                    Debug.LogError(err);
                                    // s.Results = new List<GeocodingFeature>();
                                    return;
                                }

                                _arSession.SetActive(true);
                                _arSessionOrigin.SetActive(true);
                                //RouteContainer.SetActive(true);
                                _camera.gameObject.SetActive(false);
                                //s.View = View.Route;
                                _mapboxRoute.BuildRoute(res);
                            }));
            }
        }
    }
}


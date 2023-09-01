using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.ARFoundation;
using Unity.VisualScripting;

namespace ARLocation.MapboxRoutes
{
    public class LocationDataManager : MonoBehaviour
    {
        [SerializeField] MapboxRoute _mapboxRoute;
        [SerializeField] List<LocationPointScriptableObject> _locationPoints;

        [SerializeField] LocationPointComponent locationPointPrefab;
        [SerializeField] Transform locationPointContainer;
        [SerializeField] GameObject mapsUI;

        // Start is called before the first frame update
        void Start()
        {
            foreach (var locationPoint in _locationPoints)
            {
                LocationPointComponent locationPointComponent = Instantiate(locationPointPrefab, locationPointContainer);
                locationPointComponent.SetLocationPointComponent(locationPoint);
                locationPointComponent.GetComponent<Button>().onClick.AddListener(() => StartRouting(locationPoint.locationQuery));
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartRouting(string queryRequest)
        {
            MapboxApi mapbox = new MapboxApi("eyJ1IjoiYW5hbWFsb2Nhcmlzc3MiLCJhIjoiY2xreThyYXgxMWVjcDNtcHJ6emY1a3QzaCJ9");
            RouteWaypoint _newRoute = new RouteWaypoint { Type = RouteWaypointType.Query };
            RouteLoader routeLoader = new RouteLoader(mapbox);
            routeLoader.LoadRoute(new RouteWaypoint { Type = RouteWaypointType.UserLocation }, _newRoute);
            mapsUI.SetActive(true);
            gameObject.SetActive(false);
        }

        //private void loadRoute(Location _)
        //{
        //    if (s.destination != null)
        //    {
        //        var lang = _mapboxRoute.Settings.Language;
        //        var api = new MapboxApi("eyJ1IjoiYW5hbWFsb2Nhcmlzc3MiLCJhIjoiY2xreThyYXgxMWVjcDNtcHJ6emY1a3QzaCJ9", lang);
        //        var loader = new RouteLoader(api);
        //        StartCoroutine(
        //                loader.LoadRoute(
        //                    new RouteWaypoint { Type = RouteWaypointType.UserLocation },
        //                    new RouteWaypoint { Type = RouteWaypointType.Location, Location = s.destination },
        //                    (err, res) =>
        //                    {
        //                        if (err != null)
        //                        {
        //                            s.ErrorMessage = err;
        //                            s.Results = new List<GeocodingFeature>();
        //                            return;
        //                        }
        //                        _mapboxRoute.RoutePathRenderer = currentPathRenderer;
        //                        _mapboxRoute.BuildRoute(res);
        //                    }));
        //    }
        //}
    }
}


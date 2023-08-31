using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ARLocation.MapboxRoutes
{
    public class LocationDataManager : MonoBehaviour
    {
        [SerializeField] MapboxRoute _mapboxRoute;
        [SerializeField] List<LocationPointScriptableObject> _locationPoints;

        [SerializeField] LocationPointComponent locationPointPrefab;
        [SerializeField] Transform locationPointContainer;

        // Start is called before the first frame update
        void Start()
        {
            foreach (var locationPoint in _locationPoints)
            {
                LocationPointComponent locationPointComponent = Instantiate(locationPointPrefab, locationPointContainer);
                locationPointComponent.SetLocationPointComponent(locationPoint);
                locationPointComponent.onClick.AddListener(() => StartRouting(locationPoint.locationQuery));
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartRouting(string queryRequest)
        {
            RouteWaypoint _newRoute = new RouteWaypoint { Type = RouteWaypointType.Query };
            _newRoute.Query = queryRequest;
            _mapboxRoute.LoadRoute(_newRoute);
        }
    }
}


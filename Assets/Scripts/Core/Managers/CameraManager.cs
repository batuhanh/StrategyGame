using StrategyGame.MVC;
using StrategyGame.MVC.Views;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace StrategyGame.Core.Managers
{
    public class CameraManager : MonoBehaviour
    {
        public bool IsInitialized { get { return _isInitialized; } }
        [SerializeField] private Camera cam;
        private Vector3 dragOrigin;
        private float mapMinX, mapMinY, mapMaxX, mapMaxY;
        private bool _isInitialized = false;
        private void Initiliaze()
        {
            Renderer gridRenderer = GameGrid.Instance.GameGridView.GridBgRenderer;
            mapMinX = gridRenderer.transform.position.x - (gridRenderer.bounds.size.x / 2f);
            mapMaxX = gridRenderer.transform.position.x + (gridRenderer.bounds.size.x / 2f);

            mapMinY = gridRenderer.transform.position.y - (gridRenderer.bounds.size.y / 2f);
            mapMaxY = gridRenderer.transform.position.y + (gridRenderer.bounds.size.y / 2f);
            _isInitialized = true;
        }
        private void Update()
        {
            //if (!_isInitialized)
                //Initiliaze();

            if (Input.GetMouseButtonDown(0))
            {
                dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 diff = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
                cam.transform.position = ClampCamera(cam.transform.position + diff);
            }
        }
        private Vector3 ClampCamera(Vector3 targetPosition)
        {
            float newX = Mathf.Clamp(targetPosition.x, mapMinX , mapMaxX );
            float newY = Mathf.Clamp(targetPosition.y, mapMinY , mapMaxY );

            return new Vector3(newX, newY, targetPosition.z);
        }
    }
}

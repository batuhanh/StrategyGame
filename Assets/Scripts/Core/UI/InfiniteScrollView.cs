using StrategyGame.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace StrategyGame.Core.UI
{
    public class InfiniteScrollView : MonoBehaviour
    {
        public bool IsInitialized { get { return _isInitialized; } }

        private bool _isInitialized = false;
        [SerializeField] private ScrollRect _scrollRect;
        private GridLayoutGroup _gridLayoutGroup;
        private float _disableMarginY = 0;
        private bool _isGridDisabled = false;
        private List<RectTransform> menuItems = new List<RectTransform>();
        private Vector2 _newPos = Vector2.zero;
        [SerializeField] private float _treshold;
        [SerializeField] private GameObject[] _prefabs;
        [SerializeField] private Transform _disabledPoolParent;
        private int _itemCount = 20;
        private int _columnCount = 0;
        private float _offset = 0;
        private ScrolMenuItemPool _scrollMenuItemPool;
        void Awake()
        {
            Initialize();
        }
        public void Initialize()
        {
            if (!_isInitialized)
            {
                _scrollMenuItemPool = new ScrolMenuItemPool(_prefabs,_itemCount,_disabledPoolParent, _scrollRect.content);
                _scrollRect.onValueChanged.AddListener(OnScroll);
                _scrollRect.movementType = ScrollRect.MovementType.Unrestricted;

                for (int i = 0; i < _itemCount; i++)
                {
                    GameObject newChildItem = _scrollMenuItemPool.GetObject();
                    newChildItem.SetActive(true);
                    newChildItem.transform.SetParent(_scrollRect.content);
                    menuItems.Add(_scrollRect.content.GetChild(i).GetComponent<RectTransform>());
                }
                if (_scrollRect.content.GetComponent<GridLayoutGroup>() != null)
                {
                    _gridLayoutGroup = _scrollRect.content.GetComponent<GridLayoutGroup>();
                    _columnCount = _gridLayoutGroup.constraintCount;
                }
                _itemCount = _scrollRect.content.childCount;
                _isInitialized = true;
            }
        }
        void DisableGridComponents()
        {
            _offset = menuItems[0].GetComponent<RectTransform>().anchoredPosition.y - menuItems[1].GetComponent<RectTransform>().anchoredPosition.y;
            _disableMarginY = _offset * (_itemCount / _columnCount) / 2; 
            if (_gridLayoutGroup)
            {
                _gridLayoutGroup.enabled = false;
            }
            _isGridDisabled = true;
        }
        public void OnScroll(Vector2 pos)
        {
            if (!_isGridDisabled)
                DisableGridComponents();

            for (int i = 0; i < menuItems.Count; i++)
            {
                if (_scrollRect.transform.InverseTransformPoint(menuItems[i].gameObject.transform.position).y > _disableMarginY + _treshold)
                {
                    ArrangeMenuItems(i, true, _itemCount - 1, 0);
                }
                else if (_scrollRect.transform.InverseTransformPoint(menuItems[i].gameObject.transform.position).y < -_disableMarginY)
                {
                    ArrangeMenuItems(i, false, 0, _itemCount - 1);
                }
            }
        }
        private void ArrangeMenuItems(int i,bool isUp,int oldIndex,int newIndex)
        {
            Vector2 anchorMax = menuItems[i].anchorMax;
            Vector2 anchorMin = menuItems[i].anchorMin;
            _newPos = menuItems[i].anchoredPosition;

            if(isUp)
                _newPos.y -= (_itemCount / _columnCount) * _offset;
            else
                _newPos.y += (_itemCount / _columnCount) * _offset;

            _scrollMenuItemPool.ReleaseObject(menuItems[i].gameObject);
            menuItems.RemoveAt(i);
            GameObject newMenuItem = _scrollMenuItemPool.GetObject();
            RectTransform curRect = newMenuItem.GetComponent<RectTransform>();
            curRect.anchorMin = anchorMin;
            curRect.anchorMax = anchorMax;
            curRect.anchoredPosition = _newPos;
            menuItems.Add(curRect);
            _scrollRect.content.GetChild(oldIndex).transform.SetSiblingIndex(newIndex);
        }

    }
}
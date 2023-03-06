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
        private ScrollRect _scrollRect;
        private GridLayoutGroup _gridLayoutGroup;
        private float _disableMarginY = 0;
        private bool _isGridDisabled = false;
        private List<RectTransform> menuItems = new List<RectTransform>();
        private Vector2 _newPos = Vector2.zero;
        [SerializeField] private float _treshold;
        private int _itemCount = 0;
        private int _columnCount = 0;
        private float _offset = 0;

        void Awake()
        {
            Initialize();
        }
        public void Initialize()
        {
            if (!_isInitialized)
            {
                _scrollRect = GetComponent<ScrollRect>();
                _scrollRect.onValueChanged.AddListener(OnScroll);
                _scrollRect.movementType = ScrollRect.MovementType.Unrestricted;

                for (int i = 0; i < _scrollRect.content.childCount; i++)
                {
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
                    _newPos = menuItems[i].anchoredPosition;
                    _newPos.y -= (_itemCount / _columnCount) * _offset;
                    menuItems[i].anchoredPosition = _newPos;
                    _scrollRect.content.GetChild(_itemCount - 1).transform.SetAsFirstSibling();
                }
                else if (_scrollRect.transform.InverseTransformPoint(menuItems[i].gameObject.transform.position).y < -_disableMarginY)
                {
                    _newPos = menuItems[i].anchoredPosition;
                    _newPos.y += (_itemCount / _columnCount) * _offset;
                    menuItems[i].anchoredPosition = _newPos;
                    _scrollRect.content.GetChild(0).transform.SetAsLastSibling();
                }
            }
        }
    }
}
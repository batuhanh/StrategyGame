using StrategyGame.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace StrategyGame.Core.UI
{
    public enum ScrollDirection
    {
        None,
        Up,
        Down
    }
    public class InfiniteScrollView : MonoBehaviour
    {
        public bool IsInitialized { get { return _isInitialized; } }

        [SerializeField] private int _itemCount;
        [SerializeField] private GameObject[] _itemPrefabs;
        [SerializeField] private GridLayoutGroup layout;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private float _threshold;
        private ScrolMenuItemPool _itemPool;
        private Vector2 _offset;
        private float _minScrollVal = 0.39f;
        private float _maxScrollVal = 0.44f;
        private ScrollDirection _lastScrollDir;
        private bool _isInitialized = false;
        private void Start()
        {
            if (_itemPrefabs.Length > 0)
            {
                _itemPool = new ScrolMenuItemPool(_itemPrefabs, _itemCount, layout.transform);
                for (int i = 0; i < _itemCount; i++)
                {
                    _itemPool.GetObject();
                }
                _offset = new Vector2(0, layout.cellSize.y + layout.spacing.y);
                _isInitialized = true;
            }
        }
        public void OnScrolled(Vector2 value)
        {
            if (_isInitialized)
            {
                if (value.y <= _minScrollVal)
                    _lastScrollDir = ScrollDirection.Down;
                else if (value.y >= _maxScrollVal)
                    _lastScrollDir = ScrollDirection.Up;
                else
                    _lastScrollDir = ScrollDirection.None;

                if (_lastScrollDir != ScrollDirection.None)
                {
                    int oldIndex = 0;
                    int newIndex = _itemCount - 1;
                    Vector2 curOffset = _offset;
                    if (_lastScrollDir == ScrollDirection.Down)
                    {
                        oldIndex = _itemCount - 1;
                        newIndex = 0;
                        curOffset = -_offset;
                    }

                    for (int i = 0; i < 2; i++)
                    {
                        Transform firstChild = scrollRect.content.GetChild(oldIndex);
                        firstChild.SetSiblingIndex(newIndex);
                    }
                    scrollRect.content.anchoredPosition += curOffset;
                }
            }
        }
    }
}
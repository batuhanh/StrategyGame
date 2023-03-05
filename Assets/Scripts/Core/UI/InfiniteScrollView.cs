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
        [SerializeField] private int _itemCount;
        [SerializeField] private GameObject _itemPrefab;
        [SerializeField] private GridLayoutGroup layout;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private float _threshold;
        private ScrolMenuItemPool _itemPool;
        private Vector2 _offset;
        private float _minScrollVal=0.39f;
        private float _maxScrollVal=0.44f;
        private ScrollDirection _lastScrollDir;
        private void Start()
        {
            _itemPool = new ScrolMenuItemPool(_itemPrefab, _itemCount, layout.transform);
            for (int i = 0; i < _itemCount; i++)
            {
                _itemPool.GetObject();
            }
            _offset = Vector2.up * (layout.cellSize.y + layout.spacing.y);
        }
        public void OnScrolled(Vector2 value)
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
using StrategyGame.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace StrategyGame.Core.UI
{
    public enum ScrollDirection
    {
        Up,
        Down
    }
    public class InfiniteScrollView : MonoBehaviour
    {
        [SerializeField] private int _itemCount;
        [SerializeField] private GameObject _itemPrefab;
        [SerializeField] private GridLayoutGroup layout;
        [SerializeField] private ScrollRect scrollRect;
        private ObjectPool itemPool;
        private ScrollDirection lastScrollDir;
        private void Start()
        {
            itemPool = new ObjectPool(_itemPrefab, _itemCount, layout.transform);
            for (int i = 0; i < _itemCount; i++)
            {
                itemPool.GetObject();
            }
        }
        public void OnScrolled(Vector2 value)
        {
            Debug.Log("Value "+value);
        }
    }
}
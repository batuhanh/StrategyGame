using StrategyGame.Utils;
using UnityEngine;

namespace StrategyGame.MVC.Views
{
    public class InformationPanelView : MonoBehaviour
    {
        public bool IsInitialized { get { return _isInitialized; } }
        public Context Context { get { return _context; } }
        private bool _isInitialized = false;
        private Context _context;
        public void Initialize(Context context)
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
                _context = context;
            }
        }
    }
}

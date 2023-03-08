using StrategyGame.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace StrategyGame.MVC.Views
{
    public class InformationPanelView : MonoBehaviour
    {
        public bool IsInitialized { get { return _isInitialized; } }
        public Context Context { get { return _context; } }
        public Image Image { get { return _image; } }
        public Text TitleText { get { return _titleText; } }
        public Text DescText { get { return _descText; } }
        public Transform ProductsParent { get { return _productsParent; } }

        [SerializeField] private Image _image;
        [SerializeField] private Text _titleText;
        [SerializeField] private Text _descText;
        [SerializeField] private Transform _productsParent;
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

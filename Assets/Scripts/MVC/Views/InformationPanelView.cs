using StrategyGame.Core.Gameplay.BuildingSystem;
using StrategyGame.MVC.Controllers;
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
        private InformationPanelController _controller;
        public void Initialize(Context context,InformationPanelController _informationPanelController) 
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
                _context = context;
                _controller = _informationPanelController;
            }
        }
        private void OnEnable()
        {
            Barrack.barrackDestroyedEvent += _controller.CheckIsCurrentDestroyed;
            Armory.armoryDestroyedEvent += _controller.CheckIsCurrentDestroyed;
            PowerPlant.powerPlantDestroyedEvent += _controller.CheckIsCurrentDestroyed;
            MilitaryTower.militaryTowerDestroyedEvent += _controller.CheckIsCurrentDestroyed;
        }
        private void OnDisable()
        {
            Barrack.barrackDestroyedEvent -= _controller.CheckIsCurrentDestroyed;
            Armory.armoryDestroyedEvent -= _controller.CheckIsCurrentDestroyed;
            PowerPlant.powerPlantDestroyedEvent -= _controller.CheckIsCurrentDestroyed;
            MilitaryTower.militaryTowerDestroyedEvent -= _controller.CheckIsCurrentDestroyed;
        }
    }
}

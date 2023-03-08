using StrategyGame.Core.Gameplay.BuildingSystem;
using StrategyGame.Core.UI;
using StrategyGame.MVC.Models;
using StrategyGame.MVC.Views;
using StrategyGame.Utils;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace StrategyGame.MVC.Controllers
{
    public class InformationPanelController : BaseController<InformationPanelModel, InformationPanelView>
    {
        public InformationPanelModel model { get; private set; }
        public InformationPanelView view { get; private set; }

        public InformationPanelController(InformationPanelModel model, InformationPanelView view) : base(model, view)
        {

        }
        public override void Initialize(Context context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);
                SetInformationNull();
            }
        }
        public void SetInformation(Building clickedBuilding,string title,string desc,Sprite buildingImg, GameObject[] products)
        {
            _view.TitleText.text = title;
            _view.DescText.text = desc;
            if (!_view.Image.gameObject.activeSelf)
            {
                _view.Image.gameObject.SetActive(true);
            }
            _view.Image.sprite = buildingImg;
            int childCount = _view.ProductsParent.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                GameObject.Destroy(_view.ProductsParent.transform.GetChild(i).gameObject);
            }
            for (int i = 0; i < products.Length; i++)
            {
                GameObject spawnedProduct = GameObject.Instantiate(products[i], _view.ProductsParent);
                spawnedProduct.GetComponent<SoldierProductUI>().Barrack = clickedBuilding as Barrack;
            }
        }
        private void SetInformationNull()
        {
            _view.TitleText.text = "";
            _view.DescText.text = "";
            _view.Image.sprite = null;
            _view.Image.gameObject.SetActive(false);
            int childCount = _view.ProductsParent.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                GameObject.Destroy(_view.ProductsParent.transform.GetChild(i).gameObject);
            }
        }
    }
}

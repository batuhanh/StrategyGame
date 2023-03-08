using StrategyGame.MVC.Models;
using StrategyGame.MVC.Views;
using StrategyGame.Utils;
using System.Collections;
using System.Collections.Generic;
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
            }
        }
        public void SetInformation(string title,string desc,Sprite buildingImg, GameObject[] products)
        {
            _view.TitleText.text = title;
            _view.DescText.text = desc;
            _view.Image.sprite = buildingImg;
            for (int i = 0; i < _view.ProductsParent.transform.childCount; i++)
            {
                GameObject.Destroy(_view.ProductsParent.transform.GetChild(0).gameObject);
            }
            for (int i = 0; i < products.Length; i++)
            {
                GameObject spawnedProduct = GameObject.Instantiate(products[i], _view.ProductsParent);
            }
        }
        private void SetInformationNull()
        {

        }
    }
}

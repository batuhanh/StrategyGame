using StrategyGame.MVC.Controllers;
using StrategyGame.MVC.Models;
using StrategyGame.MVC.Views;
using StrategyGame.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.MVC
{
    public class ProductionMenu
    {
        public bool IsInitialized { get { return _isInitialized; } }
        public Context Context { get { return _context; } }
        public ProductionMenuModel ProductionMenuModel { get { return _productionMenuModel; } }
        public ProductionMenuView ProductionMenuView { get { return _productionMenuView; } }
        public ProductionMenuController ProductionMenuController { get { return _productionMenuController; } }

        private bool _isInitialized = false;
        private Context _context;
        private ProductionMenuModel _productionMenuModel;
        private ProductionMenuView _productionMenuView;
        private ProductionMenuController _productionMenuController;

        public ProductionMenu(ProductionMenuView productionMenuView)
        {
            _productionMenuView = productionMenuView;
        }
        public void Initialize()
        {
            if (!IsInitialized)
            {
                _isInitialized = true;

                _context = new Context();
                _productionMenuModel = new ProductionMenuModel();
                _productionMenuController = new ProductionMenuController(_productionMenuModel, _productionMenuView);

                _productionMenuModel.Initialize(_context);
                _productionMenuView.Initialize(_context);
                _productionMenuController.Initialize(_context);
            }
        }

        public void RequireIsInitialized()
        {
            if (!_isInitialized)
            {
                throw new Exception("MustBeInitialized");
            }
        }
    }
}

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
    public class InformationPanel
    {
        public bool IsInitialized { get { return _isInitialized; } }
        public Context Context { get { return _context; } }
        public InformationPanelModel InformationPanelModel { get { return _informationPanelModel; } }
        public InformationPanelView InformationPanelView { get { return _informationPanelView; } set { _informationPanelView = value; } }
        public InformationPanelController InformationPanelController { get { return _informationPanelController; } }

        private bool _isInitialized = false;
        private Context _context;
        private InformationPanelModel _informationPanelModel;
        private InformationPanelView _informationPanelView;
        private InformationPanelController _informationPanelController;
        private InformationPanel() { }

        private static InformationPanel instance = null;
        public static InformationPanel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InformationPanel();
                }
                return instance;
            }
        }
        public void Initialize()
        {
            if (!IsInitialized)
            {
                _isInitialized = true;

                _context = new Context();
                _informationPanelModel = new InformationPanelModel();
                _informationPanelController = new InformationPanelController(_informationPanelModel, _informationPanelView);

                _informationPanelModel.Initialize(_context);
                _informationPanelView.Initialize(_context);
                _informationPanelController.Initialize(_context);
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

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
    public class GameGrid
    {
        public bool IsInitialized { get { return _isInitialized; } }
        public Context Context { get { return _context; } }
        public GameGridModel GameGridModel { get { return _gameGridModel; } }
        public GameGridView GameGridView { get { return _gameGridView; } set { _gameGridView = value; } }
        public GameGridController GameGridController { get { return _gameGridController; } }

        private bool _isInitialized = false;
        private Context _context;
        private GameGridModel _gameGridModel;
        private GameGridView _gameGridView;
        private GameGridController _gameGridController;
        private GameGrid() { }

        private static GameGrid instance = null;
        public static GameGrid Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameGrid();
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
                _gameGridModel = new GameGridModel();
                _gameGridController = new GameGridController(_gameGridModel, _gameGridView);

                _gameGridModel.Initialize(_context);
                _gameGridView.Initialize(_context);
                _gameGridController.Initialize(_context);
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

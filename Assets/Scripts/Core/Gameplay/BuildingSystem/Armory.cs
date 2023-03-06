using StrategyGame.MVC.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.Core.Gameplay.BuildingSystem
{
    public class Armory : Building
    {
        public override string Name { get { return _name; } }
        public override Sprite Image { get { return _image; } }
        public override int StartHealth { get { return _startHealth; } }
        public override int CurrentHealth { get { return _currentHealth; } }
        public override bool IsPlaced { get { return _isPlaced; } }
        public override Vector2 GridPosition { get { return _gridPosition; } }

        private string _name;
        private Sprite _image;
        private int _startHealth;
        private int _currentHealth;
        private bool _isPlaced;
        private Vector2 _gridPosition;

    }
}

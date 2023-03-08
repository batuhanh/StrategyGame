using StrategyGame.Core.Gameplay.BuildingSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.Core.Gameplay.BuildingSystem
{
    public class PowerPlant : Building
    {
        public override string Name { get { return _name; } }
        public override Sprite Image { get { return _image; } }
        public override int StartHealth { get { return _startHealth; } }
        public override int CurrentHealth { get { return _currentHealth; } }
        public override bool IsPlaced { get { return _isPlaced; } set { _isPlaced = value; } }

        public override Vector2 GridPosition { get { return _gridPosition; } }
        public override Vector3Int Size { get { return _size; } set { _size = value; } }
        [SerializeField] private string _name;
        [SerializeField] private Sprite _image;
        [SerializeField] private int _startHealth;
        [SerializeField] private int _currentHealth;
        [SerializeField] private bool _isPlaced;
        [SerializeField] private Vector2 _gridPosition;
        [SerializeField] private Vector3Int _size;
        [SerializeField] private SpriteRenderer _spriteRenderer;
    }
}

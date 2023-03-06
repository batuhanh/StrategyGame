using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.Core.Gameplay.BuildingSystem
{
    public class Barrack : Building
    {
        public override string Name { get { return _name; } }
        public override Sprite Image { get { return _image; } }
        public override int StartHealth { get { return _startHealth; } }
        public override int CurrentHealth { get { return _currentHealth; } }
        public override bool IsPlaced { get { return _isPlaced; } }
        public override Vector2 GridPosition { get { return _gridPosition; } }
        public override Vector2 Size { get { return _size; } }

        [SerializeField] private string _name;
        [SerializeField] private Sprite _image;
        [SerializeField] private int _startHealth;
        [SerializeField] private int _currentHealth;
        [SerializeField] private bool _isPlaced;
        [SerializeField] private Vector2 _gridPosition;
        [SerializeField] private Vector2 _size;
        [SerializeField] private SpriteRenderer _spriteRenderer;

    }
}

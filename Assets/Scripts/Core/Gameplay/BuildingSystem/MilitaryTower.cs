using StrategyGame.Core.Gameplay.BuildingSystem;
using StrategyGame.MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame.Core.Gameplay.BuildingSystem
{
    public class MilitaryTower : Building
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
        public override void CallInformationPanel()
        {
            string desc = "Health: " + StartHealth;
            InformationPanel.Instance.InformationPanelController.SetInformation(this, Name, desc, Image, new GameObject[0]);
        }
        public override void SetColor(Color newColor)
        {
            _spriteRenderer.color = newColor;
        }
        public override void OnHolded()
        {
            _spriteRenderer.sortingOrder = 2;
        }
        public override void Place()
        {
            IsPlaced = true;
            _spriteRenderer.sortingOrder = 0;
        }
        public override void TakeDamage(int amount)
        {

        }
    }
}

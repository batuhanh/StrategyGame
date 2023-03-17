using StrategyGame.Core.Gameplay.SoldierSystem;
using StrategyGame.MVC;
using StrategyGame.MVC.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace StrategyGame.Core.Gameplay.BuildingSystem
{
    public class Armory : Building
    {
        public override string Name { get { return _name; } }
        public override Sprite Image { get { return _image; } }
        public override int StartHealth { get { return _startHealth; } }
        public override int CurrentHealth { get { return _currentHealth; } }
        public override Vector2 GridPosition { get { return _gridPosition; } }
        public override Vector3Int Size { get { return _size; } set { _size = value; } }

        public override bool IsPlaced { get { return _isPlaced; } set { _isPlaced = value; } }

        [SerializeField] private string _name;
        [SerializeField] private Sprite _image;
        [SerializeField] private int _startHealth;
        [SerializeField] private int _currentHealth;
        [SerializeField] private bool _isPlaced;
        [SerializeField] private Vector2 _gridPosition;
        [SerializeField] private Vector3Int _size;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private BoxCollider2D _boxCollider2D;
        public static event Action<IShowable> armoryDestroyedEvent;
        public override void CallInformationPanel()
        {
            string desc = "Health: " + CurrentHealth;
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
            Vector3 blockSize = GameGrid.Instance.GameGridView.Grid.cellSize;
            _boxCollider2D.size = new Vector2(_boxCollider2D.size.x + blockSize.x,
                _boxCollider2D.size.y + blockSize.y);
        }
        public override void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                Dead();
            }
        }
        private void Dead()
        {
            GameGrid.Instance.GameGridController.ChangeGridTileState(GetStartPosition(), Size, GameGrid.Instance.GameGridView.EmptyTile);
            BattleHandler.Instance.InvokeItemDestroyed(this);
            armoryDestroyedEvent?.Invoke(this);
            Destroy(gameObject);
        }
    }
}

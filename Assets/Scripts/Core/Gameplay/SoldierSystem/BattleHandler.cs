using StrategyGame.Core.Gameplay.BuildingSystem;
using StrategyGame.Core.Managers;
using StrategyGame.MVC;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace StrategyGame.Core.Gameplay.SoldierSystem
{
    public class BattleHandler : MonoBehaviour
    {
        [SerializeField] private IClickable leftClickedItem;
        [SerializeField] private IClickable rightClickedItem;
        [SerializeField] private Soldier leftClickedSoldier;
        [SerializeField] private IDamageable rightClickedTarget;
        [SerializeField] private LayerMask clickableLayer;

        private void Update()
        {
            switch (InputManager.Instance.CurrentMouseState)
            {
                case MouseState.Down:
                    break;
                case MouseState.Hold:
                    break;
                case MouseState.Up:
                    break;
            }

        }
        public void DetectClickedTarget(int mouseIndex, Vector3 mousePosition)
        {
            if (mouseIndex == 0)
            {
                Debug.Log("Left clciked");
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1000f, clickableLayer);
                if (hit.collider != null)
                {
                    Debug.Log("clickable found");
                    leftClickedItem = hit.collider.GetComponent<IClickable>();
                    leftClickedSoldier = hit.collider.GetComponent<Soldier>();
                    var temp = hit.collider.GetComponent<IShowable>();
                    if (temp != null)
                    {
                        Debug.Log("showable found");
                        temp.CallInformationPanel();
                    }
                }
                else
                {
                    Clear();
                }
                

            }
            else if (mouseIndex == 1)
            {
                Debug.Log("Right clicked ");
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1000f);
                if (hit.collider != null)
                {
                    Debug.Log("hitted");
                    rightClickedItem = hit.collider.GetComponent<IClickable>();
                    rightClickedTarget = hit.collider.GetComponent<IDamageable>();
                    if (leftClickedSoldier != null && rightClickedTarget != null)
                    {
                        Debug.Log("Attack move started");
                        leftClickedSoldier.DecideMovement(rightClickedTarget);
                    }
                   
                }
                else if (GameGrid.Instance.GameGridController.IsPosOnGrid(Camera.main.ScreenToWorldPoint(mousePosition)))
                {
                    Debug.Log("Grid clciked");
                    TileBase clickedTile = GameGrid.Instance.GameGridController.GetTileBase(Camera.main.ScreenToWorldPoint(mousePosition));
                    Debug.Log(leftClickedSoldier + " " + clickedTile);
                    if (leftClickedSoldier != null && clickedTile == GameGrid.Instance.GameGridView.EmptyTile)
                    {
                        Debug.Log("Normal move started");
                        leftClickedSoldier.DecideMovement(Camera.main.ScreenToWorldPoint(mousePosition));
                    }
                   
                }
                else
                {
                   
                }
                Clear();

            }
            

        }
        private void Clear()
        {
            leftClickedItem = null;
            rightClickedItem = null;
            leftClickedSoldier = null;
            rightClickedTarget = null;
        }
        private void OnEnable()
        {
            InputManager.OnMouseUpEvent += DetectClickedTarget;
        }
        private void OnDisable()
        {
            InputManager.OnMouseUpEvent -= DetectClickedTarget;
        }
    }
}

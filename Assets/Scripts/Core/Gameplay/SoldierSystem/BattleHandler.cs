using StrategyGame.Core.Gameplay.BuildingSystem;
using StrategyGame.Core.Gameplay.PathFinding;
using StrategyGame.Core.Managers;
using StrategyGame.MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
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
        public static event Action<IDamageable> ItemDestroyed;
        public static event Action<IDamageable> SoldierStartedMove;
        public static BattleHandler Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
        public void DetectClickedTarget(int mouseIndex, Vector3 mousePosition)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (mouseIndex == 0)
                {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1000f, clickableLayer);
                    if (hit.collider != null)
                    {
                        if (leftClickedSoldier != null)
                        {
                            leftClickedSoldier.SetClickedStatus(false);
                        }
                        leftClickedItem = hit.collider.GetComponent<IClickable>();
                        leftClickedSoldier = hit.collider.GetComponent<Soldier>();
                        var temp = hit.collider.GetComponent<IShowable>();
                        if (temp != null)
                        {
                            temp.CallInformationPanel();
                        }
                        if (leftClickedSoldier != null)
                        {
                            leftClickedSoldier.SetClickedStatus(true);
                        }
                    }
                    else
                    {
                        Clear();

                    }


                }
                else if (mouseIndex == 1)
                {
                    Debug.Log("RightClcked");
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1000f);
                    if (hit.collider != null)
                    {
                        Debug.Log("RightClckedHitted");
                        rightClickedItem = hit.collider.GetComponent<IClickable>();
                        rightClickedTarget = hit.collider.GetComponent<IDamageable>();
                        if (leftClickedSoldier != null && rightClickedTarget != null && leftClickedSoldier != rightClickedTarget as Soldier)
                        {
                            leftClickedSoldier.DecideMovement(rightClickedTarget);
                            SoldierStartedMove?.Invoke(leftClickedSoldier as IDamageable);
                        }
                    }
                    else if (GameGrid.Instance.GameGridController.IsPosOnGrid(Camera.main.ScreenToWorldPoint(mousePosition)))
                    {
                        TileBase clickedTile = GameGrid.Instance.GameGridController.GetTileBase(Camera.main.ScreenToWorldPoint(mousePosition));
                        if (leftClickedSoldier != null && clickedTile == GameGrid.Instance.GameGridView.EmptyTile)
                        {

                            leftClickedSoldier.DecideMovement(Camera.main.ScreenToWorldPoint(mousePosition));
                            SoldierStartedMove?.Invoke(leftClickedSoldier as IDamageable);
                        }
                    }
                    else
                    {

                    }
                    Clear();
                }
            }
        }
        private void Clear()
        {
            if (leftClickedSoldier != null)
            {
                leftClickedSoldier.SetClickedStatus(false);
            }
            leftClickedItem = null;
            rightClickedItem = null;
            leftClickedSoldier = null;
            rightClickedTarget = null;
        }
        public void InvokeItemDestroyed(IDamageable target)
        {
            ItemDestroyed?.Invoke(target);
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

using BattleSystem;
using EventBusSystem;
using System.Collections.Generic;
using TileSystem;
using UnityEngine;

namespace UISystem
{
    public class OrderDrawer : IComandGivenHandler
    {
        private GameObject _arrow;
        private Transform _parent;
        private PlayersColors _colors = new();

        private Dictionary<IComand, ArrowView> arrows = new();

        public OrderDrawer(Transform parentForView)
        {
            _arrow = (GameObject)Resources.Load("ViewElements/Arrow");
            _parent = parentForView;
            EventBus.Subscribe(this);
        }

        public void OnGivenComandToAttack(TerrainCell from, TerrainCell to, IAttackComand comand)
        {
            Vector3 fromCellPosition = from.transform.position;
            Vector3 toCellPosition = to.transform.position;

            Vector3 arrowPosition = Vector3.Lerp(fromCellPosition, toCellPosition, 0.5f);
            Quaternion arrowRotation = GetRorarionBetween(fromCellPosition, toCellPosition);
            GameObject newArrow = Object.Instantiate(_arrow, arrowPosition, arrowRotation, _parent);

            ArrowView View = newArrow.GetComponent<ArrowView>();
            View.InstanceColor(_colors.GetColor(comand.GetAttackingPlayer().acktorName));
            View.comand = comand;
            arrows.Add(comand, View);
            comand.OnComandEnd += DeleteView;
        }
        private Quaternion GetRorarionBetween(Vector3 from, Vector3 to)
        {
            Vector3 dir = (to - from).normalized;
            float angle = Vector2.SignedAngle(Vector3.up, new Vector3(dir.x, dir.y));
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void DeleteView(IComand comand)
        {
            arrows.TryGetValue(comand, out ArrowView view);
            Object.Destroy(view.gameObject);
            arrows.Remove(comand);
        }
    }
}

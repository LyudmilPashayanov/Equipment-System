using UnityEngine;

namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// Handles all Unity and visual related logic for the Rock item. 
    /// </summary>
    public class RockView : ItemView
    {
        [SerializeField] private RockModel _rockModel;
        public override void InitController()
        {
            Controller = new RockController(this, _rockModel);
        }
    }
}

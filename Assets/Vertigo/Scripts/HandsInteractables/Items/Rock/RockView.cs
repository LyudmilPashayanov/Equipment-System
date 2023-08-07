using UnityEngine;

namespace Vertigo.Player.Interactables
{
    public class RockView : ItemView
    {
        [SerializeField] private RockModel _rockModel;
        public override void InitController()
        {
            Controller = new RockController(this, _rockModel);
        }
    }
}

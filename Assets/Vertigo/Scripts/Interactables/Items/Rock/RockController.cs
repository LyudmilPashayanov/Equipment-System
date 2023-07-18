using UnityEngine;

namespace Vertigo.Player.Interactables
{
    public class RockController : ItemController
    {
/*        public RockController(Rigidbody rb) : base(rb)
        {
        }*/

        public override void StartUse(Hand handUsingIt)
        {
            _handHolder.ReleaseCurrentItem();
        }
    }
}
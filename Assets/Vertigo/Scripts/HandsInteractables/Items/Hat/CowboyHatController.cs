
using System;

namespace Vertigo.Player.Interactables
{
    public class CowboyHatController : ItemController<CowboyHatView>
    {
        #region Functionality

        public CowboyHatController(CowboyHatView view) : base(view)
        {
            _view.EnableOnHeadCollider(false);
        }

        public void TryEquipOnHead(bool successfulEquip)
        {
            if (successfulEquip)
            {
                EquipOnHead();
            }
        }

        public void HatInUseIndication()
        {
            _view.UnusableIndication();
        }

        private void EquipOnHead()
        {
            // Drop from hand -> _handHolder.ReleaseCurrentItem(false);
            // UnsubscribeHand();
            _view.EnableOnHeadCollider(true);
        }

        public override Type GetItemType()
        {
            throw new NotImplementedException();
        }

        public override bool IsCompatible(Type otherItemType)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

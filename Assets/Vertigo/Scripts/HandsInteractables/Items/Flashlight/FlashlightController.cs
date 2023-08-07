namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// This class contains the business logic of the Flashlight item as a Grabbable game object.
    /// </summary>
    public class FlashlightController : ItemController<FlashlightView>
    {
        #region Variables
        private bool _isFlashOn;

        public FlashlightController(FlashlightView view) : base(view)
        {

        }
        #endregion
        #region Functionality

        public override void StartUse()
        {
            base.StartUse();
            _isFlashOn = !_isFlashOn;
            _view.ToggleFlashlight(_isFlashOn);
        }

        public override void ToggleItem()
        {
            _view.ChangeIntensity();
           // AudioManager.Instance.PlaySound(); // play shared toggle sound 
        }
        #endregion

    }
}
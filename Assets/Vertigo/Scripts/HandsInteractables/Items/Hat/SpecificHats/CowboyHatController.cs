namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// Base class marking any HatController as a Hat item. It contains the business and Unity logic of any Hat item as a Grabbable game object.
    /// </summary>
    public class CowboyHatController : HatController
    {
        #region Functionality

        public CowboyHatController(CowboyHatView view) : base(view)
        { }
        #endregion
    }
}

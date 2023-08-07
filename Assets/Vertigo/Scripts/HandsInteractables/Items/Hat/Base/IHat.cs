namespace Vertigo.Player.Interactables
{
    /// <summary>
    /// Base class marking any HatController as a Hat item.
    /// </summary>
    public interface IHat
    {
        public void TryEquipOnHead(bool equipped);
    }
}
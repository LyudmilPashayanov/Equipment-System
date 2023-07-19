using Vertigo.Player.Interactables;
using UnityEngine;

namespace Vertigo
{
    /// <summary>
    /// Simple system to spawn items in the game, when the Lever component is pulled down. 
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        #region Variables
        [SerializeField] private GameObject _objectToSpawn;
        [SerializeField] private Transform _spawningPosition;
        [SerializeField] private LeverController _lever;
        [SerializeField] private string _successfulText = "";
        #endregion

        #region Functionality
        private void Start()
        {
            _lever.AddListener(SpawnItem);
            if(_successfulText != "")
                _lever.ChangeOnSuccessfulText(_successfulText);
        }

        private void SpawnItem()
        {
            Instantiate(_objectToSpawn, _spawningPosition.transform.position, _spawningPosition.transform.rotation);
        }
        #endregion
    }
}

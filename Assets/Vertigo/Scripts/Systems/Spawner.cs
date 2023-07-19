using Vertigo.Player.Interactables;
using UnityEngine;

namespace Vertigo
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToSpawn;
        [SerializeField] private Transform _spawningPosition;
        [SerializeField] private LeverController _lever;
        [SerializeField] private string _successfulText = "";
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

    }
}

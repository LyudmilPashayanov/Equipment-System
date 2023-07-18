using Vertigo.Player.Interactables;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private Transform _spawningPosition;
    [SerializeField] private LeverController _lever;

    private void Start()
    {
        _lever.AddListener(SpawnItem);
    }

    private void SpawnItem() 
    {
        Instantiate(_objectToSpawn, _spawningPosition.transform.position, _spawningPosition.transform.rotation);
    }

}

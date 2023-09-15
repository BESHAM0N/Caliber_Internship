using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
public class SpawnManager : MonoBehaviour
{
    private const float _yRotation = -90f;
    private const float _zRotation = 90f;
    private const int _maxNamber = 100;

    public List<GameObject> prefabsContainer;
    public GameObject prefab;
    public GameObject increaseButton;
    public GameObject reduceButton;

    [SerializeField] private float _spawnRadius = 6f;

    public void StartSpawn()
    {
        increaseButton.SetActive(true);
        reduceButton.SetActive(true);
        AddObjects();
    }

    private void AddObjects()
    {
        var hundred = GetHundredObjects();
        prefabsContainer.AddRange(hundred);
    }

    private IEnumerable<GameObject> GetHundredObjects()
    {
        var prefabArray = new GameObject[_maxNamber];
        for (int i = 0; i < _maxNamber; i++)
        {
            GameObject newPrefab = CreateObject(prefab);
            prefabArray[i] = newPrefab;
        }
        return prefabArray;
    }

    private GameObject CreateObject(GameObject model)
    {
        Quaternion rotation = new Quaternion(0f, _yRotation, _zRotation, 0f);
        return Instantiate(model, model.transform.position +
                                   Random.insideUnitSphere * _spawnRadius, rotation);
    }

    public void DeleteHundredObjects()
    {
        if (prefabsContainer.Any())
        {
            reduceButton.SetActive(true);
            for (int i = prefabsContainer.Count - _maxNamber; i < prefabsContainer.Count; i++)
                Destroy(prefabsContainer[i]);

            prefabsContainer.RemoveRange(prefabsContainer.Count - _maxNamber, _maxNamber);
        }
        else
        {
            reduceButton.SetActive(false);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject cookiePrefab;
    public List<GameObject> cookiesContainer;
    public GameObject increaseButton;
    public GameObject reduceButton;
    private const float _yRotation = -90;
    private const float _zRotation = 90;
    private const int _maxNamberCookies = 100;
    [SerializeField] private float _spawnRadius = 6;


    public void StartSpawn()
    {
        increaseButton.SetActive(true);
        reduceButton.SetActive(true);
        AddPrefabs();
    }

    private void AddPrefabs()
    {
        var hundred = GetHundredCookies();
        cookiesContainer.AddRange(hundred);
    }

    private IEnumerable<GameObject> GetHundredCookies()
    {
        var cookieArray = new GameObject[_maxNamberCookies];
        for (int i = 0; i < _maxNamberCookies; i++)
        {
            GameObject newCookie = CreateCookiePrefab(cookiePrefab);
            cookieArray[i] = newCookie;
        }
        return cookieArray;
    }

    private GameObject CreateCookiePrefab(GameObject cookie)
    {
        Quaternion rotation = new Quaternion(0, _yRotation, _zRotation, 0);
        return Instantiate(cookie, cookie.transform.position +
                                   Random.insideUnitSphere * _spawnRadius, rotation);
    }

    public void DeleteHundredCookies()
    {
        if (cookiesContainer.Any())
        {
            reduceButton.SetActive(true);
            for (int i = cookiesContainer.Count - _maxNamberCookies; i < cookiesContainer.Count; i++)
                Destroy(cookiesContainer[i]);

            cookiesContainer.RemoveRange(cookiesContainer.Count - _maxNamberCookies, _maxNamberCookies);
        }
        else
            reduceButton.SetActive(false);
    }
}
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
        var cookieArray = new GameObject[100];
        for (int i = 0; i < 100; i++)
        {
            GameObject newCookie = CreateCookiePrefab(cookiePrefab);
            cookieArray[i] = newCookie;
        }

        return cookieArray;
    }

    private GameObject CreateCookiePrefab(GameObject cookie)
    {
        Quaternion rotation = new Quaternion(0, -90, 90, 0);
        return Instantiate(cookie, cookie.transform.position +
                                   Random.insideUnitSphere * _spawnRadius, rotation);
    }

    public void DeleteAllCookies()
    {
        if (cookiesContainer.Any())
        {
            reduceButton.SetActive(true);
            for (int i = cookiesContainer.Count - 100; i < cookiesContainer.Count; i++)
                Destroy(cookiesContainer[i]);

            cookiesContainer.RemoveRange(cookiesContainer.Count - 100, 100);
        }
        else
            reduceButton.SetActive(false);
    }
}
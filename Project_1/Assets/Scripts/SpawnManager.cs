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

    private const float XMax = 13;
    private const float YMax = 10;
    private const float ZPosition = -0.5f;

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
        Vector3 newPosition = new Vector3(
            Random.Range(-XMax, XMax),
            Random.Range(-YMax, YMax),
            ZPosition);
        Quaternion rotation = new Quaternion(0, -90, 90, 0); 
        return Instantiate(cookie, newPosition, rotation);
    }

    public void DeleteAllCookies()
    {
        if (cookiesContainer.Any())
        {
            reduceButton.SetActive(true);
            for (int i = cookiesContainer.Count-100; i < cookiesContainer.Count; i++)
            {
                Destroy(cookiesContainer[i]);
            }
            cookiesContainer.RemoveRange(cookiesContainer.Count - 100, 100);
        }
        else
        {
            reduceButton.SetActive(false);

        }
    }
}
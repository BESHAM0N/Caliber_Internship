using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject cookiePrefab;
    public List<GameObject> cookiesContainer;
    public GameObject startButton;
    public GameObject restartButton;
    public GameObject increaseButton;
    public GameObject reduceButton;

    private const float XMax = 13;
    private const float YMax = 10;
    private const float ZPosition = -0.5f;

    [SerializeField] private int maxCountObject = 500;
    private int minCountObject = 0;


    public void StartSpawn()
    {
        increaseButton.SetActive(true);
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
            CreateCookiePrefab(cookiePrefab);
        }
        return cookieArray;
    }

    private void CreateCookiePrefab(GameObject cookie)
    {
        Vector3 newPosition = new Vector3(
            Random.Range(-XMax, XMax),
            Random.Range(-YMax, YMax),
            ZPosition);
        Quaternion rotation = new Quaternion(0, -90, 90, 0); 
        Instantiate(cookie, newPosition, rotation);
    }


    private void DeleteAllObject()
    {
    }
}
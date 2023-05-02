using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{

    public bool canSpawn = true; // 1

    public GameObject sheepPrefab; // 2
    public GameObject bomb;
    public List<Transform> sheepSpawnPositions = new List<Transform>(); // 3
    public float timeBetweenSpawns; // 4
    

    private List<GameObject> sheepList = new List<GameObject>(); // 5

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnSheep()
    {
        
        Vector3 randomPosition = sheepSpawnPositions[Random.Range(0, sheepSpawnPositions.Count)].position; // 1
        
        GameObject something; // 2
        
        if (Random.Range(0,10)>=8){
            something = Instantiate(bomb, randomPosition, sheepPrefab.transform.rotation); // 2
            sheepList.Add(something); // 3
            something.GetComponent<Bomb>().SetSpawner(this); // 4
        }
        else{
            something = Instantiate(sheepPrefab, randomPosition, sheepPrefab.transform.rotation); // 2
            sheepList.Add(something); // 3
            something.GetComponent<Sheep>().SetSpawner(this); // 4
        }
        
        
    }
    private IEnumerator SpawnRoutine() // 1
    {
        while (canSpawn) // 2
        {
            SpawnSheep(); // 3
            yield return new WaitForSeconds(timeBetweenSpawns); // 4
        }
    }
    public void RemoveSheepFromList(GameObject sheep)
    {
        sheepList.Remove(sheep);
    }
    public void DestroyAllSheep()
    {
        foreach (GameObject sheep in sheepList) // 1
        {
            Destroy(sheep); // 2
        }

        sheepList.Clear();
    }
    
}

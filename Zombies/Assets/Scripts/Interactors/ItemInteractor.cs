using UnityEngine;

public class ItemInteractor : Interactor
{
    private const string ITEM_PATH = "Prefabs/Item";
    Item[] instance;
    Transform[] spawnPoint;
    int countSpawnPoint;

    public override void OnCreate()
    {
        spawnPoint = new Transform[10];
        instance = new Item[10];

        var spawnPointObj = GameObject.FindGameObjectsWithTag("ItemPoint");
        countSpawnPoint = spawnPointObj.Length;
        
        for (int i = 0; i < countSpawnPoint; i++)
        {
            spawnPoint[i] = spawnPointObj[i].transform;
        }
    }

    public void SpawnItem()
    {
        var prefab = Resources.Load<Item>(ITEM_PATH);
        System.Random random = new System.Random();

        for (int i = 0; i < 2; i++)
        {
            instance[i] = Instantiate(prefab, spawnPoint[random.Next(countSpawnPoint)].position, Quaternion.identity);
        }
    }


}

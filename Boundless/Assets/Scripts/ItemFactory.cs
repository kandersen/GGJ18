using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    public GameplayController GameplayController;
    public List<ItemBehaviour> ItemPrefabs =  new List<ItemBehaviour>();

    public ItemBehaviour SpawnItem()
    {
        int index = Random.Range(0, ItemPrefabs.Count);
        var prefab = ItemPrefabs[index];
        var result = Instantiate(prefab);
        result.GameplayController = GameplayController;
        return result;
    }
}
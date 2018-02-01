using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    public GameplayController GameplayController;
	public List<ItemBehaviour> GarbagePrefabs =  new List<ItemBehaviour>();
	public List<ItemBehaviour> ButtonPrefabs =  new List<ItemBehaviour>();
	public List<ItemBehaviour> AntennaPrefabs =  new List<ItemBehaviour>();
	public List<ItemBehaviour> PowerSourcePrefabs =  new List<ItemBehaviour>();

	private int ItemCount;

	public void Start()
	{
		ItemCount = GarbagePrefabs.Count + ButtonPrefabs.Count + AntennaPrefabs.Count + PowerSourcePrefabs.Count;
	}

    public ItemBehaviour SpawnItem()
    {
        int index = Random.Range(0, ItemCount);
		var prefab = PickItem(index);
        var result = Instantiate(prefab);
        result.GameplayController = GameplayController;
        return result;
    }

	private ItemBehaviour PickItem(int index)
	{
		if (index < GarbagePrefabs.Count) {
			return GarbagePrefabs [index];
		} else if (index < GarbagePrefabs.Count + ButtonPrefabs.Count) {
			return ButtonPrefabs [index - GarbagePrefabs.Count];
		} else if (index < GarbagePrefabs.Count + ButtonPrefabs.Count + AntennaPrefabs.Count) {
			return AntennaPrefabs [index - GarbagePrefabs.Count - ButtonPrefabs.Count];
		} else {
			return PowerSourcePrefabs [index - GarbagePrefabs.Count - ButtonPrefabs.Count - AntennaPrefabs.Count];
		}
	}
}
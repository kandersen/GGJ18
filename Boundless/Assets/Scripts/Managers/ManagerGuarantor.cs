#if UNITY_EDITOR

using UnityEngine;

public class ManagerGuarantor : MonoBehaviour
{
    public ManagerGuarantorPrefabs Prefabs;

    public void Awake()
    {
        EnsurePresenceOf(Prefabs.SoundManager);
        EnsurePresenceOf(Prefabs.PersistentDataManager);
    }

    private void EnsurePresenceOf<TManager>(TManager prefab)
        where TManager : Object
    {
        var t = FindObjectOfType<TManager>();
        if (t == null)
        {
            t = Instantiate(prefab);
        }
        DontDestroyOnLoad(t);
    }
}

#endif
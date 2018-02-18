using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour
{
    public PersistentDataManager PersistentDataManager;
    public SoundManager SoundManager;
    
    public void Start()
    {
        DontDestroyOnLoad(PersistentDataManager);
        DontDestroyOnLoad(SoundManager);
        
        SceneManager.LoadSceneAsync ("Intro");
    }
}
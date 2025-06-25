using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerObject : MonoBehaviour
{
    public static ManagerObject Instance { get; set; }

    public static SceneManagerJ Scene = new SceneManagerJ();
    public static TriggerEventManager TriggerEvent = new TriggerEventManager();
    public static SoundManager Sound = new SoundManager();
    public static ResourceManager Resource = new ResourceManager();

    public void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Scene.OnAwake();
        TriggerEvent.OnAwake();
        Sound.OnAwake();
        Resource.OnAwake();
    }
    public void Start()
    {
        Scene.OnStart();
        TriggerEvent.OnStart();
        Sound.OnStart();
        Resource.OnStart();
    }
    public void Update()
    {
        Scene.OnUpdate();
        TriggerEvent.OnUpdate();
        Sound.OnUpdate();
        Resource.OnUpdate();
    }

}

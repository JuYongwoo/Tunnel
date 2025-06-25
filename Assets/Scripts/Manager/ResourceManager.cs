using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResourceManager
{

    public T GetorAddComponent<T>(GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();
        if (component != null)
        {
            return component;
        }
        else
        {
            return component = go.AddComponent<T>();
        }

    }
    public void OnAwake()
    {

    }
    public void OnStart()
    {
    }

        public void OnUpdate()
    {
    }

}

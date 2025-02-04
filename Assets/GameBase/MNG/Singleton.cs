using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Generic Singleton class for non-persistent singletons.
/// </summary>
/// <typeparam name="T">Type of the singleton component.</typeparam>
public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    /// <summary>
    /// Gets the instance of the singleton.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Scene activeScene = SceneManager.GetActiveScene();
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("Managers"));
                GameObject obj = new GameObject();
                obj.name = typeof(T).Name;
                obj.hideFlags = HideFlags.HideAndDontSave;
                _instance = obj.AddComponent<T>();
                SceneManager.SetActiveScene(activeScene);
            }
            return _instance;
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}

/// <summary>
/// Generic Singleton class for persistent singletons.
/// </summary>
/// <typeparam name="T">Type of the singleton component.</typeparam>
public class SingletonPersistent<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    /// <summary>
    /// Gets the instance of the singleton.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = typeof(T).Name;
                obj.hideFlags = HideFlags.HideAndDontSave;
                _instance = obj.AddComponent<T>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
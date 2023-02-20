using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
   public static ResourceManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
    }
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/Weapons/{path}");

        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        return Object.Instantiate(prefab, parent);
    }

    public GameObject InstantiateEnemy(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/Enemy/{path}");

        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        return Object.Instantiate(prefab, parent);
    }

    public Sprite GetSprite<T>(string path)
    {
        Sprite sprite = Load<Sprite>($"{path}");

        if (sprite == null)
        {
            Debug.Log($"Failed to load sprite : {path}");
            return null;
        }
        return Resources.Load<Sprite>($"Sprites/{path}");
    }
}

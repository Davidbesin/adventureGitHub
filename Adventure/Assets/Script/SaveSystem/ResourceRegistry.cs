using UnityEngine;
using System;
using System.Collections.Generic;
public class ResourceRegistry : MonoBehaviour
{
    public static ResourceRegistry Instance;

    [SerializeField] private List<BaseResource> resources = new List<BaseResource>();
    private Dictionary<int, BaseResource> resourceDict = new Dictionary<int, BaseResource>();

    private void Awake()
    {
        Instance ??= this;

        for (int i = 0; i < resources.Count; i++)
        {
            resourceDict[i] = resources[i];
        }
    }

    public BaseResource GetResource(int key)
    {
        if (resourceDict.TryGetValue(key, out var resource))
            return resource;

        Debug.LogWarning($"Resource key {key} not found!");
        return null;
    }

    public int GetKey(BaseResource resource)
    {
        foreach (var kvp in resourceDict)
        {
            if (kvp.Value == resource)
                return kvp.Key;
        }

        Debug.LogWarning($"Resource {resource.name} not registered!");
        return -1;
    }
}


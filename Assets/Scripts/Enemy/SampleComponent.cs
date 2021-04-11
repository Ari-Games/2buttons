using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleComponent : MonoBehaviour
{
    public List<Transform> Points;

    void Awake()
    {
        foreach (Transform child in transform)
            Points.Add(child);
    }

    public void Set(bool flag)
    {
        gameObject.SetActive(flag);

    }
}

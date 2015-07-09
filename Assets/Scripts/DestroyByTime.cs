using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour
{
    public float lifetime = 0.0f;
    public GameObject g_object;

    private bool destroying = false;

    void Start()
    {
        if (lifetime > 0.0f && destroying == false)
        {
            Destroy(g_object, lifetime);
            destroying = true;
        }
    }

    void Update()
    {
        if (lifetime > 0.0f && destroying == false)
        {
            Destroy(g_object, lifetime);
            destroying = true;
        }
    }
}

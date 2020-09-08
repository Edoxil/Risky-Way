using System.Collections.Generic;
using UnityEngine;


public class ObjectsGenerator : MonoBehaviour
{

    void Start()
    {

    }

    public void Generate(List<GameObject> objects)
    {

        for (int i = 1; i <= 10; i++)
        {
            Vector3 pos = new Vector3(0f, 0.25f, 0f);
            pos.z = i * 10;
            Quaternion rot = Quaternion.Euler(0, 0, 0);
            GameObject obj = objects[Random.Range(0, objects.Count)];
            Instantiate(obj, pos, rot, transform);
        }
    }

}

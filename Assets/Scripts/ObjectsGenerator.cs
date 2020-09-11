using System.Collections.Generic;
using UnityEngine;


public class ObjectsGenerator : MonoBehaviour
{
    private List<GameObject> obstacles = null;
    private List<GameObject> powerUps = null;

    public void Generate(LevelData level)
    {
        obstacles = level.ObstaclesPrefabs;
        powerUps = level.PowerUpPrefabs;



        if (level.type == LevelData.LevelType.Streight)
        {
            // Спавн препитствий по прямой
            for (int i = 1; i <= 19; i++)
            {
                Vector3 position = new Vector3(0f, 0.25f, 0f);

                position.z = i * 10;
                Quaternion rot = Quaternion.Euler(0, 0, 0);
                GameObject obj = obstacles[Random.Range(0, obstacles.Count)];
                var instance = Instantiate(obj, position, rot, transform);
            }

            // Спавн койнов и сердец
            int[] posX = { -2, 0, 2 };
            for (int i = 1; i < 23; i++)
            {
                Vector3 position = new Vector3(0f, 0.25f, 0f);
                position.x = posX[Random.Range(0, posX.Length)];
                position.z = i * 8;
                Quaternion rot = Quaternion.Euler(0, 0, 0);
                GameObject obj = null;
                int rnd = Random.Range(0, 8);
                if (rnd == 4)
                {
                    obj = powerUps[1];
                }
                else
                {
                    obj = powerUps[0];
                }

                var instance = Instantiate(obj, position, rot, transform);
            }

        }


        if (level.type == LevelData.LevelType.Corner)
        {
            // Спавн препитствий по прямой
            for (int i = 1; i <= 10; i++)
            {

                Vector3 position = new Vector3(0f, 0.25f, 0f);

                position.z = i * 10;
                Quaternion rot = Quaternion.Euler(0, 0, 0);
                GameObject obj = obstacles[Random.Range(0, obstacles.Count)];
                var instance = Instantiate(obj, position, rot, transform);

            }
            // Спавн препитствий после поворота
            for (int i = 2; i <= 9; i++)
            {

                Vector3 position = new Vector3(0f, 0.25f, 103f);

                position.x = -(i * 10.5f);
                Quaternion rot = Quaternion.Euler(0, -90f, 0);
                GameObject obj = obstacles[Random.Range(0, obstacles.Count)];
                var instance = Instantiate(obj, position, rot, transform);

            }

            // Спавн койнов и сердец по прямой
            int[] posX = { -2, 0, 2 };
            for (int i = 1; i < 12; i++)
            {
                Vector3 position = new Vector3(0f, 0.25f, 0f);
                position.x = posX[Random.Range(0, posX.Length)];
                position.z = i * 8;
                Quaternion rot = Quaternion.Euler(0, 0, 0);
                GameObject obj = null;
                int rnd = Random.Range(0, 8);
                if (rnd == 4)
                {
                    obj = powerUps[1];
                }
                else
                {
                    obj = powerUps[0];
                }

                var instance = Instantiate(obj, position, rot, transform);
            }

            // Спавн койнов и сердец после поворота
            int[] posZ = { 101 , 103, 105 };
            for (int i = 2; i < 11; i++)
            {
                Vector3 position = new Vector3(0f, 0.25f, 0f);
                position.z = posZ[Random.Range(0, posZ.Length)];
                position.x = -(i * 8);
                Quaternion rot = Quaternion.Euler(0, 0, 0);
                GameObject obj = null;
                int rnd = Random.Range(0, 8);
                if (rnd == 4)
                {
                    obj = powerUps[1];
                }
                else
                {
                    obj = powerUps[0];
                }

                var instance = Instantiate(obj, position, rot, transform);
            }
        }

    }

}
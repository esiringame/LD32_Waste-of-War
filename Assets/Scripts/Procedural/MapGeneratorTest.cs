using UnityEngine;
using System.Collections;

public class MapGeneratorTest : MonoBehaviour
{
    void Start()
    {
        CaseData[][] map = MapGenerator.GenerateMap(5, 10, 0);
        for (int i = 0; i < map.Length; i++)
            for (int j = 0; j < map[0].Length; j++)
                Debug.Log(map[i][j]);
    }
}

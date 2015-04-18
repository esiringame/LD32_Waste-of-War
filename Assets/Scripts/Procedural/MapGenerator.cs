using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class MapGenerator
{
    private const int SectorWidth = 5;

    private const int StonesNumberBySector = 6;
    private const int ObstaclesNumberBySector = 10;
    private const int MinesNumberBySector = 12;

    public static CaseData[][] GenerateMap(int sizeX, int sizeY, int seed)
    {
        Random.seed = seed;

        var map = new CaseData[sizeY][];
        for (int i = 0; i < sizeY; i++)
            map[i] = new CaseData[sizeX];

        var sectorGenerator = new SectorGenerator {
            Height = sizeY,
            Width = sizeX,
            NumberByType =
                {
                    {CaseData.Stone, StonesNumberBySector},
                    {CaseData.Obstacle, ObstaclesNumberBySector}
                }
        };

        for (int i = 0; i < sizeX / SectorWidth; i++)
        {
            int redMinesNumber = Random.Range(MinesNumberBySector - MinesNumberBySector / 3,
                MinesNumberBySector + MinesNumberBySector / 3);

            sectorGenerator.NumberByType[CaseData.RedMines] = redMinesNumber;
            sectorGenerator.NumberByType[CaseData.GreenMines] = MinesNumberBySector - redMinesNumber;
            sectorGenerator.GenerateSector(map, i * SectorWidth, 0);
        }

        return map;
    }

    private class SectorGenerator
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Dictionary<CaseData, int> NumberByType { get; set; }

        public SectorGenerator()
        {
            NumberByType = new Dictionary<CaseData, int>();
        }

        public void GenerateSector(CaseData[][] map, int originSectorX, int originSectorY)
        {
            foreach (KeyValuePair<CaseData, int> pair in NumberByType)
            {
                CaseData type = pair.Key;
                int count = pair.Value;

                if (type == CaseData.EmptyCase)
                    continue;

                for (int i = 0; i < count; i++)
                {
                    int x, y;
                    int visitedCount = 0;

                    do
                    {
                        y = Random.Range(0, Height);
                        x = Random.Range(0, Width);

                        if (visitedCount >= Width * Height)
                            throw new Exception("Too much special cases considering the sector size !");
                    }
                    while (map[originSectorY + y][originSectorX + x] != CaseData.EmptyCase);

                    map[originSectorY + y][originSectorX + x] = type;
                }
            }
        }
    }
}

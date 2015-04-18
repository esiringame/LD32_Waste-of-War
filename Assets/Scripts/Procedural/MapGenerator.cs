using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class MapGenerator
{
    private const int SectorWidth = 5;

    private const int StonesNumberBySector = 6;
    private const int ObstaclesNumberBySector = 7;
    private const int MinesNumberBySector = 12;

    public static CaseData[][] GenerateMap(int sizeX, int sizeY, int seed)
    {
        Random.seed = seed;

        var map = new CaseData[sizeY][];
        for (int i = 0; i < sizeY; i++)
            map[i] = new CaseData[sizeX];

        // Begin

        var sectorGenerator = new SectorGenerator {
            Height = sizeY,
            Width = 3,
            NumberByType =
                {
                    {CaseData.Obstacle, ObstaclesNumberBySector}
                }
        };

        sectorGenerator.GenerateSector(map, 0, 0);

        // Center

        sectorGenerator = new SectorGenerator {
            Height = sizeY,
            Width = SectorWidth,
            NumberByType =
                {
                    {CaseData.Obstacle, ObstaclesNumberBySector},
                    {CaseData.Stone, StonesNumberBySector}
                }
        };

        for (int i = 0; i < sizeX / SectorWidth - 1; i++)
        {
            int redMinesNumber = Random.Range(MinesNumberBySector / 2 - MinesNumberBySector / 3,
                MinesNumberBySector / 2 + MinesNumberBySector / 3);

            sectorGenerator.NumberByType[CaseData.RedMines] = redMinesNumber;
            sectorGenerator.NumberByType[CaseData.GreenMines] = MinesNumberBySector - redMinesNumber;
            sectorGenerator.GenerateSector(map, 3 + i * SectorWidth, 0);
        }

        // End

        sectorGenerator = new SectorGenerator {
            Height = sizeY,
            Width = 2
        };

        sectorGenerator.GenerateSector(map, 3 + (sizeX / SectorWidth - 1) * SectorWidth, 0);

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
                        visitedCount++;

                        if (visitedCount >= Width * Height)
                            throw new Exception(string.Format("Can't place a {0} case considering the conditions !", type));
                    }
                    while (map[originSectorY + y][originSectorX + x] != CaseData.EmptyCase || !ConditionsByCaseType(map, type, originSectorX + x, originSectorY + y));

                    map[originSectorY + y][originSectorX + x] = type;
                }
            }
        }

        private bool ConditionsByCaseType(CaseData[][] map, CaseData type, int x, int y)
        {
            switch (type)
            {
                case CaseData.Obstacle:

                    for (int i = -1; i <= 1 ; i++)
                        for (int j = -1; j <= 1; j++)
                            if (y + i >= 0 && x + j >= 0 && y + i < map.Length && x + j < map[0].Length)
                                if (map[y + i][x + j] == CaseData.Obstacle)
                                    return false;

                    break;
            }

            return true;
        }
    }
}

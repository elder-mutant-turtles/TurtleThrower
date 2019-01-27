#if UNITY_EDITOR
    using UnityEditor;
#endif
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Tilemaps;
    using Game.Scripts;

    public class AutoTile8 : AutoTileBase
    {
        public Sprite[] Sprites = new Sprite[64];

        private static readonly Vector3Int NorthWest = Vector3Int.up + Vector3Int.left; //0
        private static readonly Vector3Int North = Vector3Int.up; //1
        private static readonly Vector3Int NorthEast = Vector3Int.up + Vector3Int.right; //2
        private static readonly Vector3Int West = Vector3Int.left; //3
        private static readonly Vector3Int East = Vector3Int.right; //4
        private static readonly Vector3Int SouthWest = Vector3Int.down + Vector3Int.left; //5
        private static readonly Vector3Int South = Vector3Int.down; //6
        private static readonly Vector3Int SouthEast = Vector3Int.down + Vector3Int.right; //7

        private static readonly Dictionary<int, int> MaskMap = new Dictionary<int, int>()
        {
            {2, 1}, {8, 2}, {10, 26}, {11, 29}, {16, 4}, {18, 24}, {22, 27}, {24, 6}, {26, 25}, {27, 40}, {30, 41},
            {31, 28}, {64, 3}, {66, 5}, {72, 10}, {74, 18}, {75, 35}, {80, 8}, {82, 16}, {86, 34}, {88, 9}, {90, 17},
            {91, 38}, {94, 39}, {95, 14}, {104, 13}, {106, 43}, {107, 21}, {120, 32}, {122, 46}, {123, 30}, {126, 23},
            {127, 36}, {208, 11}, {210, 42}, {214, 19}, {216, 33}, {218, 47}, {219, 15}, {222, 31}, {223, 37},
            {248, 12}, {250, 22}, {251, 44}, {254, 45}, {255, 20}, {0, 7},
        };

        public override void RefreshTile(Vector3Int location, ITilemap tilemap)
        {
            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    var offsetPosition = location + new Vector3Int(x, y, 0);
                    //if (IsAutoTile(tilemap,offsetPosition)) {
                    tilemap.RefreshTile(offsetPosition);
                    //}
                }
            }
        }

        public override void GetTileData(Vector3Int location, ITilemap tilemap,
            ref TileData tileData)
        {
            var north = IsAutoTile(tilemap, location + North);
            var east = IsAutoTile(tilemap, location + East);
            var west = IsAutoTile(tilemap, location + West);
            var south = IsAutoTile(tilemap, location + South);

            var mask = north ? 1 << 1 : 0;
            mask += west ? 1 << 3 : 0;
            mask += east ? 1 << 4 : 0;
            mask += south ? 1 << 6 : 0;

            if (north && west)
            {
                mask += IsAutoTile(tilemap, location + NorthWest) ? 1 << 0 : 0;
            }

            if (north && east)
            {
                mask += IsAutoTile(tilemap, location + NorthEast) ? 1 << 2 : 0;
            }

            if (south && west)
            {
                mask += IsAutoTile(tilemap, location + SouthWest) ? 1 << 5 : 0;
            }

            if (south && east)
            {
                mask += IsAutoTile(tilemap, location + SouthEast) ? 1 << 7 : 0;
            }

            if (!MaskMap.ContainsKey(mask))
            {
                Debug.Log(mask);
                return;
            }

            mask = MaskMap[mask];

            if (mask >= Sprites.Length || mask < 0)
            {
                Debug.LogWarning("Not enough sprites!");
            }
            else
            {
                tileData.sprite = Sprites[mask];
                tileData.color = color;
                tileData.colliderType = colliderType;
                tileData.flags = flags;
            }
        }

#if UNITY_EDITOR
// The following is a helper that adds a menu item to create a RoadTile Asset
        [MenuItem("Assets/Create/Tiles/AutoTile8")]
        public static void CreateTile()
        {
            CreateTileAsset<AutoTile8>();
        }
#endif
    }
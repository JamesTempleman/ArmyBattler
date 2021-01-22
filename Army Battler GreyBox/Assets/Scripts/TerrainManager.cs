using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{

    //size of map
    public int HorizontalTiles = 25;
    public int VerticalTiles = 25;

    //random key, works like seed in minecraft
    public int key = 1;

    //map offset
    public Vector2 MapOffset;

    //stores all terrain deets and building deets
    public TerrainType[] TerrainTypes;
    public Sprite[] Buildings;

    //chance for ruins to spawn
    public float ruinsChance = 0.80f;

    private IEnumerable<Marker> _markers;
    private List<GameObject> _buildings = new List<GameObject>();


    public Vector2 WorldToMapPosistion(Vector3 worldPosition)
    {
        if (worldPosition.x < 0)
        {
            worldPosition.x--;
        }
        if (worldPosition.y < 0)
        {
            worldPosition.y--;
        }

        return new Vector2((int)(worldPosition.x + MapOffset.x), (int)(worldPosition.y + MapOffset.y));
    }

    public TerrainType SelectTerrain(float x, float y)
    {
        return Marker.Closest(_markers, new Vector2(x, y), key).Terrain;
    }

    void LoadRuins(Marker marker)
    {
        if (!marker.Isruins)
        {
            return;
        }

        var buildingCount = Randomhelper.Range(marker.Location, key, marker.RuinsSize * 4) + marker.RuinsSize;

        for (int i = 0; i < buildingCount; i++)
        {

            var buildings = new GameObject();
            _buildings.Add(buildings);

            buildings.transform.position = new Vector3(
                marker.Location.x - marker.RuinsSize + Randomhelper.Range(marker.Location, key + i, marker.RuinsSize * 2),
                marker.Location.y - marker.RuinsSize + Randomhelper.Range(marker.Location, key - i, marker.RuinsSize * 2),
                0.1f);

            var renderer = buildings.AddComponent<SpriteRenderer>();

            renderer.sprite = Buildings[Randomhelper.Range(
                buildings.transform.position,
                key,
                Buildings.Length
                )];

            buildings.transform.parent = transform;
            buildings.name = "Building " + buildings.transform.position;
        }
    }

    void Awake()
    {

        int sortIndex = 0;

        var offset = new Vector3(0 - HorizontalTiles / 2, 0 - VerticalTiles / 2, 0);

        for (int x = 0; x < HorizontalTiles; x++)
        {
            for (int y = 0; y < VerticalTiles; y++)
            {

                var tile = new GameObject();
                tile.transform.position = new Vector3(x, y, 0) + offset;

                _markers = Marker.GetMarkers(transform.position.x, transform.position.y, key, TerrainTypes, ruinsChance);

                var spriteRenderer = tile.AddComponent<SpriteRenderer>();

                spriteRenderer.sortingOrder = sortIndex--;
                var terrain = SelectTerrain(offset.x + x, offset.y + y);
                spriteRenderer.sprite = terrain.GetTile(offset.x + x, offset.y + y, key);

                tile.name = "Terrain " + tile.transform.position;
                tile.transform.parent = transform;

                var animator = spriteRenderer.gameObject.GetComponent<Animator>();

                if (terrain.IsAnimated)
                {
                    if (animator == null)
                    {
                        animator = spriteRenderer.gameObject.AddComponent<Animator>();
                        animator.runtimeAnimatorController = terrain.AnimationController;

                    }
                }
                else
                {
                    if (animator != null)
                    {
                        GameObject.Destroy(animator);
                    }
                }
            }
        }

        _buildings.ForEach(x => Destroy(x));
        _buildings.Clear();
        foreach (var marker in _markers)
            LoadRuins(marker);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

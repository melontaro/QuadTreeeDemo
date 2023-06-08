using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TestTilemapManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    void Start()
    {
        var cellBounds = tilemap.cellBounds;
        var size = tilemap.size;
        var cellSize = tilemap.cellSize;
        string str = string.Format("size->{0},cellSize->{1}", size, cellSize);
        Debug.LogError(str);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RandomSetTilemap()
    {
        var i = Random.Range(0, 10);
        var j = Random.Range(0, 10);
        tilemap.SetTile(new Vector3Int(i, j), null);
    }

    public void RandomSetTilemap2()
    {
        var i = 2;
        var j = 2;

        tilemap.SetTile(new Vector3Int(i, j), null);
        var tilebase = AssetDatabase.LoadAssetAtPath<TileBase>("Assets/DontStarve/Tile/tileset-sliced_224.asset");
        tilemap.SetTile(new Vector3Int(i, j), tilebase);
    }
}
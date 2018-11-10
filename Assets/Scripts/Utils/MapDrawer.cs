using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Controllers;
using Model;
using UnityEngine;

public class MapDrawer : MonoBehaviour
{
    public GameController GameController;
    public GameObject TilePrefab;
    public GameObject Map;
    public MapStructure MapStructure;
    public float InstantiateDelay;
    public int InstantiateRatio;
    public int MapSize;
    public Vector2 TileSpriteScale;
    public Vector2 TileOffsetSpace;

    private Vector2 _pointScale;

    private float _timeCounter;
    private Stack<Cell> _tiles;
    private List<CellController> _goTiles;
    private List<Cell> _tilesList;

    // Use this for initialization
    void Start()
    {
        _pointScale = TileOffsetSpace;
        _tiles = new Stack<Cell>();
        _goTiles = new List<CellController>();
        _tilesList = new List<Cell>();
        for (int i = 0; i < MapSize; i++)
        {
            for (int j = 0; j < MapSize; j++)
            {
                if ((i + j) % 2 == 0)
                {
                    _tiles.Push(new Cell(new Vector2(i - MapSize / 2, j - MapSize / 2), Color.white));
                }
                else
                {
                    _tiles.Push(new Cell(new Vector2(i - MapSize / 2, j - MapSize / 2), Color.black));
                }

                //_tiles.Push(new Tile(new Vector2(i, j)));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        _timeCounter += Time.deltaTime;
        if (_timeCounter > InstantiateDelay)
        {
            _timeCounter = 0;
            for (int i = 0; i < InstantiateRatio; i++)
            {
                if (_tiles.Count > 0)
                {
                    Cell t = _tiles.Pop();
                    GameObject go = Instantiate(TilePrefab);
                    go.transform.SetParent(Map.transform);
                    go.name = "Tile: " + t.Position.x + "," + t.Position.y;
                    SpriteRenderer sr = go.GetComponentInChildren<SpriteRenderer>();
                    go.transform.localScale = new Vector3(TileSpriteScale.x * 1.0f / sr.bounds.size.x,
                        TileSpriteScale.y * 1.0f / sr.bounds.size.y, 1f);
                    switch (MapStructure)
                    {
                        case MapStructure.Square:
                            go.transform.position = new Vector3(t.Position.x * _pointScale.x,
                                t.Position.y * _pointScale.x, Constants.CellDepth);
                            break;
                        case MapStructure.Hexagon:
                            go.transform.position = HexagonalCoordinates(t.Position);
                            break;
                    }

                    go.GetComponentInChildren<CellController>().Cell = t;
                    _goTiles.Add(go.GetComponentInChildren<CellController>());
                    t.CellController = go.GetComponentInChildren<CellController>();
                    _tilesList.Add(t);
                }
                else
                {
                    //OnMapDrawingEnds();
                    Destroy(this);
                    break;
                }
            }
        }
    }

    private Vector3 HexagonalCoordinates(Vector2 position)
    {
        return new Vector3(position.x * _pointScale.x + position.y * _pointScale.y / 2f,
            position.y * _pointScale.y * 3f / 4f, Constants.CellDepth);
    }

    private void OnMapDrawingEnds()
    {
        Graph<Cell> g = Graph<Cell>.GenerateGraph(_tilesList);
        GameController.MapCreated(_goTiles, _tilesList, g);
    }
}

public enum MapStructure
{
    Square,
    Hexagon
}
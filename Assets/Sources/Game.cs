using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using sheepgame;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
    private Map _map;

    public GameObject PlayerPrefab;
    public GameObject SheepPrefab;
    public GameObject FirPrefab;
    public GameObject FencePrefab;
    public GameObject RockPrefab;
    public GameObject WaterPrefab;

    private GameObject[,] _goGrid;
    public int Width;
    public int Height;
//    public float Scale = 0.8f;
    public GameObject Map;

	void Awake ()
	{
        _map = new Map(Width, Height);
        _goGrid = new GameObject[Width,Height];
        _map.AddObject(0, 0, new Player());
        _map.AddObject(0, 10, new Rock());
        _map.AddObject(10, 10, new Rock());
        _map.AddObject(10, 0, new Rock());

        _map.AddObject(10, 10, new Sheep());
        _map.AddObject(5, 5, new Rock());

        _map.AddObject(4, 6, new Fence());
        _map.AddObject(8, 3, new Fir());
        _map.AddObject(7, 2, new Fir());
        _map.AddObject(5, 6, new Fir());
        _map.AddObject(2, 5, new Fir());
        _map.AddObject(2, 7, new Fence());

        Spawn();

	    player = GetComponent<PlayerMovementAppearance>();

	    PlayerMovementAppearance.finished += () =>
	    {
	        isMoving = false;
//	        Debug.Log("finished moving");
	    };
	}

    public GameObject shepard;

    public void Spawn()
    {
        for (var y = 0; y < Height; ++y)
        {
            for (var x = 0; x < Width; ++x)
            {
                var mapobject = _map.grid[x, y].contains;
                if(mapobject == null) 
                    continue;
                var glyph = mapobject.DrawGlyph();
                switch (glyph)
                {
                    case 'C':
                        _goGrid[x, y] = Instantiate(PlayerPrefab) as GameObject;
                        shepard = _goGrid[x, y];
                        break;

                    case 'S':
                        _goGrid[x, y] = Instantiate(SheepPrefab) as GameObject;
                        break;

                    case 'X':
                        _goGrid[x, y] = Instantiate(RockPrefab) as GameObject;
                        break;

                    case 'T':
                        _goGrid[x, y] = Instantiate(FirPrefab) as GameObject;
                        break;

                    case 'F':
                        _goGrid[x, y] = Instantiate(FencePrefab) as GameObject;
                        break;

                    case 'W':
                        _goGrid[x, y] = Instantiate(WaterPrefab) as GameObject;
                        break;
                }
                _goGrid[x, y].transform.position = new Vector3(x * 2f, 0, y * 2f);
            }
        }
    }

    private PlayerMovementAppearance player;

    public float startTurnTime;

    public bool isMoving = false;

	void Update ()
	{
	    if (isMoving)
	        return;

	    Move();
	}

    private float scale = 1.435f;

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(!_map.StartMovement(Direction.MoveSouth(1)))
                return;
            player.goTo(shepard.transform.position + new Vector3(0, 0, -1 * scale), PlayerMovementAppearance.direction.down);
          
            startTurnTime = Time.time;
            Debug.Log("Move Down");
            isMoving = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
             if(!_map.StartMovement(Direction.MoveNorth(1)))
                 return;
            player.goTo(shepard.transform.position + new Vector3(0, 0, 1 * scale), PlayerMovementAppearance.direction.up);
            startTurnTime = Time.time;
            Debug.Log("Move Up");
            isMoving = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!_map.StartMovement(Direction.MoveWest(1)))
                return;
            player.goTo(shepard.transform.position + new Vector3(-1 * scale, 0, 0), PlayerMovementAppearance.direction.left);
            
            startTurnTime = Time.time;
            Debug.Log("Move Left");
            isMoving = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!_map.StartMovement(Direction.MoveEast(1)))
                return;
            player.goTo(shepard.transform.position + new Vector3(1 * scale, 0, 0), PlayerMovementAppearance.direction.right);
            startTurnTime = Time.time;
            Debug.Log("Move Right");
            isMoving = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameScript : MonoBehaviour {

    public GameObject StartEmpty;
    public GameObject Cube;

    public Material RedMat;
    public Material NotOwned;
    public Material BlueMat;
    public Material Highlighted;

    public class GameBlock
    {
        public GameObject North;
        public GameObject East;
        public GameObject South;
        public GameObject West;
        public GameObject[] Cubes { get
            {
                return new GameObject[] { North, East, South, West };
            } }
        public ClickyThingsScript[] Scripts { get
            {
                return Cubes.Select(x => x.GetComponent<ClickyThingsScript>()).ToArray();
            } }
        public GameObject Cover; // thing that goes on top
        public WhoPlayer PlayerOwned = WhoPlayer.None;

        public Vector2 Position;
        public GameBlock(int x, int y)
        {
            Position = new Vector2(x, y);
        }
        public void SetPlayer(WhoPlayer play)
        {
            PlayerOwned = play;
            var asd = GameObject.FindGameObjectWithTag("MainManager");
            var scrp = asd.GetComponent<GameScript>();
            Material mat = scrp.NotOwned;
            if(play == WhoPlayer.Player1)
            {
                mat = scrp.RedMat;
            } else if (play == WhoPlayer.Player2)
            {
                mat = scrp.BlueMat;
            } 
            North.GetComponent<Renderer>().material = mat;
            East.GetComponent<Renderer>().material = mat;
            South.GetComponent<Renderer>().material = mat;
            West.GetComponent<Renderer>().material = mat;
        }
    }
    public enum WhoPlayer
    {
        None = 0,
        Player1 = 1,
        Player2 = 2
    }

    private List<GameObject> allCubes = new List<GameObject>();

    List<Vector3> removed = new List<Vector3>()
    {
        new Vector3(0,0,0),
        new Vector3(1,0,0),
        new Vector3(2,0,0),
        new Vector3(2,0,0),
        new Vector3(3,0,0),
        new Vector3(3,1,0),
        new Vector3(3,1,1),
        new Vector3(3,1,2),
        new Vector3(3,1,3)
    };

    public GameObject GetCube(int x, int z)
    {
        return GetCube(new Vector3(x, 0, z));
    }
    public GameObject GetCube(int x, int y, int z)
    {
        return GetCube(new Vector3(x, y, z));
    }
    public GameObject GetCube(Vector3 loc)
    {
        Debug.LogWarning("Finding: " + loc.ToString());
        foreach(var cube in allCubes)
        {
            string name = cube.name;
            int x = -1;
            int y = 0;
            int z = -1;
            string[] split = name.Split(',');
            if(split[1].Contains("-"))
            {
                y = 1;
                split[1] = split[1].Substring(0, 1);
            }
            x = int.Parse(split[0]);
            z = int.Parse(split[1]);
            Debug.Log("For '" + cube.name + "': " + x.ToString() + ", " + y.ToString() + ", " + z.ToString());
            if(x == loc.x)
            {
                if(y == loc.y)
                {
                    if(z == loc.z)
                    {
                        return cube;
                    }
                }
            }
        }
        return null;
    }

    private List<GameBlock> allBlocks = new List<GameBlock>();

    // Use this for initialization
    void Start () {
		for(int x = 0; x < 4; x++)
        {
            for(int z = 0; z < 4; z++)
            {
                Vector3 nv = new Vector3(x,0, z);
                if (!removed.Contains(nv))
                {
                    // Cubes going horizontally
                    string name = x.ToString() + "," + z.ToString();
                    var newC = Instantiate(Cube);
                    newC.transform.SetParent(StartEmpty.transform);
                    float xPos = (x * 5);
                    float zPos = 2.5f + (z * 5);
                    Vector3 posAll = new Vector3(xPos, 0.5f, zPos);
                    newC.transform.localPosition = posAll;
                    newC.name = name;
                    newC.AddComponent<ClickyThingsScript>();
                    allCubes.Add(newC);
                }
                nv = new Vector3(x, 1, z);
                if(!removed.Contains(nv))
                {
                    // Cubes going vertically
                    var vnewC = Instantiate(Cube);
                    vnewC.name = x.ToString() + "," + z.ToString() + "-V";
                    vnewC.transform.SetParent(StartEmpty.transform);
                    float xPos = 2.5f + (x * 5);
                    float zPos = 20f - (z * 5);
                    Vector3 posAll = new Vector3(xPos, 0.5f, zPos);
                    vnewC.transform.localPosition = posAll;
                    vnewC.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                    vnewC.AddComponent<ClickyThingsScript>();
                    allCubes.Add(vnewC);
                }
            }
        }
        var cube00 = new GameBlock(0, 0);
        cube00.North = GetCube(0, 0, 1);
        cube00.East = GetCube(0, 1, 3);
        cube00.South = GetCube(1, 0, 1);
        cube00.West = GetCube(0, 1, 2);
        allBlocks.Add(cube00);
        var cube01 = new GameBlock(0, 1);
        cube01.North = GetCube(0, 2);
        cube01.East = GetCube(0, 1, 1);
        cube01.South = GetCube(1, 2);
        cube01.West = GetCube(0, 1, 2);
        allBlocks.Add(cube01);
        var cube02 = new GameBlock(0, 2);
        cube02.North = GetCube(0, 3);
        cube02.East = GetCube(0, 1, 0);
        cube02.South = GetCube(1, 3);
        cube02.West = GetCube(0, 1, 1);
        allBlocks.Add(cube02);
        var cube10 = new GameBlock(1, 0);
        cube10.North = GetCube(1, 1);
        cube10.East = GetCube(1, 1, 2);
        cube10.South = GetCube(2, 1);
        cube10.West = GetCube(1, 1, 3);
        allBlocks.Add(cube10);
        var cube11 = new GameBlock(1, 1);
        cube11.North = GetCube(1, 2);
        cube11.East = GetCube(1, 1, 1);
        cube11.South = GetCube(2, 2);
        cube11.West = GetCube(1, 1, 2);
        allBlocks.Add(cube11);
        var cube12 = new GameBlock(1, 2);
        cube12.North = GetCube(1, 3);
        cube12.East = GetCube(1, 1, 0);
        cube12.South = GetCube(2, 3);
        cube12.West = GetCube(1, 1, 1);
        allBlocks.Add(cube12);
        var cube20 = new GameBlock(2, 0);
        cube20.North = GetCube(2, 1);
        cube20.East = GetCube(2, 1, 2);
        cube20.South = GetCube(3, 1);
        cube20.West = GetCube(2, 1, 3);
        allBlocks.Add(cube20);
        var cube21 = new GameBlock(2, 1);
        cube21.North = GetCube(2, 2);
        cube21.East = GetCube(2, 1, 1);
        cube21.South = GetCube(3, 2);
        cube21.West = GetCube(2, 1, 2);
        allBlocks.Add(cube21);
        var cube22 = new GameBlock(2, 2);
        cube22.North = GetCube(2, 3);
        cube22.East = GetCube(2, 1, 0);
        cube22.South = GetCube(3, 3);
        cube22.West = GetCube(2, 1, 1);
        allBlocks.Add(cube22);
        foreach (var bl in allBlocks)
        {
            bl.SetPlayer(WhoPlayer.None);
            foreach(var cb in bl.Scripts)
            {
                cb.Init(Highlighted, RedMat, BlueMat, NotOwned, bl);
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}

using UnityEngine;
using Assets.Scripts.Utility;

public class GameController : MonoBehaviour {

    private GameObject cube;
    private float camSpeed = 5;
    private cubeType[,,] worldLayout = new cubeType[200, 20, 200];
    // Use this for initialization
    void Start () {
        cube = Resources.Load("Prefabs/cube") as GameObject;
        // Instantiate floor level
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                GameObject baseCube = Instantiate(cube, new Vector3(i, 0, j), Quaternion.identity) as GameObject;
                baseCube.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Grass") as Material;
                worldLayout[i, 0, j] = cubeType.GRASS;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        //Camera movement keyboard
        // Works at any rotation
        if (Input.GetKey(KeyCode.W))
        {
            Camera.main.transform.Translate(new Vector3(0, 1, 0) * camSpeed / 10);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Camera.main.transform.Translate(new Vector3(0, -1, 0) * camSpeed / 10);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Camera.main.transform.Translate(new Vector3(1, 0, 1) * -camSpeed / 10);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Camera.main.transform.Translate(new Vector3(1, 0, 1) * camSpeed / 10);
        }
    }

    private void instantiateBlueprint(cubeType[,,] blueprint, Vector3 baseVector)
    {
        for (int i = 0; i < blueprint.GetLength(0); i++)
        {
            for (int j = 0; j < blueprint.GetLength(1); j++)
            {
                for (int k = 0; k < blueprint.GetLength(2); k++)
                {
                    GameObject tempStruc = null;
                    switch (blueprint[i, j, k])
                    {
                        case cubeType.NONE:
                            //Air
                            break;
                        case cubeType.WOOD:
                            tempStruc = Instantiate(cube, baseVector + new Vector3(i, j, k), Quaternion.identity) as GameObject;
                            tempStruc.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Wood") as Material;
                            worldLayout[i, j, k] = cubeType.WOOD;
                            break;
                        case cubeType.GRASS:
                            tempStruc = Instantiate(cube, baseVector + new Vector3(i, j, k), Quaternion.identity) as GameObject;
                            tempStruc.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Grass") as Material;
                            worldLayout[i, j, k] = cubeType.GRASS;
                            break;
                    }
                }
            }
        }
    }
}

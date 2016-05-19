﻿using UnityEngine;
using Assets.Scripts.Utility;
using System.Text;

public class TestController : MonoBehaviour {

    private GameObject cube;
    private float camSpeed = 5;
    private cubeType[,,] worldLayout = new cubeType[200,20,200];

    private cubeType[,,] blueprint;
    // Use this for initialization
    void Start () {
        cube = Resources.Load("Prefabs/cube") as GameObject;
        // Instantiate floor level
        for (int i = 0; i < 100; i++)
        {
            for(int j = 0; j <100; j++)
            {
                GameObject baseCube = Instantiate(cube, new Vector3(i, 0, j), Quaternion.identity) as GameObject;
                baseCube.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Grass") as Material;
                worldLayout[i, 0, j] = cubeType.GRASS;
            }
        }

        // Instantiate test building
        Vector3 baseVector = new Vector3(3, 1, 3);
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                GameObject tempStruc = Instantiate(cube, baseVector + new Vector3(i * 2, j, 0), Quaternion.identity) as GameObject;
                tempStruc.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Wood") as Material;
            }
        }

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                GameObject tempStruc = Instantiate(cube, baseVector + new Vector3(i * 2, j, 2), Quaternion.identity) as GameObject;
                tempStruc.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Wood") as Material;
            }
        }
        
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameObject tempStruc = Instantiate(cube, baseVector + new Vector3(i, 2, j), Quaternion.identity) as GameObject;
                tempStruc.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Wood") as Material;
            }
        }

        //BlueprintTest
        blueprint = new cubeType[3,3,3];
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                blueprint[i * 2, j, 0] = cubeType.WOOD;
            }
        }

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                blueprint[i * 2, j, 2] = cubeType.WOOD;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                blueprint[i, 2, j] = cubeType.WOOD;
            }
        }
        instantiateBlueprint(blueprint, new Vector3(7,1,3));
    }

    // Update is called once per frame
    void Update () {
        //Camera movement keyboard
        // Works at any rotation
        if (Input.GetKey(KeyCode.W))
        {
            Camera.main.transform.Translate(new Vector3(0,1,0) * camSpeed / 10);
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

        //Raycast
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                Debug.Log(objectHit.name);
                Debug.Log(objectHit.GetComponent<MeshRenderer>().material.name);

                if (objectHit.GetComponent<MeshRenderer>().material.name == "Grass (Instance)")
                {
                    instantiateBlueprint(blueprint, objectHit.transform.position+ new Vector3(0,1,0));
                }
                // Do something with the object that was hit by the raycast.
            }
        }

        //Rotate 
        //Does not work yet
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Camera.main.transform.Rotate(Vector3.forward, 90);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Camera.main.transform.Rotate(Vector3.forward, -90);
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
                    Debug.Log(worldLayout[i+ (int)baseVector.x, j+ (int)baseVector.y, k+(int)baseVector.z]);
                    switch (blueprint[i, j, k])
                    {
                        case cubeType.NONE:
                            //Air
                            break;
                        case cubeType.WOOD:
                            if (worldLayout[i + (int)baseVector.x, j + (int)baseVector.y, k + (int)baseVector.z] == cubeType.NONE)
                            {
                                tempStruc = Instantiate(cube, baseVector + new Vector3(i, j, k), Quaternion.identity) as GameObject;
                                tempStruc.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Wood") as Material;
                                worldLayout[i + (int)baseVector.x, j + (int)baseVector.y, k + (int)baseVector.z] = cubeType.WOOD;
                            }
                            break;
                        case cubeType.GRASS:
                            if (worldLayout[i + (int)baseVector.x, j + (int)baseVector.y, k + (int)baseVector.z] == cubeType.NONE)
                            {
                                tempStruc = Instantiate(cube, baseVector + new Vector3(i, j, k), Quaternion.identity) as GameObject;
                                tempStruc.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Grass") as Material;
                                worldLayout[i + (int)baseVector.x, j + (int)baseVector.y, k + (int)baseVector.z] = cubeType.GRASS;
                            }
                            break;
                    }
                }
            }
        }
    }
}

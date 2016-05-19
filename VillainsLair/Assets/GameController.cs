using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    private float camSpeed = 5;
	// Use this for initialization
	void Start () {
        GameObject cube = Resources.Load("Prefabs/cube") as GameObject;
        for (int i = 0; i < 100; i++)
        {
            for(int j = 0; j <100; j++)
            {
                Instantiate(cube, new Vector3(i, 0, j), Quaternion.identity);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        //Camera movement keyboard
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

        //Rotate 
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Camera.main.transform.Rotate(Vector3.forward, 90);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Camera.main.transform.Rotate(Vector3.up, 90);
        }
    }
}

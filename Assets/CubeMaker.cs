using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class CubeMaker : MonoBehaviour {
    MeshFilter meshFilter;
    Mesh mesh;
    BoxMeshCreator mc = new BoxMeshCreator();

    public Vector3 size = Vector3.one;
    public float offset = 1.2f;
    float noiseAmt = 0;

	// Use this for initialization
	void Start () {
        meshFilter = GetComponent<MeshFilter>();


    }
	
	// Update is called once per frame
	void Update () {
        mc.Clear();
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                Vector3 noiseVec = new Vector3((i * offset) / 10 , (j * offset) / 10, noiseAmt);
                float height = Perlin.Noise(noiseVec);
                CreateCube(new Vector3(i, 0, j), height);
            }
            meshFilter.mesh = mc.CreateMesh();
        }
        noiseAmt += .02f;
    }

    void CreateCube(Vector3 cubePos, float height)
    {
        Vector3 cubeSize = size * 0.5f;

        Vector3 t0 = new Vector3(cubeSize.x, height * 1.5f, -cubeSize.z) + cubePos;
        Vector3 t1 = new Vector3(-cubeSize.x, height * 1.5f, -cubeSize.z) + cubePos;
        Vector3 t2 = new Vector3(-cubeSize.x, height * 1.5f, cubeSize.z) + cubePos;
        Vector3 t3 = new Vector3(cubeSize.x, height * 1.5f, cubeSize.z) + cubePos;

        Vector3 b0 = new Vector3(cubeSize.x, -1, -cubeSize.z) + cubePos;
        Vector3 b1 = new Vector3(-cubeSize.x, -1, -cubeSize.z) + cubePos;
        Vector3 b2 = new Vector3(-cubeSize.x, -1, cubeSize.z) + cubePos;
        Vector3 b3 = new Vector3(cubeSize.x, -1, cubeSize.z) + cubePos;

        //Top Square
        mc.BuildTriangle(t0, t1, t2);
        mc.BuildTriangle(t0, t2, t3);

        //Bottom Square
        mc.BuildTriangle(b2, b1, b0);
        mc.BuildTriangle(b3, b2, b0);

        //Back Square
        mc.BuildTriangle(b0, t1, t0);
        mc.BuildTriangle(b0, b1, t1);

        mc.BuildTriangle(b1, t2, t1);
        mc.BuildTriangle(b1, b2, t2);

        mc.BuildTriangle(b2, t3, t2);
        mc.BuildTriangle(b2, b3, t3);

        mc.BuildTriangle(b3, t0, t3);
        mc.BuildTriangle(b3, b0, t0);
    }
}

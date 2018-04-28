using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMeshCreator {

    private List<Vector3> vertices = new List<Vector3>();

    private List<Vector3> normals = new List<Vector3>();
    private List<Vector2> uvs = new List<Vector2>();

    private List<Color32> colors = new List<Color32>();

    private List<int> triangleIndices = new List<int>();

    private Mesh mesh;

    public BoxMeshCreator()
    {
        
    }

    public void BuildTriangle(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2, Vector3 normal)
    {
        int v0Index = vertices.Count;
        int v1Index = vertices.Count + 1;
        int v2Index = vertices.Count + 2;

        vertices.Add(vertex0);
        vertices.Add(vertex1);
        vertices.Add(vertex2);

        normals.Add(normal);
        normals.Add(normal);
        normals.Add(normal);

        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 1));
        uvs.Add(new Vector2(0, 1));

        triangleIndices.Add(v0Index);
        triangleIndices.Add(v1Index);
        triangleIndices.Add(v2Index);

        //colors.Add(Color32.Lerp(Color.red, Color.green, vertex0.y));
        //colors.Add(Color32.Lerp(Color.red, Color.green, vertex1.y));
        //colors.Add(Color32.Lerp(Color.red, Color.green, vertex2.y));
    }

    public void BuildTriangle(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2)
    {
        Vector3 normal = Vector3.Cross(vertex1 - vertex0, vertex2 - vertex0).normalized;
        BuildTriangle(vertex0, vertex1, vertex2, normal);
    }

    public Mesh CreateMesh()
    {
        if (mesh == null)
        {
            mesh = new Mesh();
        }
        mesh.Clear();

        mesh.vertices = vertices.ToArray();

        mesh.normals = normals.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.triangles = triangleIndices.ToArray();

        //Color32[] colors = new Color32[vertices.Count];
        //for (int i = 0; i < vertices.Count; i++)
        //    colors[i] = Color32.Lerp(Color.red, Color.green, vertices[i].y);

        //mesh.colors32 = colors;
        //mesh.RecalculateNormals();
        return mesh;
    }

    public void Clear()
    {
        if (mesh != null)
        {
            mesh.Clear();
        }

        vertices.Clear();

        normals.Clear();

        uvs.Clear();

        triangleIndices.Clear();
    }
}

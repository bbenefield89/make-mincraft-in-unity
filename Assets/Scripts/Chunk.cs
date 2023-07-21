using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [Tooltip("Stores materials, lightning, etc.")]
    public MeshRenderer meshRenderer;

    [Tooltip("Stores data for the mesh (vertex locations, etc.)")]
    public MeshFilter meshFilter;

    private int vertexIndex = 0;
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Vector2> uvs = new List<Vector2>();
    private bool[,,] voxelMap = new bool[VoxelData.chunkWidth, VoxelData.chunkHeight, VoxelData.chunkWidth];

    void Start()
    {
        PopulateVoxelMap();
        CreateMeshData();
        CreateMesh();
    }

    /// <summary>
    /// Populates a 3d-array that allows us to check if a voxel face has been rendered in that position
    /// </summary>
    void PopulateVoxelMap()
    {
        for (int y = 0; y < VoxelData.chunkHeight; y++)
        {
            for (int x = 0; x < VoxelData.chunkWidth; x++)
            {
                for (int z = 0; z < VoxelData.chunkWidth; z++)
                {
                    voxelMap[x, y, z] = true;
                }
            }
        }
    }

    /// <summary>
    /// Iterates through coords to later check if a voxel exists in that position
    /// </summary>
    void CreateMeshData()
    {
        for (int y = 0; y < VoxelData.chunkHeight; y++)
        {
            for (int x = 0; x < VoxelData.chunkWidth; x++)
            {
                for (int z = 0; z < VoxelData.chunkWidth; z++)
                {
                    AddVoxelDataToChunk(new Vector3(x, y, z));
                }
            }
        }
    }

    /// <summary>
    /// Creates the data for a mesh if that voxel does not already exist
    /// </summary>
    void AddVoxelDataToChunk(Vector3 pos)
    {
        for (int i = 0; i < 6; i++)
        {
            if (!CheckVoxel(pos + VoxelData.faceChecks[i]))
            {
                vertices.Add(pos + VoxelData.voxelVerts[VoxelData.voxelTris[i, 0]]);
                vertices.Add(pos + VoxelData.voxelVerts[VoxelData.voxelTris[i, 1]]);
                vertices.Add(pos + VoxelData.voxelVerts[VoxelData.voxelTris[i, 2]]);
                vertices.Add(pos + VoxelData.voxelVerts[VoxelData.voxelTris[i, 3]]);
                uvs.Add(VoxelData.voxelUvs[0]);
                uvs.Add(VoxelData.voxelUvs[1]);
                uvs.Add(VoxelData.voxelUvs[2]);
                uvs.Add(VoxelData.voxelUvs[3]);
                triangles.Add(vertexIndex);
                triangles.Add(vertexIndex + 1);
                triangles.Add(vertexIndex + 2);
                triangles.Add(vertexIndex + 2);
                triangles.Add(vertexIndex + 1);
                triangles.Add(vertexIndex + 3);
                vertexIndex += 4;
            }
        }
    }

    /// <summary>
    /// Checks if a voxel already exists in the given position
    /// </summary>
    bool CheckVoxel(Vector3 pos)
    {
        int x = Mathf.FloorToInt(pos.x);
        int y = Mathf.FloorToInt(pos.y);
        int z = Mathf.FloorToInt(pos.z);

        if (x < 0 || x > VoxelData.chunkWidth - 1 || y < 0 || y > VoxelData.chunkHeight - 1 || z < 0 || z > VoxelData.chunkWidth - 1)
        {
            return false;
        }
        else
        {
            return voxelMap[x, y, z];
        }
    }

    /// <summary>
    /// Creates and renders mesh on voxel
    /// </summary>
    void CreateMesh()
    {
        Mesh mesh = new Mesh();  // Create a new mesh to lay over our voxel
        mesh.vertices = vertices.ToArray();  // Supply the vertices
        mesh.triangles = triangles.ToArray();  // Supply the vertice indexes to create a triangle
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();  // Fixes the direction in which verts are facing to help unity render lightning correctly

        meshFilter.mesh = mesh;  // Applies the mesh to be rendered in game
    }
}

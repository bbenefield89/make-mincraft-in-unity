using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VoxelData
{
    [Tooltip("Width of a chunk")]
    public static readonly int chunkWidth = 5;

    [Tooltip("Height of a chunk")]
    public static readonly int chunkHeight = 5;

    [Tooltip("All the vertices that make up a voxel")]
    public static readonly Vector3[] voxelVerts = new Vector3[8]
    {
        new Vector3(0.0f, 0.0f, 0.0f),
        new Vector3(1.0f, 0.0f, 0.0f),
        new Vector3(1.0f, 1.0f, 0.0f),
        new Vector3(0.0f, 1.0f, 0.0f),
        new Vector3(0.0f, 0.0f, 1.0f),
        new Vector3(1.0f, 0.0f, 1.0f),
        new Vector3(1.0f, 1.0f, 1.0f),
        new Vector3(0.0f, 1.0f, 1.0f),
    };

    [Tooltip("Array that lets us know which direction a face of a voxel should check to see if it is visible" +
        "Visibility is determined by whether it is touching the face of another voxel or not" +
        "If it is not touching a voxel then we render it")]
    public static readonly Vector3[] faceChecks = new Vector3[6]
    {
        new Vector3(0f, 0f, -1f),
        new Vector3(0f, 0f, 1f),
        new Vector3(0f, 1f, 0f),
        new Vector3(0f, -1f, 0f),
        new Vector3(-1f, 0f, 0f),
        new Vector3(1f, 0f, 0f),
    };

    [Tooltip("An array of voxel faces made up of vertices.\n" +
        "Each index inside an inner-array represents the index of a vertice in voxelVerts.\n" +
        "i.e. voxelTris[0,0] says the first vertice of the first voxel face is the first index in voxelVerts")]
    public static readonly int[,] voxelTris = new int[6, 4]
    {
        {0, 3, 1, 2},  // Back Face
        {5, 6, 4, 7},  // Front Face
        {3, 7, 2, 6},  // Top Face
        {1, 5, 0, 4},  // Bottom Face
        {4, 7, 0, 3},  // Left Face
        {1, 2, 5, 6},  // Right Face
    };

    [Tooltip("Array of coordinates explaining how to map a texture to a mesh" +
        "Bottom left = 0, 0" +
        "Top Left = 0, 1" +
        "Top Right = 1, 0" +
        "Bottom Right = 1, 1")]
    public static readonly Vector2[] voxelUvs = new Vector2[4]
    {
        new Vector2(0.0f, 0.0f),
        new Vector2(0.0f, 1.0f),
        new Vector2(1.0f, 0.0f),
        new Vector2(1.0f, 1.0f),
    };
}

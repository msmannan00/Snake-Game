using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtils 
{


    public static void SwapMaterialsAndMesh(GameObject CopyTo,GameObject CopyFrom)
    {
        MeshFilter _mMeshFilter = CopyTo.GetComponent<MeshFilter>();
        _mMeshFilter.sharedMesh = CopyFrom.GetComponent<MeshFilter>().sharedMesh;
        MeshRenderer _mMeshRenderer = CopyTo.GetComponent<MeshRenderer>();
        _mMeshRenderer.sharedMaterials = CopyFrom.GetComponent<MeshRenderer>().sharedMaterials;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InventoryItem))]
public class InventoryItemEditor : Editor
{
    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {

        return base.RenderStaticPreview(assetPath, subAssets, width, height);
    }
}
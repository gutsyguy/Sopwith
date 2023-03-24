using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerSettings : ScriptableObject
{
    private static float UV_SPACE = 0.0625f;

    public string tag = "Untagged";
    public Vector2 primaryUV = new Vector2(0.0f,15.0f);
    public Vector2 secondaryUV = new Vector2(0.0f, 7.0f);

    private void applyUV(Vector2 prevPrimary,Vector2 prevSecondary,Mesh mesh)
    {
        List<Vector2> uvs = new List<Vector2>();
        bool update = false;
        mesh.GetUVs(0, uvs);
        Debug.Log("Searching " + uvs.Count + " for " + prevPrimary + " and " + prevSecondary);
        for(int i=0;i<uvs.Count;i++)
        {
            Vector2 uv = uvs[i];
            if (uv.x >= prevPrimary.x && uv.x < prevPrimary.x + UV_SPACE &&
               uv.y >= prevPrimary.y && uv.y < prevPrimary.y + UV_SPACE)
            {
                update = true;
                uvs[i] = new Vector2(
                    (uv.x - prevPrimary.x) + primaryUV.x * UV_SPACE,
                    (uv.y - prevPrimary.y) + primaryUV.y * UV_SPACE);
                Debug.Log(uv + "->" + uvs[i]);
            }

            if (uv.x >= prevSecondary.x && uv.x < prevSecondary.x + UV_SPACE &&
               uv.y >= prevSecondary.y && uv.y < prevPrimary.y + UV_SPACE)
            {
                update = true;
                uvs[i] = new Vector2(
                    (uv.x - prevSecondary.x) + secondaryUV.x * UV_SPACE,
                    (uv.y - prevSecondary.y) + secondaryUV.y * UV_SPACE);
                Debug.Log(uv + "->" + uvs[i]);
            }

        }
        if (update)
        {
            Debug.Log("Updating UVs");
            mesh.SetUVs(0, uvs);
        }
    }

    public void ApplySettings(PlayerSettings previousPlayer,Transform parent)
    {
        if(previousPlayer == this)
        {
            Debug.Log("Skipping Identity Transformation");
            return;
        }
        Debug.Log("" + previousPlayer.tag + "->" + tag);
        Debug.Log("Going from " + previousPlayer.primaryUV + " to " + primaryUV+" and "+previousPlayer.secondaryUV+" to "+secondaryUV);

        if(parent.TryGetComponent<MeshFilter>(out var meshFilter))
        {
            applyUV(previousPlayer.primaryUV*UV_SPACE,previousPlayer.secondaryUV*UV_SPACE,meshFilter.mesh);
        }
        foreach(Transform child in parent)
        {
            ApplySettings(previousPlayer,child);
        }
    }

    public void ApplySettings(Transform parent)
    {
        ApplySettings(ScriptableObject.CreateInstance<PlayerSettings>(), parent);
    }
}

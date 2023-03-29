using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CleanableSurface : MonoBehaviour
{
    Texture2D dirtyTexture;
    [SerializeField] Texture2D cleanTexture;
    [SerializeField] Texture2D brush;
    void Start()
    {
        dirtyTexture = GetComponent<Renderer>().material.mainTexture as Texture2D;
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit raycast);
        if (raycast.collider == null) return;
        CleanObject(raycast.textureCoord);
    }


    /// <summary>
    /// Clean the object at the specified x and y
    /// </summary>
    /// <param name="dirtyObject"></param>
    public void CleanObject(Vector2 textureCoord)
    {
        Vector2 pixelCoord = new Vector2(textureCoord.x * dirtyTexture.width, textureCoord.y * dirtyTexture.height);
        Vector2Int pixelPosition = new Vector2Int(Mathf.RoundToInt(pixelCoord.x), Mathf.RoundToInt(pixelCoord.y));

        int startX = pixelPosition.x - Mathf.FloorToInt(brush.width / 2f);
        int startY = pixelPosition.y - Mathf.FloorToInt(brush.height / 2f);

        // Loop through the brush pixels and set the corresponding pixels on the texture
        for (int x = 0; x < brush.width; x++)
        {
            for (int y = 0; y < brush.height; y++)
            {
                int pixelX = startX + x;
                int pixelY = startY + y;

                // Make sure the pixel is within the bounds of the texture
                if (pixelX >= 0 && pixelX < dirtyTexture.width && pixelY >= 0 && pixelY < dirtyTexture.height)
                {
                    Color brushColor = brush.GetPixel(x, y);
                    Color pixelColor = dirtyTexture.GetPixel(pixelX, pixelY);

                    // Mix the brush color with the existing pixel color
                    Color mixedColor = Color.Lerp(pixelColor, brushColor, brushColor.a);

                    // Set the pixel on the texture
                    dirtyTexture.SetPixel(pixelX, pixelY, mixedColor);
                }
            }
        }

        dirtyTexture.Apply();
    }

}

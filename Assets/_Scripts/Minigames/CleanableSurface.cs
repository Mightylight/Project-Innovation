using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;

public class CleanableSurface : MonoBehaviour
{
    [SerializeField] Texture2D dirtyTexture;
    [SerializeField] Texture2D interactableTexture;
    [SerializeField] Texture2D cleanTexture;
    [SerializeField] Texture2D brush;
    public bool _hasBeenCleaned;

    // Update is called once per frame
    //void Update()
    //{
    //    Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit raycast);
    //    if (raycast.collider == null) return;
    //    CleanObject(raycast.textureCoord);
    //}


    /// <summary>
    /// Clean the object at the specified x and y
    /// </summary>
    /// <param name="dirtyObject"></param>
    public void CleanObject(Vector2 textureCoord)
    {
        if (_hasBeenCleaned == false)
        {
            _hasBeenCleaned = true;
            MinigameFSM.Instance.NextState();
        }
        Vector2 pixelCoord = new Vector2(textureCoord.x * interactableTexture.width, textureCoord.y * interactableTexture.height);
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
                if (pixelX >= 0 && pixelX < interactableTexture.width && pixelY >= 0 && pixelY < interactableTexture.height)
                {
                    Color brushColor = brush.GetPixel(x, y);
                    Color pixelColor = interactableTexture.GetPixel(pixelX, pixelY);
                    Color cleanColor = cleanTexture.GetPixel(pixelX, pixelY);
                    // Mix the brush color with the existing pixel color
                    Color mixedColor = Color.Lerp(pixelColor, cleanColor, brushColor.a);

                    // Set the pixel on the texture
                    interactableTexture.SetPixel(pixelX, pixelY, mixedColor);
                }
            }
        }

        interactableTexture.Apply();
    }
    private void OnDisable()
    {
        interactableTexture.SetPixels(dirtyTexture.GetPixels());
        interactableTexture.Apply();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SuperHelper : MonoBehaviour
{

    /// <summary>
    /// Returns whether the pointer is over the UI
    /// </summary>
    /// <returns></returns>
    public static bool IsPointerOverUI() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return true;
        }
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current); 
        pointerEventData.position =  Input.mousePosition; 
        List<RaycastResult> hits = new List<RaycastResult>(); 
        EventSystem.current.RaycastAll( pointerEventData, hits ); 
        return hits.Count > 0;
    }

    /// <summary>
    /// Converts Canvas position to World position
    /// </summary>
    /// <param name="rectTransform">The transform of the UI Position</param>
    /// <returns></returns>
    private static Vector2 CanvasToWorldPoint(RectTransform rectTransform)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, rectTransform.position, Camera.main, out var result);
        return result;
    }
    
    /// <summary>
    /// Converts World position to UI position
    /// </summary>
    /// <param name="screenPosition"></param>
    /// <param name="worldCamera">current camera, when left open will grab Camera.main</param>
    /// <returns></returns>
    public static Vector3 GetWorldPositionFromUI(Vector3 screenPosition, Camera worldCamera = null) {
        if (worldCamera == null) { worldCamera = Camera.main; }
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    
    /// <summary>
    /// Destroys all children of specified Transform
    /// </summary>
    /// <param name="parent"></param>
    public static void DestroyChildren(Transform parent) {
        foreach (Transform transform in parent)
            Destroy(transform.gameObject);
    }
}

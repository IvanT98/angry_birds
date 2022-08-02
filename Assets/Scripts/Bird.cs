using UnityEngine;

public class Bird : MonoBehaviour
{
    private void OnMouseDrag()
    {
        var mainCamera = Camera.main;

        if (!mainCamera)
        {
            return;
        }
        
        var mousePosition = Input.mousePosition;
        var newPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        transform.position = new Vector3(newPosition.x, newPosition.y, 0);
    }
}

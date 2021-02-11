using UnityEngine;

namespace UnityTemplateProjects.GameUI.MouseCursor
{
    public class SetMouseCursor : MonoBehaviour
    {
        public CursorMode cursorMode = CursorMode.Auto;
        public Vector2 hotSpot = Vector2.zero;
        
        public void SetCursor(Texture2D pCursorTexture2D)
        {
            Cursor.SetCursor(pCursorTexture2D, hotSpot, cursorMode);
        }
        
        public void SetDefaultCursor()
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}
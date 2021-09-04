using UnityEngine.EventSystems;

namespace HeathenEngineering.UX
{
    /// <summary>
    /// Sets the stae of the cursor on enter and mouse down and reverts to default on exit
    /// </summary>
    public class ButtonCursorState : MouseOverCursorState, IPointerDownHandler, IPointerClickHandler
    {
        public CursorState stateOnClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (stateOnEnter)
                CursorSystem.SetState(stateOnEnter, false, holdOnMouseDown);
            else
                CursorSystem.SetDefault();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            CursorSystem.SetState(stateOnClick, true, holdOnMouseDown);
        }
    }
}

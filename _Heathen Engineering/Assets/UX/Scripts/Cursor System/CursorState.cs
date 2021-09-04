using HeathenEngineering.Events;
using UnityEngine;

namespace HeathenEngineering.UX
{
    [CreateAssetMenu(menuName = "UX/Cursor/State")]
    public class CursorState : GameEvent<bool>
    {
        public Vector2 hotSpot = Vector2.zero;
        public CursorAnimation Animation;

        public bool Active
        {
            get => CursorSystem.CurrentState == this;
            set
            {
                if (value)
                    CursorSystem.SetState(this);
                else if (CursorSystem.CurrentState == this)
                    CursorSystem.SetDefault();
            }
        }

        public void SetState(bool setOnHold = false, bool holdOnDown = false) => CursorSystem.SetState(this, setOnHold, holdOnDown);

        public void MakeDefault() => CursorSystem.DefaultState = this;
    }
}

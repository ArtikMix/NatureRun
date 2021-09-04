using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace HeathenEngineering.UX
{
    /// <summary>
    /// Updates the animation state of the <see cref="CursorSettings"/>
    /// </summary>
    public class CursorSystem : MonoBehaviour
    {
        public CursorState defaultState;
        public CursorMode cursorMode = CursorMode.Auto;

        /// <summary>
        /// The settings object that will be activated on enable of the system.
        /// </summary>
        public static CursorSystem current;

        private static bool holdIfDown = false;
        private static bool holding = false;
        private static CursorState nextState = null;
        private static bool nextHoldingState = false;

        private void OnEnable()
        {
            current = this;
        }

        public void LateUpdate()
        {
#if ENABLE_INPUT_SYSTEM
            // New input system backends are enabled.
            if (holding && !Mouse.current.leftButton.isPressed)
            {
                holding = false;

                if (nextState != null)
                    SetState(nextState, false, nextHoldingState);
                else
                    SetDefault();
            }
            else if (holdIfDown && Mouse.current.leftButton.isPressed)
                holding = true;
#else
            // Old input backends are enabled.
            if (holding && !Input.GetMouseButton(0))
            {
                holding = false;

                if (nextState != null)
                    SetState(nextState, false, nextHoldingState);
                else
                    SetDefault();
            }
            else if (holdIfDown && Input.GetMouseButton(0))
                holding = true;
#endif

            var deltaTime = Time.unscaledDeltaTime;

            if (current == null)
            {
                Debug.LogError("Attempted to animate cursor before the cursor system has been intialized.");
                return;
            }

            if (currentState == null)
                currentState = current.defaultState;

            if (currentState == null)
                return;

            Cursor.visible = Visible;
            Cursor.lockState = LockState;

            current.frameTimer -= deltaTime;
            if (current.frameTimer <= 0f)
            {
                current.frameTimer += (1f / currentState.Animation.framesPerSecond);
                Cursor.SetCursor(currentState.Animation.textureArray[current.currentFrame], currentState.hotSpot, current.cursorMode);

                if (currentState.Animation.loop || current.currentFrame < currentState.Animation.textureArray.Length - 1)
                    current.currentFrame = (current.currentFrame + 1) % currentState.Animation.textureArray.Length;
            }
        }

        public static CursorState DefaultState
        {
            get
            {
                if (current)
                    return current.defaultState;
                else
                    return null;
            }
            set
            {
                if (current != null)
                    current.defaultState = value;
            }
        }

        private static CursorState currentState;
        public static CursorState CurrentState
        {
            get
            {
                if (current)
                    return currentState;
                else
                    return null;
            }
            set => SetState(value);
        }

        public static bool Visible { get => Cursor.visible; set => Cursor.visible = value; }
        public static CursorLockMode LockState { get => Cursor.lockState; set => Cursor.lockState = value; }
        
        public static CursorMode CursorMode
        {
            get
            {
                if (current)
                    return current.cursorMode;
                else
                    return default;
            }
            set
            {
                if (current)
                    current.cursorMode = value;
            }
        }

        private int currentFrame;
        private float frameTimer;

        public static void SetDefault() => SetState(DefaultState);

        public static void SetState(CursorState state, bool setOnHold = false, bool holdOnDown = false)
        {
            if (holding && !setOnHold)
            {
                nextHoldingState = holdOnDown;
                nextState = state;
                return;
            }

            holdIfDown = holdOnDown;

            if (current == null)
            {
                //Exit quietly the expectation is that if current is null then we arn't using the cursor system
                return;
            }
            else if (state == null && state != current.defaultState)
            {
                SetState(current.defaultState);
            }
            else if (state == null && current.defaultState == null)
            {
                Debug.LogError("Attempted to set the Cursor State to a null value.\nThis is an unsupported action, you can only call SetState with a null state if a defualt state is defined on the active CursorSettings object.");
            }


            var pState = CurrentState;
            currentState = state ? state : current.defaultState;

            current.currentFrame = 0;
            current.frameTimer = (1f / currentState.Animation.framesPerSecond);

            Cursor.SetCursor(currentState.Animation.textureArray[current.currentFrame], currentState.hotSpot, current.cursorMode);

            if (pState != null && pState != state)
                pState.Invoke(current, false);

            if (currentState != null)
                currentState.Invoke(current, true);
        }
    }
}

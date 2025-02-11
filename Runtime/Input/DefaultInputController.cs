﻿#if ENABLE_INPUT_SYSTEM

using System;
using System.Collections.Generic;
using VPG.Core.Configuration;
using UnityEngine;
using UnityEngine.InputSystem;

namespace VPG.Core.Input
{
    /// <summary>
    /// Input controller based on Unity's InputSystem.
    /// </summary>
    [RequireComponent(typeof(PlayerInput))]
    public class DefaultInputController : InputController
    {
        private string defaultActionMap;

        private PlayerInput playerInput;

        /// <summary>
        /// Focus the given input focus target.
        /// </summary>
        public override void Focus(IInputFocus target)
        {
            if (target == CurrentInputFocus)
            {
                return;
            }

            CurrentInputFocus = target;
            if (string.IsNullOrEmpty(target.ActionMapName) == false)
            {
                playerInput.SwitchCurrentActionMap(target.ActionMapName);
            }

            target.OnFocus();
            OnFocused?.Invoke(this, new InputFocusEventArgs(target));
        }

        /// <summary>
        /// Releases the focus, if possible.
        /// </summary>
        public override void ReleaseFocus()
        {
            if (CurrentInputFocus != null)
            {
                CurrentInputFocus.OnReleaseFocus();

                CurrentInputFocus = null;
                playerInput.SwitchCurrentActionMap(defaultActionMap);

                OnFocusReleased?.Invoke(this, new InputFocusEventArgs(null));
            }
        }

        protected void OnEnable()
        {
            playerInput.onActionTriggered += OnActionTriggered;
            defaultActionMap = playerInput.defaultActionMap;
        }

        protected void OnDisable()
        {
            playerInput.onActionTriggered -= OnActionTriggered;
        }

        /// <summary>
        /// Internal method handling all actions triggered by the new input system.
        /// </summary>
        protected virtual void OnActionTriggered(InputAction.CallbackContext context)
        {
            if (context.action.triggered == false || ListenerDictionary.ContainsKey(context.action.name) == false)
            {
                return;
            }

            List<ListenerInfo> infoList = ListenerDictionary[context.action.name];

            foreach (ListenerInfo info in infoList)
            {
                try
                {
                    if (CurrentInputFocus != null && info.ActionListener.IgnoreFocus == false && info.ActionListener != CurrentInputFocus)
                    {
                        break;
                    }

                    info.Action(new InputEventArgs(context));
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex);
                }
            }
        }

        protected override void Setup()
        {
            playerInput = GetComponent<PlayerInput>();
            playerInput.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
            playerInput.actions = RuntimeConfigurator.Configuration.CurrentInputActionAsset;
        }
    }
}
#endif

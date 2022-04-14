using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using UCARPG.Utilities;

namespace UCARPG.View.Input
{
    public class PlayerInputReceiver : MonoBehaviour
    {
        [SerializeField]
        private EventBusProvider _eventBusProvider;
        [SerializeField]
        private PlayerInput _playerInput;
        [SerializeField]
        private EventSystem _eventSystem;
        [SerializeField]
        private Transform _camera;
        private Stack<string> _pastActionMaps;
        private Selectable _lastSelectable;

        public void OnPlayerActorDeathStarted()
        {
            OnCloseInventoryInputTriggered(new InputAction.CallbackContext());
        }

        private void Awake()
        {
            _pastActionMaps = new Stack<string>();
            foreach (var action in _playerInput.actions)
            {
                switch (action.name)
                {
                    case "Direction":
                        action.performed += OnDirectionInputTriggered;
                        action.canceled += OnDirectionInputTriggered;
                        break;
                    case "Run":
                        action.performed += OnRunInputTriggered;
                        action.canceled += OnRunInputTriggered;
                        break;
                    case "Dodge":
                    case "Attack":
                    case "Cast":
                    case "Pickup":
                    case "Use":
                        action.performed += OnActionInputTriggered;
                        break;
                    case "OpenGameMenu":
                        action.performed += OnOpenGameMenuInputTriggered;
                        break;
                    case "CloseGameMenu":
                        action.performed += OnCloseGameMenuInputTriggered;
                        break;
                    case "OpenInventory":
                        action.performed += OnOpenInventoryInputTriggered;
                        break;
                    case "CloseInventory":
                        action.performed += OnCloseInventoryInputTriggered;
                        break;
                    case "CancelInventory":
                        action.performed += OnCancelInventoryInputTriggered;
                        break;
                    case "SwitchSelectedConsumable":
                        action.performed += OnSwitchSelectedConsumableInputTriggered;
                        break;
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var action in _playerInput.actions)
            {
                switch (action.name)
                {
                    case "Direction":
                        action.performed -= OnDirectionInputTriggered;
                        action.canceled -= OnDirectionInputTriggered;
                        break;
                    case "Run":
                        action.performed -= OnRunInputTriggered;
                        action.canceled -= OnRunInputTriggered;
                        break;
                    case "Dodge":
                    case "Attack":
                    case "Cast":
                    case "Pickup":
                    case "Use":
                        action.performed -= OnActionInputTriggered;
                        break;
                    case "OpenGameMenu":
                        action.performed -= OnOpenGameMenuInputTriggered;
                        break;
                    case "CloseGameMenu":
                        action.performed -= OnCloseGameMenuInputTriggered;
                        break;
                    case "OpenInventory":
                        action.performed -= OnOpenInventoryInputTriggered;
                        break;
                    case "CloseInventory":
                        action.performed -= OnCloseInventoryInputTriggered;
                        break;
                    case "CancelInventory":
                        action.performed -= OnCancelInventoryInputTriggered;
                        break;
                    case "SwitchSelectedConsumable":
                        action.performed -= OnSwitchSelectedConsumableInputTriggered;
                        break;
                }
            }
        }

        private void OnDirectionInputTriggered(InputAction.CallbackContext context)
        {
            Vector2 original = context.ReadValue<Vector2>();
            Vector3 baseOnCamera = original.x * _camera.right + original.y * _camera.forward;
            baseOnCamera.y = 0;
            baseOnCamera.Normalize();
            _eventBusProvider.Instance.Post(new DirectionInputTriggered(baseOnCamera.x, baseOnCamera.z));
        }

        private void OnRunInputTriggered(InputAction.CallbackContext context)
        {
            _eventBusProvider.Instance.Post(new RunInputTriggered(context.performed));
        }

        private void OnActionInputTriggered(InputAction.CallbackContext context)
        {
            _eventBusProvider.Instance.Post(new ActionInputTriggered(context.action.name));
        }

        private void OnOpenGameMenuInputTriggered(InputAction.CallbackContext context)
        {
            _pastActionMaps.Push(_playerInput.currentActionMap.name);
            _playerInput.SwitchCurrentActionMap("GameMenu");
            _lastSelectable = _eventSystem.currentSelectedGameObject?.GetComponent<Selectable>();
            _eventBusProvider.Instance.Post(new OpenGameMenuInputTriggered());
        }

        private void OnCloseGameMenuInputTriggered(InputAction.CallbackContext context)
        {
            _playerInput.SwitchCurrentActionMap(_pastActionMaps.Pop());
            _lastSelectable?.Select();
            _eventBusProvider.Instance.Post(new CloseGameMenuInputTriggered());
        }

        private void OnOpenInventoryInputTriggered(InputAction.CallbackContext context)
        {
            _pastActionMaps.Push(_playerInput.currentActionMap.name);
            OpenInventoryInputTriggered openInventoryInputTriggered = new OpenInventoryInputTriggered();
            _eventBusProvider.Instance.Post(openInventoryInputTriggered);
            if (openInventoryInputTriggered.Result)
            {
                _playerInput.SwitchCurrentActionMap("Inventory");
            }
        }

        private void OnCloseInventoryInputTriggered(InputAction.CallbackContext context)
        {
            CloseInventoryInputTriggered closeInventoryInputTriggered = new CloseInventoryInputTriggered();
            _eventBusProvider.Instance.Post(closeInventoryInputTriggered);
            if (closeInventoryInputTriggered.Result)
            {
                _playerInput.SwitchCurrentActionMap(_pastActionMaps.Pop());
            }
        }

        private void OnCancelInventoryInputTriggered(InputAction.CallbackContext context)
        {
            _eventBusProvider.Instance.Post(new CancelInventoryInputTriggered());
        }

        private void OnSwitchSelectedConsumableInputTriggered(InputAction.CallbackContext context)
        {
            _eventBusProvider.Instance.Post(new SwitchSelectedConsumableInputTriggered(context.ReadValue<float>() >= 0));
        }
    }
}
using DUMFUC.Tests;
using Il2CppInterop.Runtime.Attributes;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;
using Logger = BepInEx.Logging.Logger;

namespace DUMFUC.Input;

public class InputSystem : MonoBehaviour
{
    private List<InputAction> _inputActions = new();
    private Action _inputActionDelegateChain = () => { };
    
    static InputSystem() => ClassInjector.RegisterTypeInIl2Cpp<InputSystem>();
    
    public static InputSystem Instance = null!;

    public static void Initialize()
    {
    }

    public void RegisterInputAction(InputAction action)
    {
        if (_inputActions.Contains(action)) return;
        
        _inputActions.Add(action);
        RebuildInputActionDelegateChain();
    }

    private void Awake()
    {
        Logger.CreateLogSource("InputSystem").LogDebug("InputSystem Awake called.");
        Instance = this;
        
        _inputActions = DiscoverInputActions();
        RebuildInputActionDelegateChain();
        Logger.CreateLogSource("InputSystem").LogDebug("InputSystem Awake finished.");
    }

    private void Start()
    {
        InputSystemTests.Run();
    }

    private void Update()
    {
        _inputActionDelegateChain.Invoke();
    }

    [HideFromIl2Cpp]
    private List<InputAction> DiscoverInputActions()
    {
        // todo
        return new List<InputAction> { };
    }
    
    [HideFromIl2Cpp]
    private void RebuildInputActionDelegateChain()
    {
        _inputActionDelegateChain = () => { };
        
        foreach (var action in _inputActions) 
            _inputActionDelegateChain += action.Tick;
    }
}
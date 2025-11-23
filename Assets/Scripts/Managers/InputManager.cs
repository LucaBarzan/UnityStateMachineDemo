using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    public InputActionAsset Input => inputActionAsset;
    public InputActionMap UIMap => actionMap_UI;
    public InputActionMap GlobalMap => actionMap_Global;
    public InputActionMap PlayerMap => actionMap_Player;

    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private string actionMapName_Global = "Global";
    [SerializeField] private string actionMapName_UI = "UI";
    [SerializeField] private string actionMapName_Player = "Player";

    private InputActionMap actionMap_UI;
    private InputActionMap actionMap_Global;
    private InputActionMap actionMap_Player;

    protected override void Awake()
    {
        base.Awake();

        actionMap_UI = inputActionAsset.FindActionMap(actionMapName_UI);
        actionMap_Global = inputActionAsset.FindActionMap(actionMapName_Global);
        actionMap_Player = inputActionAsset.FindActionMap(actionMapName_Player);

        //  Global bindings must always be active
        GlobalMap.Enable();
    }

    private void OnEnable()
    {
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDisable()
    {
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.Menu:
                UIMap.Enable();
                PlayerMap.Disable();
                Debug.Log("Disable player input");
                break;

            default:
            case GameState.InGame:
                UIMap.Disable();
                PlayerMap.Enable();
                Debug.Log("Enable player input");
                break;
        }
    }
}

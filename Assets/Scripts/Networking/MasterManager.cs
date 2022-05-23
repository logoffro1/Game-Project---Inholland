using UnityEngine;

[CreateAssetMenu(menuName = "Singletons/MasterManager")]
public class MasterManager : ScriptableObjectSingleton<MasterManager> // store the game settings
{
    [SerializeField] private GameSettings gameSettings;
    public static GameSettings GameSettings { get { return Instance.gameSettings; } }
    
}

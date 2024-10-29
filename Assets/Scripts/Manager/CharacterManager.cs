using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager _instance;

    public static CharacterManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
            }
            return _instance;
        }
    }

    private Player _player;
    public Player Player { get; set; }

    private void Awake()
    {
        if (_instance != null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(_instance == this)
                Destroy(gameObject);
        }
    }
}
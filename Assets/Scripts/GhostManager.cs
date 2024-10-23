using UnityEngine;

public class GhostManager : MonoBehaviour
{
    Ghost[] _ghosts = default;

    public static GhostManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _ghosts = GetComponentsInChildren<Ghost>();
    }

    public void ToggleGhosts(float transition)
    {
        foreach (Ghost ghost in _ghosts)
        {
            ghost.Toggle(transition);
        }
    }
}

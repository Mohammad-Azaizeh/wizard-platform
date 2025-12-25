using UnityEngine;

[CreateAssetMenu(fileName = "New Telekinesis Object", menuName = "Game/Telekinesis/Object Data")]
public class TelekinesisObjectSO : ScriptableObject
{
    [Header("Object Info")]
    [SerializeField] private string objectName;
    [SerializeField] private ObjectType objectType;

    [Header("Physics Properties")]
    [SerializeField] private float mass = 1f;
    [SerializeField] private float drag = 0.5f;
    [SerializeField] private float angularDrag = 0.05f;

    [Header("Telekinesis Behavior")]
    [SerializeField] private bool canBePulled = true;
    [SerializeField] private bool canBePushed = true;
    [SerializeField] private bool canBeHeld = true;
    [SerializeField] private bool canBeRotated = true;

    [Header("Force Multipliers")]
    [SerializeField, Range(0.1f, 3f)] private float forceMultiplier = 1f;
    [SerializeField] private bool freezeRotationWhenHeld = false;

    [Header("Visual Feedback")]
    [SerializeField] private bool showHighlight = true;
    [SerializeField] private Material highlightMaterial;

    
    public string ObjectName => objectName;
    public ObjectType Type => objectType;
    public float Mass => mass;
    public float Drag => drag;
    public float AngularDrag => angularDrag;
    public bool CanBePulled => canBePulled;
    public bool CanBePushed => canBePushed;
    public bool CanBeHeld => canBeHeld;
    public bool CanBeRotated => canBeRotated;
    public float ForceMultiplier => forceMultiplier;
    public bool FreezeRotationWhenHeld => freezeRotationWhenHeld;
    public bool ShowHighlight => showHighlight;
    public Material HighlightMaterial => highlightMaterial;

    private void OnValidate()
    {
        mass = Mathf.Max(0.1f, mass);
        drag = Mathf.Max(0f, drag);
        angularDrag = Mathf.Max(0f, angularDrag);
    }
}

public enum ObjectType
{
    Platform,
    Boulder,
    Box,
    Bridge,
    Lever,
    Other
}

using UnityEngine;

[CreateAssetMenu(fileName = "New Telekinesis Ability", menuName = "Game/Telekinesis/Ability")]
public class TelekinesisAbilitySO : ScriptableObject
{
    [Header("Range")]
    [SerializeField] private float maxRange = 10f;
    [SerializeField] private float minRange = 1f;

    [Header("Force")]
    [SerializeField] private float pullForce = 15f;
    [SerializeField] private float pushForce = 15f;
    [SerializeField] private float holdForce = 20f;

    [Header("Targeting")]
    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private float aimAssistRadius = 0.5f;

    [Header("Hold Settings")]
    [SerializeField] private float holdDistance = 3f;
    [SerializeField] private float holdSmoothing = 10f;
    [SerializeField] private float maxHoldTime = 30f;

    

    [Header("Visual")]
    [SerializeField] private Color targetHighlightColor = Color.cyan;
    [SerializeField] private Color holdColor = Color.yellow;

    
    public float MaxRange => maxRange;
    public float MinRange => minRange;
    public float PullForce => pullForce;
    public float PushForce => pushForce;
    public float HoldForce => holdForce;
    public LayerMask TargetLayers => targetLayers;
    public float AimAssistRadius => aimAssistRadius;
    public float HoldDistance => holdDistance;
    public float HoldSmoothing => holdSmoothing;
    public float MaxHoldTime => maxHoldTime;
  
    public Color TargetHighlightColor => targetHighlightColor;
    public Color HoldColor => holdColor;

    private void OnValidate()
    {
        maxRange = Mathf.Max(minRange, maxRange);
        pullForce = Mathf.Max(0f, pullForce);
        pushForce = Mathf.Max(0f, pushForce);
        holdDistance = Mathf.Max(1f, holdDistance);
    }
}

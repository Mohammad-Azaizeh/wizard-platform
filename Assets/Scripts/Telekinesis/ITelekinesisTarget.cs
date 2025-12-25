using UnityEngine;


public interface ITelekinesisTarget
{
    void OnTargeted();
    void OnUntargeted();
    void OnGrabbed();
    void OnReleased();
    void ApplyPullForce(Vector2 direction, float force);
    void ApplyPushForce(Vector2 direction, float force);
    void HoldAtPosition(Vector2 targetPosition, float smoothing);
    void Rotate(float angle);
    bool CanBeAffected();
    GameObject GetGameObject();
    Rigidbody2D GetRigidbody();
    TelekinesisObjectSO GetObjectData();
}

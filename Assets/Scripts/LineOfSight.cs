using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    [SerializeField] private int distance;
    [SerializeField] private int angle;
    [SerializeField] private LayerMask obs;

    public bool IsInRange(Transform self, Transform target)
    {
        return Vector3.Distance(self.position, target.position) < distance;
    }

    public bool IsInAngle(Transform self, Transform target)
    {
        Vector3 dir = target.position - self.position;

        return Vector3.Angle(self.forward, dir) < angle / 2;
    }

    public bool HasLineOfSight(Transform self, Transform target)
    {
        Vector3 dir = target.position - self.position;
        return !Physics.Raycast(self.position, dir.normalized, dir.magnitude, obs);
    }
}

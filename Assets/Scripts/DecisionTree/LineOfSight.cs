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

    private void OnDrawGizmosSelected()
    {
        //distancia de visión
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distance);


        //águlo de visión
        Gizmos.color = Color.green;

        Vector3 leftDir = Quaternion.Euler(0, -angle / 2, 0) * transform.forward;
        Vector3 rightDir = Quaternion.Euler(0, angle / 2, 0) * transform.forward;


        Gizmos.DrawRay(
            transform.position,
            leftDir * distance
        );

        Gizmos.DrawRay(
            transform.position,
            rightDir * distance
        );
    }
}

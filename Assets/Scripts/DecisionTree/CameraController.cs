using UnityEngine;

public class CameraController : MonoBehaviour
{
    LineOfSight los;
    [SerializeField] GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Awake()
    {
        los = GetComponent<LineOfSight>();
    }

    // Update is called once per frame
    void Update()
    {
        if(los.IsInRange(transform, player.transform) &&
            los.IsInAngle(transform, player.transform) &&
            los.HasLineOfSight(transform, player.transform))
        {

        }
        else
        {

        }
    }
}

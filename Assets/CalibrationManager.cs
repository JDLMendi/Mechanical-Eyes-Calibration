using UnityEngine;
using System.Collections;

public class CalibrationManager : MonoBehaviour
{
    public GameObject target;
    public Transform[] pathPoints;
    public float speed = 2f;
    public float delay = 0.5f;

    private int currentPointIndex = 0;
    private bool movingForward = true;
    private bool isWaiting = false;

    void Update()
    {
        if (pathPoints.Length == 0 || target == null || isWaiting) return;

        Vector3 targetPosition = target.transform.position;
        Vector3 destinationPosition = pathPoints[currentPointIndex].position;

        target.transform.position = Vector3.MoveTowards(targetPosition, destinationPosition, speed * Time.deltaTime);

        if (Vector3.Distance(target.transform.position, destinationPosition) < 0.1f)
        {
            StartCoroutine(WaitAtPoint());
        }
    }

    private IEnumerator WaitAtPoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(delay);

        if (movingForward)
        {
            currentPointIndex++;
            if (currentPointIndex >= pathPoints.Length)
            {
                currentPointIndex = pathPoints.Length - 2;
                movingForward = false;
            }
        }
        else
        {
            currentPointIndex--;
            if (currentPointIndex < 0)
            {
                currentPointIndex = 1;
                movingForward = true;
            }
        }
        isWaiting = false;
    }
}

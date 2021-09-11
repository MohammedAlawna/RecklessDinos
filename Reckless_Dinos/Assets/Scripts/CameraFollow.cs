using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;

    [Range(1, 10)]
    [SerializeField] float sFactor;

    private void FixedUpdate()
    {
        if (GameManager._singletonVar._gameOver 
            || GameManager._singletonVar._gamePaused) return;
        Follow();

    }

    private void Follow()
    {
        
        Vector3 targetPos = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, targetPos
            , sFactor * Time.fixedDeltaTime);
        transform.position = smoothedPos;
    }
}

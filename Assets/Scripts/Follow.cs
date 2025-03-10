using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private bool collide;
    [SerializeField] private LayerMask mask;
    public Vector3 offset;
    [SerializeField] private Transform target;
    private Vector3 targetPos;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxDist;
    [SerializeField] private AnimationCurve dampening;
    [SerializeField] private float cameraSize;
    // Update is called once per frame
    void FixedUpdate()
    { 
	    float distance = Vector3.Distance(target.position, target.position + offset);
	    Vector3 dir = offset.normalized;
	    
	    
	    RaycastHit hit;
	    if (Physics.Raycast(target.position, dir, out hit, distance, mask))
	    {
		    targetPos = hit.point - dir.normalized * cameraSize;
	    }
        else
        {
	        targetPos = target.position + offset;
        }
	    float finalDist = Vector3.Distance(transform.position, targetPos);
	    float factor = dampening.Evaluate( finalDist > 1 ? 1 : finalDist);
	    transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * factor);
    }
}

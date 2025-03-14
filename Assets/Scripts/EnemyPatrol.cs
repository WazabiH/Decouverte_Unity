using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 3;
    public BoxCollider2D bc;
    public Animator animator;
    public LayerMask listObstacleLayers;
    public float groundCheckRadius = 0.15f;
    public bool isFacingRight = false;
    public float distanceDetection = 0.1f;

    void FixedUpdate(){
        if(rb.linearVelocity.y !=0){
            return; 
        }
        animator.SetFloat("VelocityX", Mathf.Abs(rb.linearVelocityX));
        rb.linearVelocity = new Vector2(
            moveSpeed * transform.right.normalized.x,
            rb.linearVelocity.y
        );
        if (HasNotTouchedGround() || HasCollisionWithObstacle()){
            Flip();
        }
    }
    void Flip(){
        if ((transform.right.normalized.x > 0 && !isFacingRight) ||
        (transform.right.normalized.x < 0 && isFacingRight)
        ){
            transform.Rotate(0,180,0);
            isFacingRight=!isFacingRight;
        }
    }

    bool HasNotTouchedGround(){
        Vector2 detectionPosition = new Vector2(
            bc.bounds.center.x + (transform.right.normalized.x * (bc.bounds.size.x/2)),
            bc.bounds.min.y
        );
        return !Physics2D.OverlapCircle(
            detectionPosition,
            groundCheckRadius,
            listObstacleLayers
        );
    }

    bool HasCollisionWithObstacle(){
        RaycastHit2D hit = Physics2D.Linecast(
            bc.bounds.center,
            bc.bounds.center + new Vector3(
                distanceDetection * transform.right.normalized.x,
                0,
                0
            ),
            listObstacleLayers
        );
        return hit.transform != null;
    }

    void OnDrawGizmos(){
        if (bc !=null){
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(
                bc.bounds.center,
                bc.bounds.center + new Vector3(
                distanceDetection * transform.right.normalized.x,
                0,
                0
            )
            );
        }
            Gizmos.color = Color.red;
            Vector2 detectionPosition = new Vector2(
            bc.bounds.center.x + (transform.right.normalized.x * (bc.bounds.size.x/2)),
            bc.bounds.min.y
            );
            Gizmos.DrawWireSphere(
                detectionPosition,
                groundCheckRadius
            );
    }
} 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private float angle = 70f;
    [SerializeField] private float grappleLength = 1;
    [SerializeField] private float gravityRope = 3f, gravityAir = 1f;

    private Rigidbody2D rb;
    private DistanceJoint2D joint;
    private LineRenderer line;
    private Vector2 grapplePoint;
    private Vector2 direction;
    private RaycastHit2D hit;

    private bool onSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<DistanceJoint2D>();
        line = GetComponent<LineRenderer>();
        joint.enabled = false;
        line.enabled = false;

        float rad = angle * Mathf.Deg2Rad;
        direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
        hit = GetHit();

    }

    // Update is called once per frame
    void Update()
    {
        if (!hit) return;

        if (TouchScreen.instance.click)
        {
            if (!onSound)
            {
                SoundManager8.instance.PlaySound(5);
                onSound = true;
            }

            joint.connectedAnchor = grapplePoint;
            joint.enabled = true;
            joint.distance = grappleLength;
            line.enabled = true;
            rb.gravityScale = gravityRope;
        }

        else
        {
            onSound = false;

            hit = GetHit();
            grapplePoint = hit.point;

            joint.enabled = false;
            line.enabled = false;
            rb.gravityScale = gravityAir;
        }

        DrawLine();
    }

    private RaycastHit2D GetHit()
            => Physics2D.Raycast(transform.position, direction, Mathf.Infinity, LayerMask.GetMask("Ground"));

    private void DrawLine()
    {
        if (!line.enabled) return;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, grapplePoint);
    }
}

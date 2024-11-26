using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoKitten : MonoBehaviour
{

    public Transform gizmo;
    
private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Player"))
    {
        if (gizmo != null)
        {
            var gizmoController = gizmo.GetComponent<GizmoController>();
            if (gizmoController != null)
            {
                gizmoController.enabled = true;
            }

            var animator = gizmo.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("isRun", true);
            }
        }
        else
        {
            Debug.LogWarning("Gizmo foi destruído antes de OnTriggerEnter2D ser chamado.");
        }
    }
}

private void OnTriggerExit2D(Collider2D collision)
{
    if (collision.CompareTag("Player"))
    {
        if (gizmo != null)
        {
            var gizmoController = gizmo.GetComponent<GizmoController>();
            if (gizmoController != null)
            {
                gizmoController.enabled = false;
            }

            var animator = gizmo.GetComponent<Animator>();
            animator?.SetBool("isRun", false);
        }
    }
}


}
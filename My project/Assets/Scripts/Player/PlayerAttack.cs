using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collission){

        if(collission.CompareTag("Keeper")){
            collission.GetComponent<KeeperController>().life--;
        }

        if(collission.CompareTag("Gizmo")){
            collission.GetComponent<GizmoController>().life--;
        }

}


}

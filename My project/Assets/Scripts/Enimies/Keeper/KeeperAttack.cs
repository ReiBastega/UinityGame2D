using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeperAttack : MonoBehaviour
{

private void OnTriggerEnter2D(Collider2D collission){

    if(collission.CompareTag("Player")){
        
        collission.GetComponent<PlayerController>().life--;
    }

}

}


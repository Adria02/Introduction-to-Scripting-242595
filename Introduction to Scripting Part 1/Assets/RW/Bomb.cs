using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float runSpeed; // 1
    public float gotHayDestroyDelay; // 2
    private bool hitByHay;
    private bool Fall;
    public float dropDestroyDelay; // 1
    private Collider myCollider; // 2
    private Rigidbody myRigidbody;
    private SheepSpawner sheepSpawner;
    public float heartOffset; // 1

    
    // Start is called before the first frame update
    void Start(){
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
        
                
    }

    // Update is called once per frame
    void Update(){
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);

    }
    private void HitByHay(){

        sheepSpawner.RemoveSheepFromList(gameObject);
        hitByHay = true; 
        runSpeed = 0; 
        Destroy(gameObject, 0); 
       
        TweenScale tweenScale = gameObject.AddComponent<TweenScale>();; 
        tweenScale.targetScale = 0; 
        tweenScale.timeToReachTarget = gotHayDestroyDelay;
        SoundManager.Instance.PlaySheepHitClip();
        GameStateManager.Instance.DroppedSheep();
    }
    private void OnTriggerEnter(Collider other) // 1
    {   
        if (other.CompareTag("Hay") && !hitByHay) // 2
        {   
            Destroy(other.gameObject);
            HitByHay();
             
        }
        else if (other.CompareTag("DropSheep") && !Fall)
        {
            Drop();
            Fall = true; 
        }
    }
    private void Drop()
    {
        
        sheepSpawner.RemoveSheepFromList(gameObject);
        myRigidbody.isKinematic = false; // 1
        myCollider.isTrigger = false; // 2
        Destroy(gameObject, 0); // 3
        SoundManager.Instance.PlaySheepDroppedClip();
        //GameStateManager.Instance.DroppedSheep();
    }
    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }
    
}

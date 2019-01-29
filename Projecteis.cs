using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projecteis : MonoBehaviour
{
    private EnemyController target;
    private TowerMaster parent;
    private float time,wait = 2;
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        MoveToTarget();
        if(target != null){
            // Destroy(gameObject);
        }
    }
    public void Initialize(TowerMaster parent)
    {
        this.parent = parent;
        this.target = parent.target;
        this.damage = parent.getDamage();
    }

    private void MoveToTarget()
    {
        if(target !=null && target.Vivo)
        {
            transform.position = Vector3.MoveTowards(transform.position,target.transform.position,Time.deltaTime *10);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {   
            if(target.vida>=0){
                if(this.tag == "Azul" || this.tag == "Raio" ){
                    target.vida -=damage;
                    print("está morrendo"+target.vida);
                    Destroy(gameObject);
                }
            }  
        }
    }
    public void OnTriggerExit2D(Collider2D colison)
    {
        if(colison.tag == "Range")
        {

            //Destroy(gameObject);
        }
    }
}

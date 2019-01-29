using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Transform PlayerPos;
    private bool viuPlayer = false;
    public float speed;
    private float auxSpeed;
    private float waitTime;
    public float startWaitTime;
    private Vector2 direction;
    public Transform[] PatrolPoints;
    private int RandomSpot;
    [SerializeField]
    private Stat health;
    [SerializeField]
    private GameObject Base;
    private float timing = 3f;
    private bool canAttack = false;
    public bool isAlien01 = false;
    public bool isAlien02 = false;
    public bool isAlien03 = false;
    public bool isAlien04 = false;
    private bool atacando = false;

     /*Quando quiser dar dano na base use isso
     Base.GetComponent<Base>().TakeDamage(insira o dano desejado inteiro );  */



    private Animator animator;
    public float vida;
    public bool Vivo = true;
    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        auxSpeed = speed;
        RandomSpot = Random.Range(0, PatrolPoints.Length);
        animator = GetComponent<Animator>();
        health.Inicialize(vida,vida);
    }

    // Update is called once per frame
    void Update()
    {   
        print("COOLDOWN: " + timing);
        animator.SetFloat("Horizontal",-direction.x);
        animator.SetFloat("Vertical",-direction.y);
        health.MyCurrentValue = vida;
        Morreu();
        if(Vivo){   
            if(viuPlayer){
                ChasePlayer();
            }else{
                direction = (transform.position-PatrolPoints[RandomSpot].position);
                transform.position = Vector2.MoveTowards(transform.position, PatrolPoints[RandomSpot].position, speed * Time.deltaTime);
                if(Vector2.Distance(transform.position,PatrolPoints[RandomSpot].position) < 0.2f){
                    if(waitTime <= 0){
                        int AuxRandomSpot = RandomSpot; 
                        RandomSpot = Random.Range(0, PatrolPoints.Length);
                        if(RandomSpot == AuxRandomSpot){
                            RandomSpot = Random.Range(0, PatrolPoints.Length);
                        }
                        waitTime = startWaitTime;
                        speed = auxSpeed;
                    }else{
                        waitTime -= Time.deltaTime;
                        speed = 0;
                    }
                }
            }
        }
        if(canAttack){
            if(timing == 3f){
                print("ATACANDO");
                Base.GetComponent<Base>().TakeDamage(50);
                atacando = true;
            }
            if(timing >0){
                timing -= Time.deltaTime;
            }
            if(timing <= 0){
                timing = 3;
            }
        }
        
    }

    void ChasePlayer(){
        viuPlayer = true;
        waitTime = 0;
        if(Vector2.Distance(transform.position,PlayerPos.position) < 4){
            direction = (transform.position-PlayerPos.position);
            transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, speed * Time.deltaTime);
        }else{
            viuPlayer = false;
        }
    }
    public void Morreu()
    {
        if(vida <=0)
        {
            Vivo =false;
        }
        if(!Vivo)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {   
        // print("Tag do Colisor: " + collision.gameObject.tag);
        if(collision.gameObject.tag == "Player"){
            collision.gameObject.SendMessage("Die");

        }else if(collision.gameObject.tag == "Interable"){
            if(collision.gameObject.GetComponent<Interacao>().isBase){
                if( atacando){
                    if(isAlien01){
                        collision.collider.SendMessage("TakeDamage",25);
                    }else if(isAlien02){
                        collision.collider.SendMessage("TakeDamage",50);
                    }else if(isAlien03){
                        collision.collider.SendMessage("TakeDamage",100);
                    }else if(isAlien04){
                        collision.collider.SendMessage("TakeDamage",200);
                    }
                    
                    atacando = false;
                }
                canAttack = true;
            }
            
        }
    }public void OnCollisionExit2D(Collision2D collision)
    {   
        if(collision.gameObject.tag == "Interable"){
            if(collision.gameObject.GetComponent<Interacao>().isBase){
                canAttack = false;
            }
            
        }
    }
}

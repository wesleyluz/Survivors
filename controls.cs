using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class controls : MonoBehaviour
{
    private float speed;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    private Interable isinterable;
    private Animator animator;
    public int vida;
    private int Wood = 100;
    private int Metal = 100;
    private int EnergyCells = 100;
    private int PowerTanks = 100;
    private int Cannons = 100;
    private AudioSource audioSource;
    private bool isPlaying = false;
    public AudioClip walking;
    public GameController gameController;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Cursor.visible = true;
        InvokeRepeating ("PlaySound", 0.0f, 0.5f);    


    }

    void Update()
    {

        
        if(vida > 0){
            Move();
            Onclick();
            if(isPlaying){
                audioSource.Play();
            }else{
                audioSource.Stop();
            }
            isPlaying = false;
        }
       
    }
    void Move()
    {
        isPlaying = true;
        Vector2 MoveInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        moveVelocity = MoveInput.normalized * speed;
        if(Input.GetButton("Stealth")){
            speed = 3;
		}else if(Input.GetButton("run")){
            speed = 10;
        }else{
			speed = 5;
		}
        animator.SetFloat("Vertical",MoveInput.y);
        animator.SetFloat("Horizontal",MoveInput.x);
        animator.SetFloat("Magnitude",Mathf.Abs(MoveInput.x)+Mathf.Abs(MoveInput.y)/2);
    }
    void FixedUpdate(){
        // rb.MovePosition(rb.position + moveVelocity*Time.fixedDeltaTime);
        rb.AddForce(moveVelocity*10);
    }

    void Onclick()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);
        if(isinterable!= null){
            if(Input.GetButtonDown("Fire1")){  
                if(hit.collider!=null){
                    isinterable.Interact();
                }
            }
        }else{
            if(Input.GetButtonDown("Fire1")){
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {   
        if(collider.tag == "Interable"){
            isinterable = collider.GetComponent<Interable>();
        }
        if(collider.tag == "Enemy"){
            print("Inimigo");
            collider.SendMessage("ChasePlayer");
        }
    }
    
    public void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Interable"){

            if(isinterable != null){
                isinterable.StopInteract();
                isinterable = null;
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D collision){   
        // REPELE O PLAYER QUANDO TOCA NA NEVOA
        if(collision.collider.tag == "Mist"){
            print("Mist");
            rb.AddForce(-(new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized)*4000);
        }
    }


    public void Die(){
        vida = 0;
        print("MORREU");
        Destroy(GetComponent<BoxCollider2D>());
        SceneManager.LoadScene("Death");
        speed = 0;
    }
    // get everyone
    public int getWood(){
        return Wood;
    }
    public int getMetal(){
        return Metal;
    }
    public int getEnergyCells(){
        return EnergyCells;
    }
    public int getPowerTanks(){
        return PowerTanks;
    }
    public int getCannons(){
        return Cannons;
    }
    //Set everyone
    public void setWood(int wood, bool desconto){
        if(desconto){
            Wood -= wood;
        }else{
            Wood += wood;
        }
    }
    public void setMetal(int metal, bool desconto){
        if(desconto){
            Metal -= metal;
        }else{
            Metal += metal;
        }
    }
    public void setEnergyCells(int energy, bool desconto){
        if(desconto){
            EnergyCells -= energy;
        }else{
            EnergyCells += energy;
        }
    }
    public void setPowerTanks(int power, bool desconto){
        if(desconto){
            PowerTanks -= power;
        }else{
            PowerTanks += power;
        }
    }
    public void setCannons(int cannon, bool desconto){
        if(desconto){
            Cannons -= cannon;
        }else{
            Cannons += cannon;
        }
    }
    void PlaySound () {
     if ((Input.GetButton("Vertical") || Input.GetButton("Horizontal")) && (GetComponent<SpriteRenderer>().enabled)){
           audioSource.PlayOneShot(walking);
     }else{
         audioSource.Stop();
     }
 }
}

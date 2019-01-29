using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMaster : MonoBehaviour
{
    public EnemyController target;
    private Queue<EnemyController> alvos =  new Queue<EnemyController>();
    [SerializeField]
    private GameObject TipodeProjetil;
    private GameObject instanciado; 
    private Projecteis tiro;   
    private bool canAtack = true;
    public float timeAtack;
    private float CoolDown = 3;
    public float ProjetilSpeed = 1;
    public Transform Mira;
    public float SpeedAlien01 = 2;
    public GameObject Torre;
    private int damage = 50;


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        //print(target);
        Attack();
        
    }
    
    public void Attack()
    {
        if(!canAtack)
        {
            timeAtack += Time.deltaTime;
            if(timeAtack >= CoolDown)
            {
                canAtack = true;
                timeAtack = 0;

            }
        }
    
        if(target == null && alvos.Count>0)
        {
            target = alvos.Dequeue();
        }
        if(target != null && target.Vivo)
        {   
            if(canAtack){
                Shoot();
                canAtack = false;
            }
        }
    }
    public void Shoot()
    {   
        Vector2 diferenca = transform.position - target.transform.position;
        Debug.DrawLine(transform.position,target.transform.position);
        float rotZ = Mathf.Atan2(diferenca.y,diferenca.x) * Mathf.Rad2Deg;
        instanciado = Instantiate(TipodeProjetil,transform.position,Mira.rotation);
        tiro = instanciado.GetComponent<Projecteis>();
        tiro.Initialize(this);
        print("atirou:");
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        print("TEM COISA NO TRIGGER: " + col.tag);
        if(col.tag == "Enemy")
        {

            alvos.Enqueue(col.GetComponent<EnemyController>());
            if(Torre.tag == "TorreAzul" || Torre.tag == "Eletrica"){
                Shoot();
            }
        }
    }
    public void OnTriggerStay2D(Collider2D col){
        if(col != null){
            if(col.tag == "Enemy" ){
                if(Torre.tag == "TorreAzul"){
                    Vector2 diferenca = transform.position - target.transform.position;
                    Debug.DrawLine(transform.position,target.transform.position);
                    float rotZ = Mathf.Atan2(diferenca.y,diferenca.x) * Mathf.Rad2Deg;
                    Mira.rotation = Quaternion.Euler(0f,0f,rotZ);
                }else if(Torre.tag == "TorreRoxa" || Torre.tag == "Eletrica"){
                    col.GetComponent<EnemyController>().speed = SpeedAlien01/2;
                }
            }
        }
        
    }
    
    public void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Enemy")
        {
            target = null;
            if(Torre.tag == "TorreRoxa" || Torre.tag == "Eletrica"){
                col.GetComponent<EnemyController>().speed = SpeedAlien01;
            }
        }
    }

    public void setDamage(int nivel,bool upgrade){
        if(upgrade)
        {
            damage *= nivel;
        }

    }

    public int getDamage()
    {
        return damage;
    }

    
}

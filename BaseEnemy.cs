using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class BaseEnemy : MonoBehaviour, IDamagable
{
    public PathCreator PC;
    private WaveManager WM;
    public EndOfPathInstruction End;
    public float Speed;
    public float DistanceTravelled;
    public int KillMoney;
    public int DamageToPlayer;
    public bool isSlow;
    public bool isCamo;
    private int enemyHealth;
    private int maxEnemyHealth;
    private Color color;
    public GameObject enemyToSpawn;
    private float alpha = .3f;
    private SpriteRenderer SP;
    private Animator Anim;
    [SerializeField]
    private int EnemyLevel;
    public int enemyLevel
    {
        get { return EnemyLevel; }
        set
        {
            EnemyLevel = value;
            switch (enemyLevel)
            {
                case 1:
                    if (isSlow)
                        Speed = Speed / 2;
                    else
                        Speed = 2;
                    DamageToPlayer = 1;
                    KillMoney = 1;                    
                    color = Color.red;                  
                    //Anim.SetInteger("Enemy2", 0);
                    if (isCamo)
                        color.a = alpha;
                    SP.color = color;
                    break;
                case 2:
                    if (isSlow)
                        Speed = Speed / 2;
                    else
                        Speed = 3;
                    DamageToPlayer = 2;
                    KillMoney = 2;
                    color = Color.blue;
                    //Anim.SetInteger("Enemy2", 1);
                    if (isCamo)
                        color.a = alpha;
                    SP.color = color;
                    break;
                case 3:
                    if (isSlow)
                        Speed = Speed / 2;
                    else
                        Speed = 4;
                    DamageToPlayer = 3;
                    KillMoney = 3;
                    color = Color.green;
                    //Anim.SetInteger("Enemy2", 2);
                    if (isCamo)
                        color.a = alpha;
                    SP.color = color;
                    break;
                case 4:
                    if (isSlow)
                        Speed = Speed / 2;
                    else
                        Speed = 5;
                    DamageToPlayer = 4;
                    KillMoney = 4;
                    color = Color.yellow;
                    //Anim.SetInteger("Enemy2", 3);
                    if (isCamo)
                        color.a = alpha;
                    SP.color = color;
                    break;
                case 5:
                    Speed = 2;
                    DamageToPlayer = 5;
                    KillMoney = 5;
                    color = Color.black;
                    SP.color = color;
                    enemyHealth = 15;
                    maxEnemyHealth = 1;
                    break;
                case 6:
                    Speed = 3;
                    DamageToPlayer = 6;
                    KillMoney = 6;
                    color = Color.gray;
                    SP.color = color;
                    enemyHealth = 30;
                    maxEnemyHealth = 2;
                    break;
                case 7:
                    Speed = 4;
                    DamageToPlayer = 7;
                    KillMoney = 7;
                    color = Color.white;
                    SP.color = color;
                    enemyHealth = 50;
                    maxEnemyHealth = 3;
                    break;
                case 8:
                    Speed = 5;
                    DamageToPlayer = 8;
                    KillMoney = 8;
                    color = Color.magenta;
                    SP.color = color;
                    enemyHealth = 80;
                    maxEnemyHealth = 4;
                    break;
            }
        }
    }

    void Awake()
    {
        PC = GameObject.Find("PathCreator").GetComponent<PathCreator>();
        WM = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        SP = GetComponent<SpriteRenderer>();
        //Anim = GetComponent<Animator>();
        transform.position = PC.path.GetPoint(0);
        //enemyLevel = enemyLevel;
    }

    void Update()
    {
        DistanceTravelled += Speed * Time.deltaTime;
        transform.position = PC.path.GetPointAtDistance(DistanceTravelled, End);
       // transform.rotation = PC.path.GetRotationAtDistance(DistanceTravelled, End);
       
    }

    public void TakeDamage(int Damage, bool? MoneyUpgrade)
    {

        if(enemyLevel >= 5)
        {
            enemyHealth -= Damage;
            if (enemyHealth <= 0)
            {
                WM.RemainingEnemies--;
                float currentDistance = DistanceTravelled;
                for(int i = 0; i <= 4; i++)
                {
                    GameObject enemy = Instantiate(enemyToSpawn, new Vector3(0, 0, 0), Quaternion.identity);
                    BaseEnemy newEnemy = enemy.GetComponent<BaseEnemy>();
                    newEnemy.DistanceTravelled = currentDistance - .5f;
                    currentDistance = newEnemy.DistanceTravelled;
                    newEnemy.enemyLevel = this.maxEnemyHealth;
                    WM.RemainingEnemies++;

                }                
                Destroy(gameObject);
            }
        }
        else
        {           
            if (MoneyUpgrade == true)
                Player.Instance.money += KillMoney + 1;
            else
                Player.Instance.money += KillMoney;
            enemyLevel -= Damage;
            if (enemyLevel <= 0)
            {
                WM.RemainingEnemies--;
                if (WM.RemainingEnemies <= 0)
                    Player.Instance.WaveResult(true);                
                Destroy(gameObject);
            }
        }
      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EndPoint")
        {
            Player.Instance.TakeDamage(DamageToPlayer, null);
            if (WM.RemainingEnemies <= 0)
                Player.Instance.WaveResult(true);
            Destroy(gameObject);
        }
    }

    public IEnumerator SlowDown(float slowTime, float SlowAmount)
    {
        Speed = Speed / SlowAmount;
        isSlow = true;
        yield return new WaitForSeconds(slowTime);
        Speed = Speed * SlowAmount;
        isSlow = false;
    }
    public IEnumerator Reverse()
    {
        float CurrentSpeed = Speed;
        Speed -= Speed + 1;
        isSlow = true;
        yield return new WaitForSeconds(1.5f);
        Speed = CurrentSpeed;
        isSlow = false;
    }
    public IEnumerator Stop()
    {
        float CurrentSpeed = Speed;
        Speed = 0;
        yield return new WaitForSeconds(2);
        Speed = CurrentSpeed;
    }
}

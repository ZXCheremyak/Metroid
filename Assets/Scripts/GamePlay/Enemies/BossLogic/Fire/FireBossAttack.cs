using UnityEngine;
using System.Collections;

public class FireBossAttack : BossAttack
{
    [Header("Meteor")]
    [SerializeField] Attack meteorRain;
    [SerializeField] Transform leftPoint, rightPoint;

    [Header("Barrage")]
    [SerializeField] Attack fireBallBarrage;
    [SerializeField] Transform[] barragePoints;

    [Header("FireBall")]
    [SerializeField] Attack bigFireball;
    [SerializeField] Transform fireBallPoint;

    IEnumerator[] attacks;

    float attackCoolDown;

    [SerializeField] float openForAttackWindow;

    [SerializeField] PlayerBase player;

    [SerializeField] AnimationPlayer animPlayer;  

    protected override void Start()
    {
        attacks = new IEnumerator[3];

        attacks[0] = CastBigFireball();
        attacks[1] = CastFireBallBarrage();
        attacks[2] = CastMeteorRain();
        
        animPlayer = GetComponent<AnimationPlayer>();

        base.Start();
    }

    public override void PerformAttack(int attackNumber)
    {
        if(!GetComponent<BaseBoss>().bossFightStarted) return;
        IEnumerator attack = attacks[attackNumber];
        Debug.Log(attack);
        StartCoroutine(attacks[attackNumber]);
        Debug.Log("Boss Attack");
    }

    IEnumerator CastBigFireball(){
        
        Debug.Log("FireBall");
        
        Flip();
        GameObject fireBallInstance = Instantiate(bigFireball.attackPrefab, fireBallPoint.position, Quaternion.identity);

        SetProjectileParameters(fireBallInstance.GetComponent<Projectile>(), bigFireball);

        fireBallInstance.GetComponent<Projectile>().destroysOnPlayerCollision = false;

        animPlayer.PlayAnimation("AttackStart");

        animPlayer.allowOtherAnimations = false;
        
        
        for(int i = 0; i < 100; i++)
        {   
            Flip();
            fireBallInstance.transform.position = fireBallPoint.position;
            fireBallInstance.transform.localScale = new Vector3(0.03f,0.03f,0.03f) * i;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.2f);

        animPlayer.allowOtherAnimations = true;
        animPlayer.PlayAnimation("AttackEnd");

        yield return new WaitForSeconds(0.5f);
        animPlayer.PlayAnimation("Idle");

        fireBallInstance.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(transform.localScale.x * bigFireball.speed, 0) * 2;
        yield return new WaitForSeconds(openForAttackWindow);
        GetComponent<FireBossMovement>().TeleportToAnotherPosition();
    }

    IEnumerator CastFireBallBarrage(){
        Debug.Log("Barrage");
        for(int i = 0; i < 40; i++)
        {
            Flip();
            animPlayer.PlayAnimation("AttackStart");
            animPlayer.allowOtherAnimations = false;
            yield return new WaitForSeconds(0.2f);
            GameObject fireBall = Instantiate(fireBallBarrage.attackPrefab, barragePoints[Random.Range(0, barragePoints.Length)].position, Quaternion.identity);
            fireBall.GetComponent<Rigidbody2D>().linearVelocity = new Vector2((barragePoints[0].position - transform.position).normalized.x * fireBallBarrage.speed, 0);
            SetProjectileParameters(fireBall.GetComponent<Projectile>(), fireBallBarrage);
            animPlayer.allowOtherAnimations = true;
            animPlayer.PlayAnimation("AttackEnd");
            yield return new WaitForSeconds(0.1f);
        }
        animPlayer.PlayAnimation("Idle");
        yield return new WaitForSeconds(openForAttackWindow);
        GetComponent<FireBossMovement>().TeleportToAnotherPosition();
        
    }

    IEnumerator CastMeteorRain(){
        Debug.Log("Meteor rain");
        for(int i = 0; i < 70; i++)
        {
            Flip();
            Vector2 meteorSpawnPos = new Vector2(Random.Range(leftPoint.position.x, rightPoint.position.x), leftPoint.position.y/2 + rightPoint.position.y/2);
            GameObject meteorInstance = Instantiate(meteorRain.attackPrefab, meteorSpawnPos, Quaternion.identity);

            meteorInstance.GetComponent<Rigidbody2D>().linearVelocity = Vector2.down * meteorRain.speed;
            SetProjectileParameters(meteorInstance.GetComponent<Projectile>(), meteorRain);

            yield return new WaitForSeconds(0.1f);
        }

        animPlayer.PlayAnimation("Idle");
        yield return new WaitForSeconds(openForAttackWindow);
        GetComponent<FireBossMovement>().TeleportToAnotherPosition();
    }

    void SetProjectileParameters(Projectile projectile, Attack attack){
        projectile.GetComponent<Projectile>().damage = attack.damage;
        projectile.GetComponent<Projectile>().element = element;
    }

    void Flip(){
        if(player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        else{
            transform.localScale = new Vector3(1,1,1);
        }
    }

    [System.Serializable]
    class Attack{
        [SerializeField] internal GameObject attackPrefab;
        [SerializeField] internal float damage, speed;
    }
}


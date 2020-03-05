using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTurret : BaseTurret
{

    public GameObject TurretHead2, missile, Rocket1, Rocket2;
    public Transform firePoint, FirePoint2, FirePoint3;
    public float MissileSpeed;
    public float MissileRadius;
    private bool DoubleRockets;
    public override string Upgrade1Info()
    {
        if (Level2UpgradeRank > 1 && Level1UpgradeRank == 1)
            return "Max upgrades reached.";
        switch (Level1UpgradeRank)
        {
            case 0:
                return "Increases Damage. ";
            case 1:
                return "Increases missile explosion range. ";
            case 2:
                return "Missile coveres a huge range but slower attack speed. ";
            default:
                return "Max upgrades reached.";


        }
    }
    public override string Upgrade2Info()
    {
        if (Level1UpgradeRank > 1 && Level2UpgradeRank == 1)
            return "Max upgrades reached.";
        switch (Level2UpgradeRank)
        {
            case 0:
                return "Increases Radius. ";
            case 1:
                return "Increase fire rate and allows turret to see camo enemies. ";
            case 2:
                return "allows turret to launch 2 missiles at once. ";
            default:
                return "Max upgrades reached.";
        }
    }
    protected override IEnumerator FindEnemies()
    {
        if (Placed)
        {
            Hit = Physics2D.OverlapCircleAll(transform.position, Radius + 0.1f);
            Collider2D Target = null;
            Collider2D Target2 = null;
            float Distance = 0;
            foreach (Collider2D enemy in Hit)
            {
                if (enemy.tag == "Enemy")
                {
                    Enemy = enemy.GetComponent<BaseEnemy>();
                    if (Distance <= Enemy.DistanceTravelled)
                    {
                        Distance = Enemy.DistanceTravelled;
                        Target = enemy;
                        if(!DoubleRockets)
                        {
                            TurretHead.transform.LookAt(Target.transform);
                            TurretHead.transform.localEulerAngles = new Vector3(0, TurretHead.transform.localEulerAngles.y - 90, 0);
                        }
                        else
                            TurretHead.transform.LookAt(Target.transform);
                            TurretHead.transform.localEulerAngles = new Vector3(TurretHead.transform.localEulerAngles.x - 90, TurretHead.transform.localEulerAngles.y, 0);

                        if (ShootAnim != null)
                        ShootAnim.SetActive(true);
                    }
                }
            }
            if(DoubleRockets)
            {
                foreach (Collider2D enemy in Hit)
                {
                    if (enemy.tag == "Enemy")
                    {
                        if (Distance >= enemy.GetComponent<BaseEnemy>().DistanceTravelled)
                        {
                            Distance = enemy.GetComponent<BaseEnemy>().DistanceTravelled;
                            Target2 = enemy;
                        }
                    }
                }
            }

            if (Target != null)
            {
                Missile Missile = Instantiate(missile, firePoint.transform.position, firePoint.transform.rotation).GetComponent<Missile>();
                Missile.damage = Damage;
                Missile.speed = MissileSpeed;
                Missile.radius = MissileRadius;
                Missile.moneyUpgrade = MoneyUpgrade;
                Missile.gameObject.transform.position = new Vector3(firePoint.transform.position.x, firePoint.transform.position.y, 0);
                Missile.sender = this;
                Missile.target = Target.gameObject;
            }
            if(DoubleRockets)
            {
                if (Target2 != null)
                {
                    Missile Missile = Instantiate(missile, FirePoint3.transform.position, firePoint.transform.rotation).GetComponent<Missile>();
                    Missile.damage = Damage;
                    Missile.speed = MissileSpeed;
                    Missile.radius = MissileRadius;
                    Missile.moneyUpgrade = MoneyUpgrade;
                    Missile.gameObject.transform.position = new Vector3(firePoint.transform.position.x, firePoint.transform.position.y, 0);
                    Missile.sender = this;
                    Missile.target = Target2.gameObject;
                }
            }


        }
        yield return new WaitForSeconds(FireRate);
        if(ShootAnim != null)
        ShootAnim.SetActive(false);
        StartCoroutine(FindEnemies());
    }


    public override void UpgradeType1()
    {
        switch (Level1UpgradeRank)
        {
            case 0:
                if (Player.Instance.money >= T1TurretUpgradeCost)
                {
                    Player.Instance.money -= T1TurretUpgradeCost;
                    SellPrice += T1TurretUpgradeCost / 2;
                    T1TurretUpgradeCost += 400;                   
                    Damage++;
                    Player.Instance.TurretDamageText.text = "" + Damage;
                    Level1UpgradeRank++;
                }
                break;
            case 1:
                if (Player.Instance.money >= T1TurretUpgradeCost && Level2UpgradeRank <= 1)
                {
                    Player.Instance.money -= T1TurretUpgradeCost;
                    SellPrice += T1TurretUpgradeCost / 2;
                    T1TurretUpgradeCost += 700;
                    MissileRadius += 0.5f;
                    Level1UpgradeRank++;
                }
                break;
            case 2:
                if (Player.Instance.money >= T1TurretUpgradeCost)
                {
                    Player.Instance.money -= T1TurretUpgradeCost;
                    SellPrice += T1TurretUpgradeCost / 2;
                    MissileRadius += 0.5f;
                    Level1UpgradeRank++;
                }
                break;
        }
        Player.Instance.UpdateUpgradeText();
    }

    public override void UpgradeType2()
    {
        switch (Level2UpgradeRank)
        {
            case 0:
                if (Player.Instance.money >= T2TurretUpgradeCost)
                {
                    Player.Instance.money -= T2TurretUpgradeCost;
                    SellPrice += T2TurretUpgradeCost / 2;
                    T2TurretUpgradeCost += 400;                   
                    Radius += 0.5f;
                    Level2UpgradeRank++;
                    SetPoints();
                }
                break;
            case 1:
                if (Player.Instance.money >= T2TurretUpgradeCost && Level1UpgradeRank <= 1)
                {
                    Player.Instance.money -= T2TurretUpgradeCost;
                    SellPrice += T2TurretUpgradeCost / 2;
                    T2TurretUpgradeCost += 400;                   
                    FireRate -= 0.5f;
                    canSeeCamo = true;
                    Level2UpgradeRank++;                   
                }
                break;
            case 2:
                if (Player.Instance.money >= T2TurretUpgradeCost)
                {
                    Player.Instance.money -= T2TurretUpgradeCost;
                    SellPrice += T2TurretUpgradeCost / 2;
                    DoubleRockets = true;
                    Destroy(Rocket1);
                    Rocket2.SetActive(true);
                    firePoint = FirePoint2;
                    TurretHead = TurretHead2;
                    Level2UpgradeRank++;
                }
                break;
        }
        Player.Instance.UpdateUpgradeText();
    }
}

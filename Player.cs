using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour, IDamagable
{
    public delegate void Outcome();
    public event Outcome WaveLost;
    public event Outcome WaveWon;
    public static Player Instance;

    public Text MoneyText;
    public Text LivesText;
    public Text SellPriceText;
    public Text TurretNameText, TurretDamageText, T1TurretUpgradeInfoText, T2TurretUpgradeInfoText, KillCountText;
    public GameObject Turret, AutomaticTurret, RocketTurret, SlowTurret, RadarTurret, ElectricTurret, BigBertha, Drone, DronePoints;
    public int TurretCost, AutomaticTurretCost, RocketTurretCost, SlowTurretCost, RadarTurretCost, ElectricTurretCost, BigBerthaCost, DroneCost;
    public bool PlayerLost;
    public bool DroneBought;
    public GameObject UpgradePanel;
    public Button DroneButton;
    public List<Radar> Radars;
    [SerializeField] int Lives;
    public int lives
    {
        get { return Lives; }
        set
        {
            Lives = value;
            LivesText.text = "Lives: " + lives;

        }
    }
    [SerializeField] int Money;
    public int money
    {
        get { return Money;}
        set
        {
            Money = value;
            MoneyText.text = "Money: " + money;
            
        }
    }

    private BaseTurret currentTurret;
    public BaseTurret CurrentTurret
    {
        get { return currentTurret; }
        set
        {
            currentTurret = value;
            if(CurrentTurret != null && CurrentTurret.Placed)
            {
                UpgradePanel.SetActive(true);               
                UpdateUpgradeText();
            }
            else
            {
                UpgradePanel.SetActive(false);
                TurretNameText.text = "";
                TurretDamageText.text = "";
            }
            
        }
    }
    public void UpdateUpgradeText()
    {
        TurretNameText.text = "" + CurrentTurret.TurretName;
        TurretDamageText.text = "Damage: " + CurrentTurret.Damage;
        SellPriceText.text = "Sell: " + CurrentTurret.SellPrice;
        KillCountText.text = "Kills: " + CurrentTurret.KillCount;
        if ((CurrentTurret.Level1UpgradeRank <= 2 && CurrentTurret.Level2UpgradeRank <= 1) || CurrentTurret.Level1UpgradeRank == 0)
            T1TurretUpgradeInfoText.text = CurrentTurret.Upgrade1Info() + "£" + CurrentTurret.T1TurretUpgradeCost;
        else
            T1TurretUpgradeInfoText.text = CurrentTurret.Upgrade1Info();

        if ((CurrentTurret.Level2UpgradeRank <= 2 && CurrentTurret.Level1UpgradeRank <= 1) || CurrentTurret.Level2UpgradeRank == 0)
            T2TurretUpgradeInfoText.text = CurrentTurret.Upgrade2Info() + "£" + CurrentTurret.T2TurretUpgradeCost;
        else
            T2TurretUpgradeInfoText.text = CurrentTurret.Upgrade2Info();
    }
    void Start()
    {
        Instance = this;
        WaveLost += waveLost;
        WaveWon += waveWon;
        LivesText.text = "Lives: " + lives;
        MoneyText.text = "Money: " + money;
    }

    public void BuyTurret()
    {
        if(CurrentTurret != null)
        DeSelectTurret();
        if(money >= TurretCost)
        {
            money -= TurretCost;
            Instantiate(Turret);
        }       
    }
    public void BuyAutomaticTurret()
    {
        if (CurrentTurret != null)
            DeSelectTurret();
        if (money >= AutomaticTurretCost)
        {
            money -= AutomaticTurretCost;
            Instantiate(AutomaticTurret);
        }
    }
    public void BuyRocketTurret()
    {
        if (CurrentTurret != null)
            DeSelectTurret();
        if (money >= RocketTurretCost)
        {
            money -= RocketTurretCost;
            Instantiate(RocketTurret);
        }
    }
    public void BuySlowTurret()
    {
        if (CurrentTurret != null)
            DeSelectTurret();
        if (money >= SlowTurretCost)
        {
            money -= SlowTurretCost;
            Instantiate(SlowTurret);
        }
    }
    public void BuyRadarTurret()
    {
        if (CurrentTurret != null)
            DeSelectTurret();
        if (money >= RadarTurretCost)
        {
            money -= RadarTurretCost;
            Instantiate(RadarTurret);
        }
    }
    public void BuyElectricTurret()
    {
        if (CurrentTurret != null)
            DeSelectTurret();
        if (money >= ElectricTurretCost)
        {
            money -= ElectricTurretCost;
            Instantiate(ElectricTurret);
        }
    }
    public void BuyBigBertha()
    {
        if (CurrentTurret != null)
            DeSelectTurret();
        if (money >= BigBerthaCost)
        {
            money -= BigBerthaCost;
            Instantiate(BigBertha);
        }
    }
    public void BuyDrone()
    {
        if (CurrentTurret != null)
            DeSelectTurret();
        if (money >= DroneCost)
        {
            money -= DroneCost;
            Drone.SetActive(true);
            DroneBought = true;
            if(!WaveManager.Instance.WaveStarted)
            DronePoints.SetActive(true);
            DroneButton.interactable = false;
        }
    }
    public void UpdateKillCount() { if(CurrentTurret != null)KillCountText.text = "Kills: " + CurrentTurret.KillCount; }
    public void TakeDamage(int Damage, bool? NA)
    {
        lives -= Damage;
        WaveManager.Instance.RemainingEnemies--;
        if(lives <= 0)
        {
            WaveResult(false);
        }
    }
    public void WaveResult(bool Won)
    {
        if(Won)
            WaveWon.Invoke();
        else
            WaveLost.Invoke();
    }

    private void waveLost()
    {
        PlayerLost = true;
        print("oof");
        Time.timeScale = 0;
    }
    public void waveWon()
    {
        print("yeet");        
    }
    /*
    public void SelectedTurret(BaseTurret Sender)
    {
        CurrentTurret = Sender;
        Sender.UpgradeTurret -= SelectedTurret;
    }
    */
    public void ScanRadars(bool sold)
    {
        foreach(Radar ActiveRadars in Radars)
        {
            ActiveRadars.FindAllies(sold);
        }
    }

    public virtual void SellTurret()
    {
        if (CurrentTurret.GetComponent<Radar>())
        {
            ScanRadars(true);
            Radars.Remove(CurrentTurret.GetComponent<Radar>());           
        }
        money += CurrentTurret.SellPrice;
        Destroy(CurrentTurret.gameObject);
        DeSelectTurret();
    }
    public void UpgradeTurretT1()
    {
        CurrentTurret.UpgradeType1();
    }
    public void UpgradeTurretT2()
    {
        CurrentTurret.UpgradeType2();
    }
    public void DeSelectTurret()
    {
        
        CurrentTurret.Selected = false;
        CurrentTurret = null;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.NiceVibrations;

public class UpgradeController2 : MonoBehaviour
{
    public static UpgradeController2 instance;
    public int SceneIndex;
    public int[] upgradeIndex = new int[10];

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SaveLoadSystem.Load();
        updateText();
        upgradeCanvas.SetActive(false);

        for (int i = 0; i < 10; i++)
        {
            upgradeIndex[i] = SaveLoadSystem.instance.upgrades2[i + SceneIndex * 10];
        }
    }

    private bool[] pressable = new bool[10];

    public GameObject upgradeCanvas;
    public GameObject notEnoughMoneyText;

    public GameObject clickImage;
    
    public TextMeshProUGUI[] allUpgrades;
    private void updateText()
    {
        SaveLoadSystem.Load();
        
        /*allUpgrades[1].text = "$" + CollectChargeSpeedIncreaseMoney[SaveLoadSystem.instance.upgrades2[1]].ToString();
        allUpgrades[2].text = "$" + HotDogLineCapacityIncreaseMoney[SaveLoadSystem.instance.upgrades2[2]].ToString();
        allUpgrades[3].text = "$" + HamburgerLineCapacityIncreaseMoney[SaveLoadSystem.instance.upgrades2[3]].ToString();
        allUpgrades[4].text = "$" + PizzaLineCapacityIncreaseMoney[SaveLoadSystem.instance.upgrades2[4]].ToString();
        
        allUpgrades[5].text = "$" + HotDogTruckDecorationUpgradeMoney[SaveLoadSystem.instance.upgrades2[5]].ToString();
        allUpgrades[6].text = "$" + HamburgerTruckDecorationUpgradeMoney[SaveLoadSystem.instance.upgrades2[6]].ToString();
        allUpgrades[7].text = "$" + PizzaTruckDecorationUpgradeMoney[SaveLoadSystem.instance.upgrades2[7]].ToString();
        allUpgrades[8].text = "$" + CustomerEatSpeedIncreaseMoney[SaveLoadSystem.instance.upgrades2[8]].ToString();
        allUpgrades[9].text = "$" + CustomerMoveSpeedIncreaseMoney[SaveLoadSystem.instance.upgrades2[9]].ToString();*/
        
        CheckMaxOrNot();
    }

    

    IEnumerator CloseAndEnableUI(float _timer)
    {
        upgradeCanvas.SetActive(false);
        yield return new WaitForSeconds(_timer);
        if (player)
        {
            upgradeCanvas.SetActive(true);
        }
    }

    private void CheckMaxOrNot()
    {
        for (int i = 0; i < pressable.Length; i++)
        {
            pressable[i] = false;
        }
        
        if (characterCarryCapacityMoney.Length <= SaveLoadSystem.instance.upgrades2[0])
        {
            allUpgrades[0].text = "MAX";
            pressable[0] = true;
        }
        else
        {
            allUpgrades[0].text = "$" + characterCarryCapacityMoney[SaveLoadSystem.instance.upgrades2[0]].ToString();
        }

        if (CollectChargeSpeedIncreaseMoney.Length <= SaveLoadSystem.instance.upgrades2[1])
        {
            allUpgrades[1].text = "MAX";
            pressable[1] = true;
        }
        else
        {
            allUpgrades[1].text = "$" + CollectChargeSpeedIncreaseMoney[SaveLoadSystem.instance.upgrades2[1]].ToString();
        }
        
        if (HotDogLineCapacityIncreaseMoney.Length <= SaveLoadSystem.instance.upgrades2[2 + SceneIndex * 10])
        {
            allUpgrades[2].text = "MAX";
            pressable[2] = true;
        }
        else
        {
            allUpgrades[2].text = "$" + HotDogLineCapacityIncreaseMoney[SaveLoadSystem.instance.upgrades2[2 + SceneIndex * 10]].ToString();
        }
        
        if (VIPtipsMoney.Length <= SaveLoadSystem.instance.upgrades2[3+ SceneIndex * 10])
        {
            allUpgrades[3].text = "MAX";
            pressable[3] = true;
        }
        else
        {
            allUpgrades[3].text = "$" + VIPtipsMoney[SaveLoadSystem.instance.upgrades2[3+ SceneIndex * 10]].ToString();
        }
        
        if (PizzaLineCapacityIncreaseMoney.Length <= SaveLoadSystem.instance.upgrades2[4+ SceneIndex * 10])
        {
            allUpgrades[4].text = "MAX";
            pressable[4] = true;
        }
        else
        {
            allUpgrades[4].text = "$" + PizzaLineCapacityIncreaseMoney[SaveLoadSystem.instance.upgrades2[4+ SceneIndex * 10]].ToString();
        }
        
        if (HotDogTruckDecorationUpgradeMoney.Length <= SaveLoadSystem.instance.upgrades2[5+ SceneIndex * 10])
        {
            allUpgrades[5].text = "MAX";
            pressable[5] = true;
        }
        else
        {
            allUpgrades[5].text = "$" + HotDogTruckDecorationUpgradeMoney[SaveLoadSystem.instance.upgrades2[5+ SceneIndex * 10]].ToString();
        }
        
        if (HamburgerTruckDecorationUpgradeMoney.Length <= SaveLoadSystem.instance.upgrades2[6+ SceneIndex * 10])
        {
            allUpgrades[6].text = "MAX";
            pressable[6] = true;
        }
        else
        {
            allUpgrades[6].text = "$" + HamburgerTruckDecorationUpgradeMoney[SaveLoadSystem.instance.upgrades2[6+ SceneIndex * 10]].ToString();
        }
        
        if (PizzaTruckDecorationUpgradeMoney.Length <= SaveLoadSystem.instance.upgrades2[7+ SceneIndex * 10])
        {
            allUpgrades[7].text = "MAX";
            pressable[7] = true;
        }
        else
        {
            allUpgrades[7].text = "$" + PizzaTruckDecorationUpgradeMoney[SaveLoadSystem.instance.upgrades2[7+ SceneIndex * 10]].ToString();
        }
        
        if (CustomerEatSpeedIncreaseMoney.Length <= SaveLoadSystem.instance.upgrades2[8+ SceneIndex * 10])
        {
            allUpgrades[8].text = "MAX";
            pressable[8] = true;
        }
        else
        {
            allUpgrades[8].text = "$" + CustomerEatSpeedIncreaseMoney[SaveLoadSystem.instance.upgrades2[8+ SceneIndex * 10]].ToString();
        }
        
        if (CustomerMoveSpeedIncreaseMoney.Length <= SaveLoadSystem.instance.upgrades2[9+ SceneIndex * 10])
        {
            allUpgrades[9].text = "MAX";
            pressable[9] = true;
        }
        else
        {
            allUpgrades[9].text = "$" + CustomerMoveSpeedIncreaseMoney[SaveLoadSystem.instance.upgrades2[9+ SceneIndex * 10]].ToString();
        }

        if (SaveLoadSystem.instance.upgrades2[0] == 0)
        {
            allUpgrades[0].text = "FREE";
            
            clickImage.SetActive(true);

            for (int i = 1; i < pressable.Length; i++)
            {
                pressable[i] = true;
            }
        }
        else
        {
            clickImage.SetActive(false);
        }
    }

    private void createDontEnoghMoneyText()
    {
        Instantiate(notEnoughMoneyText, upgradeCanvas.transform);
    }
    
    public int[] characterCarryCapacityMoney;
    public void CharacterCarryCapacityIncrease()
    {
        if (pressable[0])
        {
            return;
        }
        
        SaveLoadSystem.Load();
        int requiredMoney = characterCarryCapacityMoney[SaveLoadSystem.instance.upgrades2[0]];
        if (PlayerMoneyData.instance.TotalMoney < requiredMoney )
        {
            createDontEnoghMoneyText();
            return;
        }
        MMVibrationManager.Haptic (HapticTypes.MediumImpact);
        
        PlayerMoneyData.instance.TotalMoney -= requiredMoney;
        SaveLoadSystem.instance.upgrades2[0]++;
        SaveLoadSystem.Save();
        PlayerUpgradeController.instance.characterStackUpgradeChanged();
        updateText();
    }

    public int[] CollectChargeSpeedIncreaseMoney;
    public void CollectChargeSpeedIncrease()
    {
        if (pressable[1])
        {
            return;
        }
        
        SaveLoadSystem.Load();
        int requiredMoney = CollectChargeSpeedIncreaseMoney[SaveLoadSystem.instance.upgrades2[1]];
        if (PlayerMoneyData.instance.TotalMoney < requiredMoney )
        {
            createDontEnoghMoneyText();
            return;
        }
        MMVibrationManager.Haptic (HapticTypes.MediumImpact);
        
        PlayerMoneyData.instance.TotalMoney -= requiredMoney;
        SaveLoadSystem.instance.upgrades2[1]++;
        SaveLoadSystem.Save();
        PlayerUpgradeController.instance.characterStackUpgradeChanged();
        updateText();
    }

    public int[] HotDogLineCapacityIncreaseMoney;
    public void HotDogLineCapacityIncrease()
    {
        if (pressable[2])
        {
            return;
        }
        
        SaveLoadSystem.Load();
        int requiredMoney = HotDogLineCapacityIncreaseMoney[SaveLoadSystem.instance.upgrades2[2+ SceneIndex * 10]];
        if (PlayerMoneyData.instance.TotalMoney < requiredMoney )
        {
            createDontEnoghMoneyText();
            return;
        }
        MMVibrationManager.Haptic (HapticTypes.MediumImpact);
        
        PlayerMoneyData.instance.TotalMoney -= requiredMoney;
        SaveLoadSystem.instance.upgrades2[2+ SceneIndex * 10]++;
        SaveLoadSystem.Save();

        for (int i = 0; i < SceneData.instance.allTruck.Count; i++)
        {
            SceneData.instance.allTruck[i].UpgradeOccured();
        }
        
        updateText();
    }

    public int[] VIPtipsMoney;
    public void VIPupgradeButton()
    {
        if (pressable[3])
        {
            return;
        }
        
        SaveLoadSystem.Load();
        int requiredMoney = VIPtipsMoney[SaveLoadSystem.instance.upgrades2[3+ SceneIndex * 10]];
        if (PlayerMoneyData.instance.TotalMoney < requiredMoney )
        {
            createDontEnoghMoneyText();
            return;
        }
        MMVibrationManager.Haptic (HapticTypes.MediumImpact);
        
        PlayerMoneyData.instance.TotalMoney -= requiredMoney;
        SaveLoadSystem.instance.upgrades2[3+ SceneIndex * 10]++;
        SaveLoadSystem.Save();
        SceneData.instance.CheckUpgrades();
        
        updateText();
    }

    public int[] PizzaLineCapacityIncreaseMoney;
    public void PizzaLineCapacityIncrease()
    {
        //todo
    }

    public int[] HotDogTruckDecorationUpgradeMoney;
    public void HotDogTruckDecorationUpgrade()
    {
        //todo
    }

    public int[] HamburgerTruckDecorationUpgradeMoney;
    public void HamburgerTruckDecorationUpgrade()
    {
        
    }

    public int[] PizzaTruckDecorationUpgradeMoney;
    public void PizzaTruckDecorationUpgrade()
    {
        
    }

    public int[] CustomerEatSpeedIncreaseMoney;
    public void CustomerEatSpeedIncrease()
    {
        if (pressable[8])
        {
            return;
        }
        
        SaveLoadSystem.Load();
        int requiredMoney = CustomerEatSpeedIncreaseMoney[SaveLoadSystem.instance.upgrades2[8+ SceneIndex * 10]];
        if (PlayerMoneyData.instance.TotalMoney < requiredMoney )
        {
            createDontEnoghMoneyText();
            return;
        }
        MMVibrationManager.Haptic (HapticTypes.MediumImpact);
       
        PlayerMoneyData.instance.TotalMoney -= requiredMoney;
        SaveLoadSystem.instance.upgrades2[8+ SceneIndex * 10]++;
        SaveLoadSystem.Save();
        SceneData.instance.CheckUpgrades();
        updateText();
    }

    public int[] CustomerMoveSpeedIncreaseMoney;
    public void CustomerMoveSpeedIncrease()
    {
        if (pressable[9])
        {
            return;
        }
        
        SaveLoadSystem.Load();
        int requiredMoney = CustomerMoveSpeedIncreaseMoney[SaveLoadSystem.instance.upgrades2[9+ SceneIndex * 10]];
        if (PlayerMoneyData.instance.TotalMoney < requiredMoney )
        {
            createDontEnoghMoneyText();
            return;
        }
        MMVibrationManager.Haptic (HapticTypes.MediumImpact);
       
        PlayerMoneyData.instance.TotalMoney -= requiredMoney;
        SaveLoadSystem.instance.upgrades2[9+ SceneIndex * 10]++;
        SaveLoadSystem.Save();
        SceneData.instance.CheckUpgrades();
        updateText();
    }
    
    

    public void CloseCanvas()
    {
        upgradeCanvas.SetActive(false);
    }

    private Transform player;
    private void Update()
    {
        if (player == null)
        {
            return;
        }
        
        if (Vector3.Distance(transform.position , player.position) > 1f)
        {
            upgradeCanvas.SetActive(false);
            player = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            upgradeCanvas.SetActive(true);
            player = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            upgradeCanvas.SetActive(false);
        }
    }
}

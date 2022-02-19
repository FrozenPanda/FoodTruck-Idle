using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController2 : MonoBehaviour
{
    private void Start()
    {
        updateText();
        upgradeCanvas.SetActive(false);
    }

    public GameObject upgradeCanvas;

    public TextMeshProUGUI[] allUpgrades;
    private void updateText()
    {
        SaveLoadSystem.Load();
        allUpgrades[0].text = "$" + characterCarryCapacityMoney[SaveLoadSystem.instance.upgrades2[0]].ToString();
        allUpgrades[1].text = "$" + CollectChargeSpeedIncreaseMoney[SaveLoadSystem.instance.upgrades2[1]].ToString();
        allUpgrades[2].text = "$" + HotDogLineCapacityIncreaseMoney[SaveLoadSystem.instance.upgrades2[2]].ToString();
        allUpgrades[3].text = "$" + HamburgerLineCapacityIncreaseMoney[SaveLoadSystem.instance.upgrades2[3]].ToString();
        allUpgrades[4].text = "$" + PizzaLineCapacityIncreaseMoney[SaveLoadSystem.instance.upgrades2[4]].ToString();
        
        allUpgrades[5].text = "$" + HotDogTruckDecorationUpgradeMoney[SaveLoadSystem.instance.upgrades2[5]].ToString();
        allUpgrades[6].text = "$" + HamburgerTruckDecorationUpgradeMoney[SaveLoadSystem.instance.upgrades2[6]].ToString();
        allUpgrades[7].text = "$" + PizzaTruckDecorationUpgradeMoney[SaveLoadSystem.instance.upgrades2[7]].ToString();
        allUpgrades[8].text = "$" + CustomerEatSpeedIncreaseMoney[SaveLoadSystem.instance.upgrades2[8]].ToString();
        allUpgrades[9].text = "$" + CustomerMoveSpeedIncreaseMoney[SaveLoadSystem.instance.upgrades2[9]].ToString();
    }
    
    public int[] characterCarryCapacityMoney;
    public void CharacterCarryCapacityIncrease()
    {
        SaveLoadSystem.Load();
        int requiredMoney = characterCarryCapacityMoney[SaveLoadSystem.instance.upgrades2[0]];
        if (PlayerMoneyData.instance.TotalMoney < requiredMoney )
        {
            return;
        }

        PlayerMoneyData.instance.TotalMoney -= requiredMoney;
        SaveLoadSystem.instance.upgrades2[0]++;
        SaveLoadSystem.Save();
        PlayerUpgradeController.instance.characterStackUpgradeChanged();
        updateText();
    }

    public int[] CollectChargeSpeedIncreaseMoney;
    public void CollectChargeSpeedIncrease()
    {
        SaveLoadSystem.Load();
        int requiredMoney = CollectChargeSpeedIncreaseMoney[SaveLoadSystem.instance.upgrades2[1]];
        if (PlayerMoneyData.instance.TotalMoney < requiredMoney )
        {
            return;
        }
        
        PlayerMoneyData.instance.TotalMoney -= requiredMoney;
        SaveLoadSystem.instance.upgrades2[1]++;
        SaveLoadSystem.Save();
        PlayerUpgradeController.instance.characterStackUpgradeChanged();
        updateText();
    }

    public int[] HotDogLineCapacityIncreaseMoney;
    public void HotDogLineCapacityIncrease()
    {
        SaveLoadSystem.Load();
        int requiredMoney = HotDogLineCapacityIncreaseMoney[SaveLoadSystem.instance.upgrades2[2]];
        if (PlayerMoneyData.instance.TotalMoney < requiredMoney )
        {
            return;
        }
        
        PlayerMoneyData.instance.TotalMoney -= requiredMoney;
        SaveLoadSystem.instance.upgrades2[2]++;
        SaveLoadSystem.Save();
        HotDogQueuManager.instance.UpgradeOccured();
        updateText();
    }

    public int[] HamburgerLineCapacityIncreaseMoney;
    public void HamburgerLineCapacityIncrease()
    {
        SaveLoadSystem.Load();
        int requiredMoney = HamburgerLineCapacityIncreaseMoney[SaveLoadSystem.instance.upgrades2[3]];
        if (PlayerMoneyData.instance.TotalMoney < requiredMoney )
        {
            return;
        }
        
        //todo
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
        SaveLoadSystem.Load();
        int requiredMoney = CustomerEatSpeedIncreaseMoney[SaveLoadSystem.instance.upgrades2[8]];
        if (PlayerMoneyData.instance.TotalMoney < requiredMoney )
        {
            return;
        }

        PlayerMoneyData.instance.TotalMoney -= requiredMoney;
        SaveLoadSystem.instance.upgrades2[8]++;
        SaveLoadSystem.Save();
        SceneData.instance.CheckUpgrades();
        updateText();
    }

    public int[] CustomerMoveSpeedIncreaseMoney;
    public void CustomerMoveSpeedIncrease()
    {
        SaveLoadSystem.Load();
        int requiredMoney = CustomerMoveSpeedIncreaseMoney[SaveLoadSystem.instance.upgrades2[9]];
        if (PlayerMoneyData.instance.TotalMoney < requiredMoney )
        {
            return;
        }

        PlayerMoneyData.instance.TotalMoney -= requiredMoney;
        SaveLoadSystem.instance.upgrades2[9]++;
        SaveLoadSystem.Save();
        SceneData.instance.CheckUpgrades();
        updateText();
    }

    public void CloseCanvas()
    {
        upgradeCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            upgradeCanvas.SetActive(true);
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

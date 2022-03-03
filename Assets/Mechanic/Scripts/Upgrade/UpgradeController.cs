using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    public GameObject UpgradeCanvas;
    public GameObject notEnoughMoneyText;
    public GameObject HotDogAI;
    
    public TextMeshProUGUI[] allUpgrades;
    public bool[] pressable = new bool[3];

    public int[] HireStuffCosts;
    //For All stuff hire things
    public void EnableHotDogAI()
    {
        if (pressable[0])
        {
            return;
        }
        
        SaveLoadSystem.Load();
        int requiredMoney = HireStuffCosts[SaveLoadSystem.instance.HireStaffUpgrades];

        if (PlayerMoneyData.instance.TotalMoney < requiredMoney)
        {
            createDontEnoghMoneyText();
            return;
        }
        
        if (SaveLoadSystem.instance.HireStaffUpgrades == 0)
        {
            PlayerAIMove.instance.EnableAI();
            UpgradeCanvas.SetActive(false);
            SaveLoadSystem.instance.HireStaffUpgrades++;
            PlayerMoneyData.instance.TotalMoney -= requiredMoney;
            SaveLoadSystem.Save();
            UpdateText();
        }
    }

    public int[] MaxCapacityCost;
    public void IncreaseMaxCapacity()
    {
        if (pressable[1])
        {
            return;
        }
        
        SaveLoadSystem.Load();
        int requiredMoney = MaxCapacityCost[SaveLoadSystem.instance.HireStaffCapacity];
        if (PlayerMoneyData.instance.TotalMoney < requiredMoney)
        {
            createDontEnoghMoneyText();
            return;
        }

        PlayerMoneyData.instance.TotalMoney -= requiredMoney;
        SaveLoadSystem.instance.HireStaffCapacity++;
        SaveLoadSystem.Save();
        SceneData.instance.CheckUpgrades();
        
        UpdateText();
        SaveLoadSystem.Load();
        for (int i = 0; i < PlayerAIdatabase.instance.AllPlayerAIlist.Count; i++)
        {
            PlayerAIdatabase.instance.AllPlayerAIlist[i].CheckUpgrades();
        }

    }

    public int[] StuffMoveSpeedCost;
    public void IncreaseMoveSpeed()
    {
        if (pressable[2])
        {
            return;
        }
        
        SaveLoadSystem.Load();
        int requiredMoney = StuffMoveSpeedCost[SaveLoadSystem.instance.HireStaffMoveSpeed];
        if (PlayerMoneyData.instance.TotalMoney < requiredMoney)
        {
            createDontEnoghMoneyText();
            return;
        }
        
        PlayerMoneyData.instance.TotalMoney -= requiredMoney;
        SaveLoadSystem.instance.HireStaffMoveSpeed++;
        SaveLoadSystem.Save();
        SceneData.instance.CheckUpgrades();
        
        UpdateText();
        SaveLoadSystem.Load();
        for (int i = 0; i < SaveLoadSystem.instance.HireStaffUpgrades; i++)
        {
            SceneData.instance.allPlayerAI[i].CheckUpgrades();
        }
    }
    
    void Start()
    {
        UpdateText();
        CheckUpgrades();
    }

    private void CheckUpgrades()
    {
        SaveLoadSystem.Load();

        for (int i = 0; i < SaveLoadSystem.instance.HireStaffUpgrades; i++)
        {
            SceneData.instance.allPlayerAI[i].EnableAI();
        }
    }

    private void UpdateText()
    {
        if (HireStuffCosts.Length <= SaveLoadSystem.instance.HireStaffUpgrades)
        {
            allUpgrades[0].text = "MAX";

            pressable[0] = true;
        }
        else
        {
            if (SaveLoadSystem.instance.HireStaffUpgrades == 0)
            {
                allUpgrades[0].text = "$" + HireStuffCosts[SaveLoadSystem.instance.HireStaffUpgrades];
                
                pressable[0] = false;
            }
            
            if (SaveLoadSystem.instance.HireStaffUpgrades == 1)
            {
                if (SaveLoadSystem.instance.TableUnlock[5] == -1)
                {
                    allUpgrades[0].text = "NotUnlockedYet";
                    
                    pressable[0] = true;
                }
                else
                {
                    allUpgrades[0].text = "$" +HireStuffCosts[SaveLoadSystem.instance.HireStaffUpgrades];
                    
                    pressable[0] = false;
                }
            }
        }

        if (MaxCapacityCost.Length < SaveLoadSystem.instance.HireStaffCapacity)
        {
            allUpgrades[1].text = "MAX";

            pressable[1] = true;
        }
        else
        {
            allUpgrades[1].text = "$" + MaxCapacityCost[SaveLoadSystem.instance.HireStaffCapacity];
        }

        if (StuffMoveSpeedCost.Length < SaveLoadSystem.instance.HireStaffMoveSpeed)
        {
            allUpgrades[2].text = "MAX";

            pressable[2] = true;
        }
        else
        {
            allUpgrades[2].text = "$" + StuffMoveSpeedCost[SaveLoadSystem.instance.HireStaffMoveSpeed];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UpgradeCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            UpgradeCanvas.SetActive(false);
        }
    }
    
    public void CloseCanvas()
    {
        UpgradeCanvas.SetActive(false);
    }
    
    private void createDontEnoghMoneyText()
    {
        Instantiate(notEnoughMoneyText, UpgradeCanvas.transform);
    }
}

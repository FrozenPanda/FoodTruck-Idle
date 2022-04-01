using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class UpgradeController : MonoBehaviour
{
    public GameObject UpgradeCanvas;
    public GameObject notEnoughMoneyText;
    public GameObject HotDogAI;
    
    public TextMeshProUGUI[] allUpgrades;
    public bool[] pressable = new bool[3];

    public int[] HireStuffCosts;
    //For All stuff hire things
    
    
    //5 for 1. stuff -- 18 for 2 -- 19 3
    public void EnableHotDogAI()
    {
        if (pressable[0])
        {
            return;
        }
        
        SaveLoadSystem.Load();
        int requiredMoney = HireStuffCosts[SaveLoadSystem.instance.upgrades[0 +SceneIndex * 10 ]];

        if (PlayerMoneyData.instance.TotalMoney < requiredMoney)
        {
            createDontEnoghMoneyText();
            return;
        }
        
        /*if (SaveLoadSystem.instance.upgrades[0 +SceneIndex * 10 ] == 0)
        {
            PlayerAIMove.instance.EnableAI();
            
        }*/
        
        MMVibrationManager.Haptic (HapticTypes.MediumImpact);
        PlayerAIdatabase.instance.AllPlayerAIlist[SaveLoadSystem.instance.upgrades[0 +SceneIndex * 10 ]].EnableAI();
        
        SaveLoadSystem.instance.upgrades[0 +SceneIndex * 10 ]++;
        PlayerMoneyData.instance.TotalMoney -= requiredMoney;
        SaveLoadSystem.Save();
        UpdateText();
        
    }

    public int[] MaxCapacityCost;
    public void IncreaseMaxCapacity()
    {
        if (pressable[1])
        {
            return;
        }
        
        SaveLoadSystem.Load();
        int requiredMoney = MaxCapacityCost[SaveLoadSystem.instance.upgrades[1 +SceneIndex * 10 ]];
        if (PlayerMoneyData.instance.TotalMoney < requiredMoney)
        {
            createDontEnoghMoneyText();
            return;
        }

        PlayerMoneyData.instance.TotalMoney -= requiredMoney;
        SaveLoadSystem.instance.upgrades[1 +SceneIndex * 10 ]++;
        SaveLoadSystem.Save();
        SceneData.instance.CheckUpgrades();
        MMVibrationManager.Haptic (HapticTypes.MediumImpact);
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
        int requiredMoney = StuffMoveSpeedCost[SaveLoadSystem.instance.upgrades[2 +SceneIndex * 10 ]];
        if (PlayerMoneyData.instance.TotalMoney < requiredMoney)
        {
            createDontEnoghMoneyText();
            return;
        }
        
        MMVibrationManager.Haptic (HapticTypes.MediumImpact);
        PlayerMoneyData.instance.TotalMoney -= requiredMoney;
        SaveLoadSystem.instance.upgrades[2 +SceneIndex * 10 ]++;
        SaveLoadSystem.Save();
        SceneData.instance.CheckUpgrades();
        
        UpdateText();
        SaveLoadSystem.Load();
        for (int i = 0; i < PlayerAIdatabase.instance.AllPlayerAIlist.Count; i++)
        {
            PlayerAIdatabase.instance.AllPlayerAIlist[i].CheckUpgrades();
        }
        
    }

    private int SceneIndex;
    private void Awake()
    {
        
    }

    void Start()
    {
        SceneIndex = UpgradeController2.instance.SceneIndex;
        UpdateText();
        CheckUpgrades();

        for (int i = 0; i < 40; i++)
        {
            Debug.Log("Table ID:" + i + " " + SaveLoadSystem.instance.TableUnlock[i] );
        }
    }

    private void CheckUpgrades()
    {
        SaveLoadSystem.Load();

        for (int i = 0; i < SaveLoadSystem.instance.upgrades[0 +SceneIndex * 10 ] ; i++)
        {
            PlayerAIdatabase.instance.AllPlayerAIlist[i].EnableAI();
        }
    }

    IEnumerator CloseAndOpenUI(float timer = 3f)
    {
        UpgradeCanvas.SetActive(false);

        yield return new WaitForSeconds(timer);
        
        UpgradeCanvas.SetActive(true);
    }

    private void UpdateText()
    {
        if (HireStuffCosts.Length <= SaveLoadSystem.instance.upgrades[0 +SceneIndex * 10 ])
        {
            allUpgrades[0].text = "MAX";

            pressable[0] = true;
        }
        else
        {
            if (SaveLoadSystem.instance.upgrades[0 +SceneIndex * 10 ] == 0)
            {
                allUpgrades[0].text = "$" + HireStuffCosts[SaveLoadSystem.instance.upgrades[0 +SceneIndex * 10 ]];
                
                pressable[0] = false;
            }
            
            if (SaveLoadSystem.instance.upgrades[0 +SceneIndex * 10 ] == 1)
            {
                if (SaveLoadSystem.instance.TableUnlock[5 + SceneIndex * 20] == -1)
                {
                    allUpgrades[0].text = "LOCKED";
                    
                    pressable[0] = true;
                }
                else
                {
                    allUpgrades[0].text = "$" +HireStuffCosts[SaveLoadSystem.instance.upgrades[0 +SceneIndex * 10 ]];
                    
                    pressable[0] = false;
                }
            }

            if (SaveLoadSystem.instance.upgrades[0 +SceneIndex * 10 ] == 2)
            {
                if (SaveLoadSystem.instance.TableUnlock[10  + SceneIndex * 20 ] == -1)
                {
                    allUpgrades[0].text = "LOCKED";
                    
                    pressable[0] = true;
                }
                else
                {
                    allUpgrades[0].text = "$" +HireStuffCosts[SaveLoadSystem.instance.upgrades[0 +SceneIndex * 10 ]];
                    
                    pressable[0] = false;
                }
            }
            
            if (SaveLoadSystem.instance.upgrades[0 +SceneIndex * 10 ] == 3)
            {
                if (SaveLoadSystem.instance.TableUnlock[19  + SceneIndex * 20] == -1)
                {
                    allUpgrades[0].text = "LOCKED";
                    
                    pressable[0] = true;
                }
                else
                {
                    allUpgrades[0].text = "$" +HireStuffCosts[SaveLoadSystem.instance.upgrades[0 +SceneIndex * 10 ]];
                    
                    pressable[0] = false;
                }
            }

            if (SaveLoadSystem.instance.upgrades[0 +SceneIndex * 10 ] == 4)
            {
                allUpgrades[0].text = "MAX";
                    
                pressable[0] = true;
            }
        }

        if (MaxCapacityCost.Length <= SaveLoadSystem.instance.upgrades[1 +SceneIndex * 10 ])
        {
            allUpgrades[1].text = "MAX";

            pressable[1] = true;
        }
        else
        {
            allUpgrades[1].text = "$" + MaxCapacityCost[SaveLoadSystem.instance.upgrades[1 +SceneIndex * 10 ]];
        }

        if (StuffMoveSpeedCost.Length <= SaveLoadSystem.instance.upgrades[2 +SceneIndex * 10 ])
        {
            allUpgrades[2].text = "MAX";

            pressable[2] = true;
        }
        else
        {
            allUpgrades[2].text = "$" + StuffMoveSpeedCost[SaveLoadSystem.instance.upgrades[2 +SceneIndex * 10 ]];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UpgradeCanvas.SetActive(true);
            
            UpdateText();
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

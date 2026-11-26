using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class UpgradeBase : MonoBehaviour
{
    public string upgradeName;
    public int upgradeCost = 1;

    public int maxUpgrade = 3;
    public int currentUpgrade = 0;

    public List<UpgradeBase> connectedUpgrades;
    public bool isUnlocked;

    public TextMeshProUGUI upgradeText;
    public TextMeshProUGUI amountText;
    public Button upgradeButton;

    public abstract void Upgrade();

    public abstract void UnlockUpgrade();

    public abstract void LockUpgrade();

    public void UpdateUI() {
        upgradeText.text = upgradeName;
        amountText.text = $"{currentUpgrade}/{maxUpgrade} | {upgradeCost}SP";
        upgradeButton.interactable = isUnlocked;
    }
}

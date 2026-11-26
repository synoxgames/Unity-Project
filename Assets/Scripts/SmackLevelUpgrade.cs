using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmackLevelUpgrade : UpgradeBase
{
    public IEUpgradable upgrade;
    public override void LockUpgrade() {
        isUnlocked = false;
        UpdateUI();
    }

    public override void UnlockUpgrade() {
        isUnlocked = true;
        UpdateUI();
    }

    public override void Upgrade() {
        
    }
}

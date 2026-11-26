using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    public int perkPoints = 0;

    public static SkillTreeManager instance;

    private void Awake() {
        if (instance != this && instance != null) {
            print("WOAH! WE ACTUALLY GOT THIS");
            Destroy(instance);
        }

        instance = this;
    }

    public void AddPerkPoint(int perkPoints) {
        perkPoints += perkPoints;
    }

    public bool SpendPerkPoints(int cost) {
        if (cost > perkPoints) return false;

        perkPoints -= cost;
        return true;
    }
}

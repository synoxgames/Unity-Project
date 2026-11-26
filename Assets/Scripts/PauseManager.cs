using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public List<IEPausable> allPausableItems = new List<IEPausable>();
    public bool isGamePaused = false;

    private void Start() {
        PopulateList();
    }

    private void Update() {
        
        if (Input.GetKeyDown(KeyCode.X)) {
            if (!isGamePaused) PauseGame();
            else UnPauseGame();
        }
    }

    public void PopulateList() {
        if (allPausableItems.Count > 0) allPausableItems.Clear();
        allPausableItems.AddRange(FindInterfaces<IEPausable>(true));
    }

    public void PauseGame() {
        foreach (IEPausable pausable in allPausableItems) {
            pausable.OnPause();
        }

        isGamePaused = true;
    }

    public void UnPauseGame() {
        foreach (IEPausable pausable in allPausableItems) {
            pausable.OnUnPause();
        }

        isGamePaused = false;
    }

    public static List<T> FindInterfaces<T>(bool includeInactive = false) where T : class {
        FindObjectsInactive inactiveMode = includeInactive
            ? FindObjectsInactive.Include
            : FindObjectsInactive.Exclude;

        return FindObjectsByType<MonoBehaviour>(inactiveMode, FindObjectsSortMode.None)
            .OfType<T>()
            .ToList();
    }
}

using System;
using UnityEngine;

public class LoseManager : MonoBehaviour
{
    [SerializeField] private GameObject LosePanel;
    public static bool isGameOver { get; private set; }
    private void Awake()
    {
        isGameOver = false;
    }

    public void SetActiveLosePanel()
    {
        LosePanel.SetActive(true);
        isGameOver = true;
    }

    private void OnDestroy() => isGameOver = false;
}

using System;
using UnityEngine;

public class Achievement
{
    public event Action OnComplete;
    public event Action<Achievement> OnIncreaseValue;
    public string Name { get; private set; }
    public string Description { get; private set; }
    private int currentAmount;
    private int targetAmount;
    public Achievement(string name, string description, int targetAmount)
    {
        this.Name = name;
        this.Description = description;
        this.targetAmount = targetAmount;

        this.OnComplete += Completion;

        if (PlayerPrefs.HasKey($"{this.Name}-amount"))
        {
            currentAmount = PlayerPrefs.GetInt($"{this.Name}-amount");
            Debug.Log("Current Amount: " + currentAmount);
        }
    }
    public void IncreaseCurrent()
    {
        if (IsCompleted()) return;

        currentAmount++;
        PlayerPrefs.SetInt($"{this.Name}-amount", currentAmount);

        if (currentAmount == targetAmount)
            OnComplete();
    }
    public bool IsCompleted() => currentAmount >= targetAmount;
    private void Completion()
    {
        if (PlayerPrefs.GetInt(this.Name) == 1)
        {
            Debug.Log($"{this.Name} already completed!");
            return;
        }
        PlayerPrefs.SetInt(this.Name, 1);
    }

}
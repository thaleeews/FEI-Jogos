using UnityEngine;
using System;

public class Fruit : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int healthRecovered = 2;
    public static Action<int> OnFruitCollected;

    public void Collect() {
        OnFruitCollected?.Invoke(healthRecovered);
        animator.SetTrigger("Collect");
    }
}

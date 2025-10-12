using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public class ManagerLight : MonoBehaviour
{
    public List<Image> fireIcons;
    public int maxFire = 5;
    public int currentFire;



    private void Start()
    {
        currentFire = maxFire;
        UpdateFire();

    }

    private void UpdateFire()
    {
        for (int i = 0; i < fireIcons.Count; i++)
        {
            fireIcons[i].enabled = (i < currentFire);
        }
    }

    public void AddFire(int amount)
    {
        currentFire = Mathf.Clamp(currentFire - amount, 0, maxFire);
        UpdateFire();
    }

    public void UseFire(int amount)
    {
        currentFire = Mathf.Clamp(currentFire - amount,0,maxFire);
        UpdateFire();
    }
}


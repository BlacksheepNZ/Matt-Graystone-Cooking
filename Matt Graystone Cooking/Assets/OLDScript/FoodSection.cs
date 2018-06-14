using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 
/// </summary>
public interface FoodSection
{
    bool CanPurchase();
    float CostToBuy();

    void Update();
    void Upgrade();
    void FirstTimePurchase();

    IEnumerator UpdateTimer();
    void ResetTimer();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmountMoney
{
    public double amount = 0.00E0D;

    public AmountMoney(double amount)
    {
        this.amount = amount;
    }



    // Update the amount at the specific level
    public void updateAmount(double amount = 0.00E0D)
    {
        this.amount += amount;
    }

    public double getAmount()
    {
        return amount;
    }

    

    
     public string ToString()
    {
        
        int power = (int)Mathf.Floor(Mathf.Log10((float)amount));


        int reste = power % 3;


        int div = power / 3;


        double d5 = amount / Mathf.Pow(10, 3 * div);


        string testing = d5.ToString();
        if (reste == 0)
        {
            reste = 1;
        }
        string final = testing;
        if (testing.Length > 3)
        {
            final = testing.Substring(0, 5 - reste);
        }

        string letter = "";
        if (div < 1)
        {
            letter = "";
        }
        if (div == 4)
        {
            letter = " T";
        }
        if (div == 3)
        {
            letter = " B";
        }
        if (div == 2)
        {
            letter = " M";
        }

        if (div == 1)
        {
            letter = " K";
        }

        return final + letter;
    }

    private string getLetter(double amount)
    {
        if (amount.CompareTo(1E3D) < 1)
        {
            return "";
        }
        if (amount.CompareTo(1E12D) > 0)
        {
            return " T";
        }
        if (amount.CompareTo(1E9D) > 0)
        {
            return " B";
        }
        if (amount.CompareTo(1E6D) > 0)
        {
            return " M";
        }
        
        if (amount.CompareTo(1E3D) > 0)
        {
            return " K";
        }
        return "Abcd";
    }
}

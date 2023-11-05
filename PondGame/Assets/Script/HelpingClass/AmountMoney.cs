using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmountMoney : MonoBehaviour
{
    public List<int> listGold = new List<int>();
    public string letter = "";

    public AmountMoney(int amount, string letter)
    {
        for (int i = 0; i < 10; i++)
        {
            listGold.Add(0);
        }
        int index = getIndexForLetter(letter);
        listGold[index] = amount;
    }

    public void updateLetter()
    {
        int lim = 0;
        for (int i = 0; i < listGold.Count; i++)
        {
            if (listGold[i] == 0)
            {
                lim = i - 1;
                break;
            }
        }
        switch (lim)
        {
            case 1:
                letter = "K";
                break;
            case 2:
                letter = "M";
                break;
            case 3:
                letter = "B";
                break;
            case 4:
                letter = "T";
                break;
            case 5:
                letter = "Q";
                break;
            case 6:
                letter = "Qu";
                break;
            case 7:
                letter = "Sx";
                break;
            case 8:
                letter = "Se";
                break;
            case 9:
                letter = "Oc";
                break;
            default:
                letter = "";
                break;
        }
    }

    public void updateAllAmount(AmountMoney amount)
    {
        int r = 0;
        int tmp = 0;
        for (int  i = 0; i < listGold.Count && i < amount.listGold.Count; i++)
        {
            tmp = listGold[i] + amount.listGold[i] + r;
            r = 0;
            if (tmp > 999)
            {
                tmp = tmp - 1000;
                r = 1;
            }
            listGold[i] = tmp;
        }
        updateLetter();
    }


    // Update the amount at the specific level
    public void updateAmount(int amount, string letter)
    {
        int i = getIndexForLetter(letter);
        _updateAmount(amount, i);
        updateLetter();
    }

    private void _updateAmount(int amount, int index)
    {
        if (index < listGold.Count)
        {
            int tmp = amount + listGold[index];
            if (tmp > 999)
            {
                _updateAmount(tmp % 1000, index + 1);
                tmp -= 1000;
            }
            listGold[index] = tmp;
        }
    }

    public (int,string) getAmount()
    {
        int result = 0;
        int lim = 0;
        for (int i = 0; i < listGold.Count; i++)
        {
            if (listGold[i] == 0)
            {
                lim = i - 1;
                break;
            }
        }
        result = listGold[lim];

        return (result,letter);
    }

    public string ToString()
    {
        int result = 0;
        int lim = 0;
        for (int i = 0; i < listGold.Count; i++)
        {
            if (listGold[i] != 0)
            {
                lim = i;
            }
        }
        result = listGold[lim];

        return "" + result + " " + letter;
    }

    public int getIndexForLetter(string letter)
    {
        switch (letter)
        {
            case "K":
                return 1;
            case "M":
                return 2;
            case "B":
                return 3;
            case "T":
                return 4;
            case "Q":
                return 5;
            case "Qu":
                return 6;
            case "Sx":
                return 7;
            case "Se":
                return 8;
            case "Oc":
                return 9;
            default:
                return 0;
        }
    }

    public void substractAllAmount(AmountMoney amount)
    {
        int r = 0;
        int tmp = 0;
        for (int i = 0; i < listGold.Count && i < amount.listGold.Count; i++)
        {
            tmp = listGold[i] - amount.listGold[i] - r;
            r = 0;
            if (tmp < 0)
            {
                tmp = 1000 - tmp;
                r = 1;
            }
            listGold[i] = tmp;
        }
        updateLetter();
    }

    public void copyAmount(AmountMoney amount)
    {
        for (int i = 0;i < listGold.Count; i++)
        {
            listGold[i] = amount.listGold[i];
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueRequirement {
    public enum CheckType
    {
        Value, NPCMoney, PlayerMoney
    }

    public enum ValueCheck
    {
        LargerThan, LargerOrEqual, Equal, LessOrEqual, LessThan, NotEqual
    }

    public CheckType checkType;
    public ValueCheck valueCheck;
    public CheckType valueType;
    public string equalsValue;

    public bool Check(NPC npc, Character player)
    {
        if(checkType == CheckType.Value)
        {
            Debug.LogWarning("Check type can not be 'Value'!");
            return false;
        }
        else
        {
            string valueToCheck;

            switch (valueType)
            {
                case CheckType.NPCMoney:
                    valueToCheck = npc.money.ToString();
                    break;

                case CheckType.PlayerMoney:
                    valueToCheck = player.money.ToString();
                    break;

                default:
                    valueToCheck = equalsValue;
                    break;
            }

            switch (checkType)
            {
                case CheckType.NPCMoney:
                    return Compare(npc.money, int.Parse(valueToCheck));
                case CheckType.PlayerMoney:
                    return Compare(player.money, int.Parse(valueToCheck));
            }

            return false;
        }
    }

    private bool Compare(string val, string than)
    {
        switch (valueCheck)
        {
            case ValueCheck.Equal:
                return val.Equals(than);

            case ValueCheck.NotEqual:
                return !val.Equals(than);

            default:
                Debug.LogWarning("Comparison is not allowed!");
                return false;
        }
    }

    private bool Compare(int val, int than)
    {
        switch (valueCheck)
        {
            case ValueCheck.LargerThan:
                return val > than;

            case ValueCheck.LargerOrEqual:
                return val >= than;

            case ValueCheck.Equal:
                return val == than;

            case ValueCheck.LessOrEqual:
                return val <= than;

            case ValueCheck.LessThan:
                return val < than;

            case ValueCheck.NotEqual:
                return val != than;

            default:
                Debug.LogWarning("Comparison is not allowed!");
                return false;
        }
    }

    private bool Compare(float val, float than)
    {
        switch (valueCheck)
        {
            case ValueCheck.LargerThan:
                return val > than;

            case ValueCheck.LargerOrEqual:
                return val >= than;

            case ValueCheck.Equal:
                return val == than;

            case ValueCheck.LessOrEqual:
                return val <= than;

            case ValueCheck.LessThan:
                return val < than;

            case ValueCheck.NotEqual:
                return val != than;

            default:
                Debug.LogWarning("Comparison is not allowed!");
                return false;
        }
    }

}

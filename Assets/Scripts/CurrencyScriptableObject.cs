using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CurrencyData", menuName = "Currency Data", order = 1)]
public class CurrencyScriptableObject : ScriptableObject
{
    [SerializeField] public List<CurrencyData> currencyData;

    [System.Serializable]
    public class CurrencyData
    {
        [SerializeField] public string currencyValue;
        [SerializeField] public Binding binding;
    }

    [System.Serializable]
    public class Binding
    {
        [SerializeField] public string countryName;
        [SerializeField] public string currencyType;
    }
}

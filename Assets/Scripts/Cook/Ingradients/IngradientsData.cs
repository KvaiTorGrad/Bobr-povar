using System;
using UnityEngine;

[CreateAssetMenu(fileName = "IngradientsData", menuName = "IngradientsData")]
public class IngradientsData : ScriptableObject
{
    [Serializable]
    public struct IngradientParametrs
    {
        public IngradientIDName ingradientID;
        public string nameRU;
        public string nameEng;
        public int index;
        public GameObject fantom;
    }
    [SerializeField] private IngradientParametrs[] _paramters;
    public IngradientParametrs[] Parametrs => _paramters;
}
public enum IngradientIDName
{
    Strawberries,
    Cones,
    Nuts,
    Mushrooms,
    FlyAgaric,
    Grass,
    Bugs,
    Sawdust,
    Salt,
    Sugar,
    Pepper,
    Fish
}
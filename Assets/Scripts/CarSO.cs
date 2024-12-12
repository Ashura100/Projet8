using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CarSo")]
public class CarSO : ScriptableObject
{
    public RenderTexture image;
    public string objectName;
    public string description;
    public GameObject gameOject;
    public Material color;
    public string energy;
    public float ch;
    public float weights;
    public float years;
    public float price;
}
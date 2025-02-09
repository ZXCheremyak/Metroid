using UnityEngine;

[CreateAssetMenu(menuName = "Element", fileName = "Element")]
public class Element : ScriptableObject
{    
    public GameObject particles;

    public ElementType elementType;

    public Element strongerElement;

    public Element weakerElement;

    public AudioClip swapSound;

    public Color particleColor;
}

public enum ElementType
{
    Physical,
    Fire,
    Water,
    Air,
    Earth
}
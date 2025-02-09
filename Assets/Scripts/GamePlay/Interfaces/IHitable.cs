using UnityEngine;

public interface IHitable
{
    public void TakeDamage(float value, Element attackingElement);
}

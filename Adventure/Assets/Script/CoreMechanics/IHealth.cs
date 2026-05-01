using UnityEngine;

public interface IHealth
{
    public int Health{get;}
    public void TakeDamage(int damage);
}

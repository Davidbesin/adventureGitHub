using UnityEngine;

public interface IEnemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   

    // Update is called once per frame
    public void MyDefault();
    public bool isAttacking {get;}
    public void Die();
    

}

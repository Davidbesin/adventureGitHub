using UnityEngine;

public class DropDownClick : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool dropped;
    

    public GameObject dropDownBar;

   public void DropOrNoDrop()
   {
        dropped = !dropped;
        dropDownBar.SetActive(dropped);
   }
}

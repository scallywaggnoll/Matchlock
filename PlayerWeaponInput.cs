using UnityEngine;

public class PlayerWeaponInput : MonoBehaviour
{
	public GameObject equippedWeapon;
	public GameObject equippedMeleeWeapon;	
	
	private BlackpowderWeapons primaryWeaponScript;
	
	public Animator playerAnimator;
	public bool isArmed;
	
	
	void Start()
	{
		
	}

    // Update is called once per frame
    void Update()
    {
		if (equippedWeapon != null)
        {
            primaryWeaponScript = equippedWeapon.GetComponent<BlackpowderWeapons>();
        }
		
        if (equippedWeapon != null && equippedWeapon.activeInHierarchy && primaryWeaponScript != null)
        {
            primaryWeaponScript.MyInput();
        }
    }
	
	public void PlayerWeaponAnimations()
	{
		if (Input.GetKeyDown(KeyCode.LeftControl))
        {
			isArmed = true;
		} 
		else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
			isArmed = false;
		}		
		
		if (isArmed)
		{
			playerAnimator.SetBool("isArmed", true);
		}
		else
		{
			playerAnimator.SetBool("isArmed", false);
		}
	}
	
	
}

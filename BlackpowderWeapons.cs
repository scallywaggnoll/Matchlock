using UnityEngine;

public class BlackpowderWeapons : MonoBehaviour
{
	public float damageBullet;
	public float damageRange;
	
	public float range;
	public float spread; //accuracy
	public float reloadTime; //Time for entire animation and stuff
	
	[Range(0.5f, 0.9f)] //Calibers go from .50 to .90
	public float caliberBonus;
	public int bulletsPerShot;
	public LayerMask destructibleLayer;
	
	public bool shooting;
	public bool reloading;
	public bool readyToShoot;
	
	[Range(0, 4)]
	public int cartridgeLoaded; //1 - Powder Loaded, 2 - Bullets and Wad Loaded, 3 - Rammed, 4 - Primer added, 0 - Fired.
	[Range(0, 2)]
	public float powderCharge; //used blackpowder, can be overloaded
	public float powderChargeOverLoad; //Once powdercharge is more than 2, this will be added, to cause an explosion.
	[Range(0, 50)]
	public int leadPellets; //Used pellets.
	
	public float recoil;
	public float knockback;
	
	public GameObject barrelPoint;
	public Transform attackPoint;
	public RaycastHit rayHit;
	
	public void MyInput()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0) && !reloading && readyToShoot)
		{
			Shoot();
			Debug.Log("Player Shot");
		}
		
		if (Input.GetKeyDown(KeyCode.R) && cartridgeLoaded < 4)
		{
			Reload();
			Debug.Log("Player Reloaded");
		}
	}
	
	void Shoot()
	{
		readyToShoot = false;
		cartridgeLoaded -= 4;
		powderCharge -= 2;
		leadPellets -= 50;
		
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
		
		Vector3 direction = barrelPoint.transform.forward + new Vector3(x, y, 0);
		

		
		if (Physics.Raycast(barrelPoint.transform.position, direction, out rayHit, range, destructibleLayer))
        {
            Debug.Log(rayHit.collider.name);
			
			TargetHealthSystem enemyHealth = rayHit.collider.GetComponent<TargetHealthSystem>();
			Destructibles destructiblesHealth = rayHit.collider.GetComponent<Destructibles>();
			
            if (rayHit.collider.CompareTag("Enemy"))
			{
				if (enemyHealth != null)
				{
					enemyHealth.hitPoints -= (damageBullet + Random.Range(-damageRange, damageRange));
				}
				Debug.Log("We have shot the enemy");
			}
			else if (rayHit.collider.CompareTag("Ally"))
			{
				Debug.Log("You blithering idiot, hitting your allies?");
			}
			else if (rayHit.collider.CompareTag("Neutral"))
			{
				Debug.Log("Really? Hitting innocents?");
			}	
			else if (rayHit.collider.CompareTag("Destructible"))
			{
				Debug.Log("We have hit something...");
			}	
			else if (rayHit.collider.CompareTag("Player"))
			{
				Debug.Log("We have hit the local imbecile");
			}		              
        }

	}

	void Reload()
	{
		reloading = true;
		Invoke(nameof(ReloadFinished), reloadTime);
	}
	void ReloadFinished()
	{
		if (cartridgeLoaded == 0) //Empty
		{
			cartridgeLoaded++;
		}
		
		if (cartridgeLoaded == 1) //Powder
		{
			powderCharge += caliberBonus;
			
			if (powderCharge > 2)
			{
				powderChargeOverLoad += caliberBonus;
			}
			
			cartridgeLoaded++;
		}
		if (cartridgeLoaded == 2) //Wad and pellets
		{
			leadPellets += bulletsPerShot;
			cartridgeLoaded++;			
		}
		
		if (cartridgeLoaded == 3) //Rammed
		{
			cartridgeLoaded++;			
		}
		
		if (cartridgeLoaded == 4) //Primered
		{
			readyToShoot = true;			
			reloading = false;
		}
		
	}

	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //MyInput();
    }
}

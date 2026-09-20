using System.Collections;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
	[Range(1, 5)]
	public int damageType; //1 = Blades, 2 = Spears, 3 = Axes, 4 = Blunt, 5 = Halberd - ANIMATION SETTINGS AND ATTACK TYPES
	public bool throwable;
	
	public float damageMelee;
	public float damageMeleeRange; //damage Range
	public float attackSpeed; //seconds between attacks.

	
	[Range(0, 1)]
	public float attackForce; //so to do damage while attacking, not just touching the enemy will damage in full.
	
	public Collider meleeCollider;
	
	//public LayerMask enemyMask;
	//add 2 more for limb collisions. to add extra damage
	
	//There are 4 tags for ai detection - Enemy, Ally, Neutral, Destructible and Player...
			
	//AI Behaviourv and animation
	public bool isAttacking;
	public bool readyToAttack;
	
	
	public void Attack()
	{
		if (!readyToAttack)
		{
			isAttacking = true;
			
			if (isAttacking)
			{
				attackForce = 1f;
				
			}
			else
			{
				attackForce = 0f;
			}
		}
		
		readyToAttack = true;
	}
	
	
	void OnTriggerEnter(Collider other)
	{
		if (!isAttacking) return;
		
		TargetHealthSystem enemyHealth = other.GetComponent<TargetHealthSystem>();
		Destructibles destructiblesHealth = other.GetComponent<Destructibles>();
		
		if (other.CompareTag("Enemy"))
		{
			
			if (enemyHealth != null)
			{
				enemyHealth.hitPoints -= (damageMelee + Random.Range(-damageMeleeRange, damageMeleeRange)) * attackForce;
			}
			
			Debug.Log("We have hit the enemy");
		}
		else if (other.CompareTag("Ally"))
		{
			Debug.Log("You blithering idiot, hitting your allies?");
		}
		else if (other.CompareTag("Neutral"))
		{
			Debug.Log("Really? Hitting innocents?");
		}	
		else if (other.CompareTag("Destructible"))
		{
			Debug.Log("We have hit something...");
		}	
		else if (other.CompareTag("Player"))
		{
			Debug.Log("We have hit the local imbecile");
		}		
	}
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && readyToAttack)
		{
			Attack();
			readyToAttack = false;
		}
 
    }
}

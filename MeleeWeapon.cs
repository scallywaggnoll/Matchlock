using System.Collections;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
	[Range(1, 5)]
	public int damageType; //1 = Blades, 2 = Spears, 3 = Axes, 4 = Blunt, 5 = Halberd - ANIMATION SETTINGS AND ATTACK TYPES
	public bool throwable;
	
	public float damageMelee;
	public float damageMeleeRange; //damage Range
	public float attackCooldown; //seconds between attacks.

	
	[Range(0, 1)]
	public float attackForce; //so to do damage while attacking, not just touching the enemy will damage in full.
	
	public GameObject meleeAttackPoint;
	public Transform attackPoint;
	public RaycastHit rayHit;
	public float meleeRange;
	public LayerMask destructibleMeleeLayer;
	
	//public LayerMask enemyMask;
	//add 2 more for limb collisions. to add extra damage
	
	//There are 4 tags for ai detection - Enemy, Ally, Neutral, Destructible and Player...
			
	//AI Behaviourv and animation
	public bool readyToAttack;
	
	public void AttemptAttack()
	{
		if (!readyToAttack)
		{
			return;
		}
		
		Attack();
	}
	
	public void Attack()
	{	
		float x = Random.Range(0f, 0f);
        float y = Random.Range(0f, 0f);
		
		Vector3 direction = meleeAttackPoint.transform.forward + new Vector3(x, y, 0);
		
		if (Physics.Raycast(meleeAttackPoint.transform.position, direction, out rayHit, meleeRange, destructibleMeleeLayer))
        {
            Debug.Log(rayHit.collider.name);
			
			TargetHealthSystem enemyHealth = rayHit.collider.GetComponent<TargetHealthSystem>();
			Destructibles destructiblesHealth = rayHit.collider.GetComponent<Destructibles>();
			
            if (rayHit.collider.CompareTag("Enemy"))
			{
				if (enemyHealth != null)
				{
					enemyHealth.hitPoints -= (damageMelee + Random.Range(-damageMeleeRange, damageMeleeRange));
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
		
	public IEnumerator WeaponCooldownTime()
	{
		readyToAttack = false;
		
		yield return new WaitForSeconds(attackCooldown);
		
		readyToAttack = true;
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

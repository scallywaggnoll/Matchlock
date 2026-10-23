using UnityEngine;

public class PlayerSupplyUsage : MonoBehaviour
{
	public PlayerHealth playerHealthRefrence;
	
	[Range(0, 100)]
	public float rationsAmount; //food
	[Range(0, 100)]
	public float waterAmount; //water
	[Range(0, 100)]
	public float moonshineAmount;
	[Range(0, 100)]
	public int smokes;
	[Range(0, 100)]
	public float laudanumAmount;
	
	public float ammunitionAmount; //ammo for muskets
	
	
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
	
	public void UseRations()
	{
		rationsAmount -= 1f;
	}
	
	public void UseWater()
	{
		waterAmount -= 1f;	
	}
	
	public void UseAlcohol()
	{		
		moonshineAmount -= 1f;			
	}
	
	public void Smoke()
	{
		smokes -= 1;				
	}
	
	public void UseLaudanum()
	{
		laudanumAmount -= 1f;		
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}

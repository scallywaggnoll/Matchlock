using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
	[Range(0, 600)]
	public float hitPoints;
	public float maxHP; //for every point of hp, get 1.5 point of stamina
	
	public float regenRate;
	[Range(0, 750)]
	public float stamina;
	public float maxStamina;
	
	[Range(-100, 100)]
	public float condition;
	[Range(0, 100)]
	public float oxygenLevel; //for drowing
	
	public float bleedRate;
	
	[Range(0, 200)]
	public float defense;
	
	[SerializeField]
	private TextMeshProUGUI healthText;
	[SerializeField]
	private TextMeshProUGUI staminaText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxStamina = maxHP * 1.5f;
		stamina = maxStamina;
		hitPoints = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        hitPoints = Mathf.Min(hitPoints, maxHP);
		hitPoints -= bleedRate * Time.deltaTime;
		hitPoints += regenRate * Time.deltaTime;
		
		stamina = Mathf.Min(stamina, maxStamina);
		stamina += regenRate * Time.deltaTime;
		oxygenLevel += regenRate * Time.deltaTime;
		
		TextDisplay();
    }
	
	void TextDisplay()
	{
		float hitPointPercent;
		float staminaPointsPercent;
		
		hitPointPercent = hitPoints / maxHP;
		staminaPointsPercent = stamina / maxStamina;
		
		healthText.text = ("HP - " + hitPointPercent.ToString("P0"));
	}
	
	void PlayerDeath()
	{
		
	}
}

using UnityEngine;

public class EnvironmentTimeSystem : MonoBehaviour
{
	public float secondsPerDay = 900f;
	public float dayTimer = 0f;
	public float daysPassed;
	
	public float minutes;
	public float hours;
	
	public float day;
	public float month; //dependnns on level
	public float year; //1654

	public void UpdateTimer()
	{
		if (dayTimer >= secondsPerDay)
		{
			dayTimer = 0f;
			daysPassed += 1f;
		}
		else if (dayTimer < secondsPerDay)
		{
			dayTimer += 1 * Time.deltaTime;
		}
	}
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }
}

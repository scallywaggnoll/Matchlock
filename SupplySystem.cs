using UnityEngine;

public class SupplySystem : MonoBehaviour
{
	public supplyData[] supplies;
	
	[System.Serializable]
	public class supplyData
	{
		public string supplyName;
		
		public float gain; //Gain per unit of time
		public float amount; //TotalSupply
		public float useLoss; //loss or use of item.
		
		public float useType; //0 = Military good, 1 = Staple Good - Food/Water, 2 = Leisure, 3 = Medical, 4 - Raw Material
		public float useMult; //Mult of good use.
		
		public bool usesStorage; //dictates if this good uses units of storage.
		
		public float tradePrice;
		public int tradeID; //Id for consumption 
		//[1=Rations][2=FreshGameFish][3=Water][4=Alcohol][5=MilitarySupplies][6=Gunpowder][7=Fabric/Clothes][8=Furs][9=Iron/Steel][10=MedicalSupplies][11=Wood][12=TobbaccoHerbs]
	}
	
	public float totalStorage; //total Amount of barrels, crates, and other forms of storage.
	public float currentStored; //amount of every item
	public float requiredStorage; //required for currentStored if it surpasses storage.
	
	public float generalSufferingIndex; //Injuries of soldiers or shortages... lowers morale.
	
	public int nonCombatants; //Civilians... not the number of warmen, Laborer size
	public int infantrySize; //War party from YOU
	public int enemyWarParty; //Enemies to spawn every so oftern
	
	public float labourProduced;
	public float labourUsed;
	
	public float avgMorale;
	
	//Weather conditions
	
	[Range(1, 4)]
	public int currentSeason;
	public float rainIntensity;
	public float snowIntensity;

	//ADD a new array for soldiers... 
	
	public void GarrisonLabourUse()
	{
		labourProduced = nonCombatants / (generalSufferingIndex + 1f);
	}

	public void GoodProductionSchemes()
	{
		supplyData rations = null;
		supplyData freshGame = null;
		
		foreach (supplyData supply in supplies)
		{		
			//Rations[1]
			if (supply.tradeID == 1) rations = supply;
			
			//Fresh Game/Fish[2]
			else if (supply.tradeID == 2) freshGame = supply;
		}
		
		if (rations == null || freshGame == null) return; //add "|| above foreach line name == null"
		
		//Rations
		rations.amount += rations.gain - rations.useLoss; //should be per day.
		rations.gain = labourUsed * 40f; //should add a mininum required labour to labourused to produce good.
		rations.useLoss = nonCombatants + infantrySize; //normal rate
	}
	
	public void StorageAmountGoods()
	{
		currentStored = 0f;
		
		foreach (supplyData supply in supplies)
		{		
			if (supply.usesStorage)
			{
				currentStored += supply.amount;
			}
			
			
		}			
	}
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StorageAmountGoods();
		GoodProductionSchemes();
    }
}

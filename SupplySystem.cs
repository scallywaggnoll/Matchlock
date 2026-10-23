using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplySystem : MonoBehaviour
{
	
	[System.Serializable]
	public class supplyData
	{
		public string supplyName;
		
		public float gain; //Gain per unit of time
		public float amount; //TotalSupply
		public float useLoss; //loss or use of item
		
		public float useType; //0 = Military good, 1 = Staple Good - Food/Water, 2 = Leisure, 3 = Medical, 4 - Raw Material
		public float useMult; //Mult of good use.
		
		public bool usesStorage; //dictates if this good uses units of storage.
		
		public float tradePrice;
		public int tradeID; //Id for consumption 
		//[1=Rations][2=FreshGameFish][3=Water][4=Alcohol][5=MilitarySupplies][6=Gunpowder][7=Fabric/Clothes][8=Furs][9=Iron/Steel][10=MedicalSupplies][11=Wood][12=TobbaccoHerbs]
		
		public float consumptionPerLaborer;
		public float consumptionPerWarman;
		
		public float labourAllocatedToProduction; //Since there can only be 1 PM at a time, this can change. Labour Required to produce 1u/ of this unit
		public float currentLabourAllocated; //Labour Used.
		
		public float baseConsumption;
		
	}
	
	public float totalStorage; //total Amount of barrels, crates, and other forms of storage.
	public float currentStored; //amount of every item
	public float requiredStorage; //required for currentStored if it surpasses storage.
	
	[Range(0, 100)]
	public float generalSufferingIndex; //Injuries of soldiers or shortages... lowers morale.
	
	public int nonCombatants; //Civilians... not the number of warmen, Laborer size
	public int infantrySize; //War party from YOU
	public int enemyWarParty; //Enemies to spawn every so oftern
	public int currentGarrisonInjuredSick; //Sum of everyone who is sick/injured.
	
	public float labourProduced; //Total Labour Produced
	public float labourUsed; //Total Labour Used in currentLabourAllocated
	public float totalLabourAvailable; //totalLabourAvailableToUse
	public float totalLabourPossible;
	
	public float totalFirstAidUsed; //First aid, is how much labour is Used in care. Feeding the sick, wounded and Injured.
	
	public float avgMorale;

	//Weather conditions

	[Range(1, 4)]
	public int currentSeason;
	public float rainIntensity;
	public float snowIntensity;
	
	public EnvironmentTimeSystem daysPassedScript;

    

    //ADD a new array for soldiers... 



    public void GarrisonLabourUse()
	{
		labourProduced = nonCombatants / (generalSufferingIndex + 1f);
		totalLabourPossible = nonCombatants;
		
		foreach (KeyValuePair<string, supplyData> supply in supplies)
		{
			totalLabourAvailable = totalLabourPossible - (labourUsed + supply.Value.currentLabourAllocated + totalFirstAidUsed); 
		}
	}

    public Dictionary<string, supplyData> supplies = new Dictionary<string, supplyData>();

    public void InitializeSupplies()
	{
        supplies.Add("rations", new supplyData());
        supplies["rations"].tradeID = 1;
		supplies["rations"].baseConsumption = 0f; //Doesnt Get Consumed via PM's, instead uses consumptionPerLaborer and consumptionPerWarman
		supplies["rations"].consumptionPerLaborer = 1f;
		supplies["rations"].consumptionPerWarman = 2f;
		supplies["rations"].labourAllocatedToProduction = 1f; //labour required to produce 1u/good
		
        supplies.Add("freshGame", new supplyData());
        supplies["freshGame"].tradeID = 2;
		supplies["freshGame"].baseConsumption = 0f; //Doesnt Get Consumed via PM's, instead uses consumptionPerLaborer and consumptionPerWarman
		supplies["freshGame"].consumptionPerLaborer = 2f;
		supplies["freshGame"].consumptionPerWarman = 1f;
		supplies["freshGame"].labourAllocatedToProduction = 2f; //labour required to produce 1u/good

        supplies.Add("water", new supplyData());
        supplies["water"].tradeID = 3;
		supplies["water"].baseConsumption = 1f; //Doesnt Get Consumed via PM's, instead uses consumptionPerLaborer and consumptionPerWarman
		supplies["water"].consumptionPerLaborer = 2f;
		supplies["water"].consumptionPerWarman = 2f;
		supplies["water"].labourAllocatedToProduction = 1f; //labour required to produce 1u/good

        supplies.Add("alcohol", new supplyData());
        supplies["alcohol"].tradeID = 4;
		supplies["alcohol"].baseConsumption = 1f * generalSufferingIndex; //Doesnt Get Consumed via PM's, instead uses consumptionPerLaborer and consumptionPerWarman
		supplies["alcohol"].consumptionPerLaborer = 1f;
		supplies["alcohol"].consumptionPerWarman = 1f;
		supplies["alcohol"].labourAllocatedToProduction = 1f; //labour required to produce 1u/good

        supplies.Add("militarySupplies", new supplyData());
        supplies["militarySupplies"].tradeID = 5;
		supplies["militarySupplies"].baseConsumption = 0f; //Doesnt Get Consumed via PM's, instead uses consumptionPerLaborer and consumptionPerWarman
		supplies["militarySupplies"].consumptionPerLaborer = 0f;
		supplies["militarySupplies"].consumptionPerWarman = 1f;
		supplies["militarySupplies"].labourAllocatedToProduction = 2f; //labour required to produce 1u/good

        supplies.Add("gunpowder", new supplyData());
        supplies["gunpowder"].tradeID = 6;
		supplies["gunpowder"].baseConsumption = 0f; //Doesnt Get Consumed via PM's, instead uses consumptionPerLaborer and consumptionPerWarman
		supplies["gunpowder"].consumptionPerLaborer = 0f;
		supplies["gunpowder"].consumptionPerWarman = 1f;
		supplies["gunpowder"].labourAllocatedToProduction = 1f; //labour required to produce 1u/good
		
        supplies.Add("fabric", new supplyData());
        supplies["fabric"].tradeID = 7;
		supplies["fabric"].baseConsumption = 1f; //Fabric gets Consumed via heating needs.
		supplies["fabric"].consumptionPerLaborer = 1f;
		supplies["fabric"].consumptionPerWarman = 1f;
		supplies["fabric"].labourAllocatedToProduction = 2f; //labour required to produce 1u/good
		
        supplies.Add("furs", new supplyData());
        supplies["furs"].tradeID = 8;
		supplies["furs"].baseConsumption = 0f; //Fabric gets Consumed via heating needs.
		supplies["furs"].consumptionPerLaborer = 0f;
		supplies["furs"].consumptionPerWarman = 0f;
		supplies["furs"].labourAllocatedToProduction = 2f; //labour required to produce 1u/good
		
        supplies.Add("metalIngots", new supplyData());
        supplies["metalIngots"].tradeID = 9;
		supplies["metalIngots"].baseConsumption = 3f; //Iron is Needed
		supplies["metalIngots"].consumptionPerLaborer = 0f;
		supplies["metalIngots"].consumptionPerWarman = 0f;
		supplies["metalIngots"].labourAllocatedToProduction = 2f; //labour required to produce 1u/good

        supplies.Add("medicalSupplies", new supplyData());
        supplies["medicalSupplies"].tradeID = 10;
		supplies["medicalSupplies"].baseConsumption = 1f * totalFirstAidUsed; //Base consumption is that
		supplies["medicalSupplies"].consumptionPerLaborer = 0f;
		supplies["medicalSupplies"].consumptionPerWarman = 0f;
		supplies["medicalSupplies"].labourAllocatedToProduction = 1f; //labour required to produce 1u/good

        supplies.Add("wood", new supplyData());
        supplies["wood"].tradeID = 11;
		supplies["wood"].baseConsumption = 5f; //Heating Needs, Militry units
		supplies["wood"].consumptionPerLaborer = 0f;
		supplies["wood"].consumptionPerWarman = 0f;
		supplies["wood"].labourAllocatedToProduction = 1f; //labour required to produce 1u/good

        supplies.Add("tobacco", new supplyData());
        supplies["tobacco"].tradeID = 12;
		supplies["tobacco"].baseConsumption = 0f; //Heating Needs, Militry units
		supplies["tobacco"].consumptionPerLaborer = 1f;
		supplies["tobacco"].consumptionPerWarman = 1f;
		supplies["tobacco"].labourAllocatedToProduction = 1f; //labour required to produce 1u/good

        /*
		foreach(KeyValuePair<string, supplyData> supply in supplies)
		{
			print(supply.Key); //Gets the string
			print(supply.Value); //Gets the supplyData
		}
		*/
    }


    public void GoodProductionSchemes()
	{
        //Rations
        supplies["rations"].amount += (supplies["rations"].gain - supplies["rations"].useLoss) * (Time.deltaTime / daysPassedScript.secondsPerDay); //should be per day.

        //FreshGameFish
        supplies["freshGame"].amount += (supplies["freshGame"].gain - supplies["freshGame"].useLoss) * (Time.deltaTime / daysPassedScript.secondsPerDay);

        //Labour Generation

        if (generalSufferingIndex >= 1f)
        {
            labourProduced = nonCombatants / generalSufferingIndex;
        }
        else 
        {
            labourProduced = nonCombatants; //GSI less than 1 doesnt matter.
        }
		
		

		//Gain of Goods (Production Schemes)

		foreach (KeyValuePair<string, supplyData> supply in supplies)
		{
			supply.Value.gain = (supply.Value.currentLabourAllocated / supply.Value.labourAllocatedToProduction);
		}
		
		//Loss of Goods (Consumption)
		foreach (KeyValuePair<string, supplyData> supply in supplies)
		{
			supply.Value.useLoss = supply.Value.baseConsumption + supply.Value.consumptionPerLaborer + supply.Value.consumptionPerWarman;
		}
		
    }

    public void StorageAmountGoods()
	{
        
		currentStored = 0f;
		
		foreach (KeyValuePair<string, supplyData> supply in supplies)
		{		
			if (supply.Value.usesStorage)
			{
				currentStored += supply.Value.amount;
			}
			
			
		}	
		
    }

    public void GeneralSufferingIndex()
	{
		totalFirstAidUsed = currentGarrisonInjuredSick;
		
	}
	
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		InitializeSupplies();
    }

    // Update is called once per frame
    void Update()
    {
        StorageAmountGoods();
        GoodProductionSchemes();
    }
}
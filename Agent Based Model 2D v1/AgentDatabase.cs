/*
    Project Name: ABDiSE
                  (Agent-Based Disaster Simulation Environment)

    Version:      pre-alpha
    
    File Name:    AgentDatabase.cs 

    SVN $Revision: $

    Abstract:     Database and data structure of agents
                  TODO: support XML file I/O
 
    Authors:      T.L. Hsu 
  
    Contacts:     Lightorz@gmail.com
     
    Major Revision History:
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABDiSE.AgentDatabase
{
    /*  
    * public enum FireAgentDataTypes
    * 
    * Description:
    *      this enum includes data types of fire agent 
    *      
    */
    public enum FireAgentDataTypes
    {
        //Class A	Ordinary combustibles
        ClassA,
        //Class B	Flammable liquids
        ClassB,
        //Class C	Flammable gases
        ClassC,
        //Class D	Combustible metals
        ClassD,
        //Class E	Electrical equipment
        ClassE,
        //Class F	Cooking oil or fat
        ClassF,
        MaximumFireType

    }

    /*  
    * public enum SmokeAgentDataTypes
    * 
    * Description:
    *      this enum includes data types of smoke agent 
    *      from http://www.servpro.com/fire_smoke
    */
    public enum SmokeAgentDataTypes
    {
        //Result from smoldering fires with low heat. 
        //Residues are sticky, smeary and with pungent odors.  
        //Smoke webs can be difficult to clean.
        TypeWetSmokeResidues,

        //Result from fast burning fires at high temperatures. 
        //Residues are often dry, powdery, small, nonsmeary smoke particles.
        TypeDrySmokeResidues,

        //Virtually invisible residues that discolor paints and varnishes.
        //Extreme pungent odor.
        TypeProteinResidues,


        //Furnace puff backs distribute fuel oil soot.
        TypeFuelOilSoot,

        //Tear gas, fingerprint powder, and fire extinguisher residues 
        //also need cleanup.
        TypeOtherTypesOfResidues,

        MaximumTreeType

    }

    /*  
    * public enum BuildingAgentDataTypes
    * 
    * Description:
    *      this enum includes data types of building agent 
    *      
    */
    public enum BuildingAgentDataTypes
    {
        TypeSingleStoryHouse,

        TypeVilla,

        TypeEdifice,

        MaximumBuildingType
    }

    /*  
    * public enum TreeAgentDataTypes
    * 
    * Description:
    *      this enum includes data types of tree agent 
    *      
    */
    public enum TreeAgentDataTypes
    {
        TypePineTree,

        TypeBanyan,

        TypeMaple,

        TypeCupressaceae,

        TypeOtherTypesOfTree,

        MaximumTreeType
    }

    /*  
    * public enum CinderAgentDataTypes
    * 
    * Description:
    *      this enum includes data types of cinder agent 
    *      
    */
    public enum CinderAgentDataTypes
    {
        TypeCinder,

        MaximumCinderType
    }

    /*  
    * public enum FloodAgentDataTypes
    * 
    * Description:
    *      this enum includes data types of flood agent 
    *      
     * http://www.floodsite.net/juniorfloodsite/html/en/student/thingstoknow/hydrology/floodtypes.html
    */
    public enum FloodAgentDataTypes
    {
        FlashFloods, CoastalFloods, UrbanFloods, RiverFloods, Ponding,

        MaximumFloodType
    }

    /*  
    * public enum WaterAgentDataTypes
    * 
    * Description:
    *      this enum includes data types of water agent 
    *      
    */
    public enum WaterAgentDataTypes
    {
        Lake, River, 

        MaximumWaterType
    }

}

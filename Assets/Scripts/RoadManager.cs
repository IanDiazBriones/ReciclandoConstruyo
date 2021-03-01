using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public PlacementManager placementManager;

    public List<Vector3Int> temporaryPlacementPositions = new List<Vector3Int>();
    public List<Vector3Int> roadPositionsToRecheck = new List<Vector3Int>();

    private Vector3Int startPosition;
    private bool placementMode = false;

    public RoadFixer roadFixer;
    public StructureManager structureManager;
    public GameObject Terremoto;
    private int chance = 0;
    private int elementosActuales = 0;
    private bool inTerremoto = false;
    GameObject BDController;




    private string SAVE_SEPARATOR = "/";

    private void Start()
    {
        BDController = GameObject.Find("BDController");
        inTerremoto = false;
        Debug.Log(Application.persistentDataPath + "/save.txt");
        roadFixer = GetComponent<RoadFixer>();

        if (File.Exists(Application.persistentDataPath + "/save.txt"))
        {
            string[] datos = File.ReadAllLines(Application.persistentDataPath + "/save.txt");
            File.WriteAllText(Application.persistentDataPath + "/save.txt", "");
            foreach (var dato in datos)
            {
                string[] datoDivido = dato.Split('/');
                if (datoDivido[1] == "road")
                {
                    PlaceRoad(parseVector3(datoDivido[0]));
                    FinishPlacingRoad();
                }

            }

            foreach (var dato in datos)
            {
                string[] datoDivido = dato.Split('/');
                
                if (datoDivido[1] == "house")
                {
                    structureManager.PlaceHouseCargando(parseVector3(datoDivido[0]), Convert.ToInt32(datoDivido[2]));

                }

                if (datoDivido[1] == "special")
                {
                    structureManager.PlaceSpecialCargando(parseVector3(datoDivido[0]), Convert.ToInt32(datoDivido[2]));

                }

                if (datoDivido[1] == "BigStructure")
                {
                    structureManager.PlaceBigStructureCargando(parseVector3(datoDivido[0]), Convert.ToInt32(datoDivido[2]));

                }


            }
        } else
        {
            File.WriteAllText(Application.persistentDataPath + "/save.txt", "");
        }
        
        
        
        
    }

    private void Update()
    {
        
        //Debug.Log(structureManager.elementos);
        if (structureManager.elementos >= 200)
        {
            Terremoto.GetComponent<ButtonTerremoto>().Terremoto();
        }

        Debug.Log(elementosActuales);
        Debug.Log(structureManager.elementos > elementosActuales);
        if (structureManager.elementos >= 10 && structureManager.elementos > elementosActuales)
        {
            if (!inTerremoto) { 
            chance = UnityEngine.Random.Range(1,2);
            Debug.Log(chance);
            if (chance == 1)
                {
                    inTerremoto = true;
                    Terremoto.GetComponent<ButtonTerremoto>().Terremoto();
                    
                }
                

            }
        }
        elementosActuales = structureManager.elementos;
    }

    private Vector3Int parseVector3(string sourceString)
    {

        string outString;
        Vector3Int outVector3 = Vector3Int.zero;
        string [] splitString;

        // Trim extranious parenthesis

        outString = sourceString.Substring(1, sourceString.Length - 2);

        // Split delimted values into an array

        splitString = outString.Split(","[0]);

        // Build new Vector3 from array elements
        
        outVector3.x = Convert.ToInt32(splitString[0]);
        outVector3.y = Convert.ToInt32(splitString[1]);
        outVector3.z = Convert.ToInt32(splitString[2]);

        return outVector3;

    }

    public void PlaceRoad(Vector3Int position)
    {
        string[] contents = new string[]
                {
                    ""+position,
                    "road"
                };
        BDController.GetComponent<ConexionBD>().InsertMovement(position.x, position.y, 2, 9005, 300201, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

        string saveString = string.Join(SAVE_SEPARATOR, contents);
        File.AppendAllText(Application.persistentDataPath + "/save.txt", saveString + Environment.NewLine);

        if (placementManager.CheckIfPositionInBound(position) == false)
            return;
        if (placementManager.CheckIfPositionIsFree(position) == false)
            return;
        if (placementMode == false)
        {
            temporaryPlacementPositions.Clear();
            roadPositionsToRecheck.Clear();

            placementMode = true;
            startPosition = position;

            temporaryPlacementPositions.Add(position);
            placementManager.PlaceTemporaryStructure(position, roadFixer.deadEnd, CellType.Road);

        }
        else
        {



            placementManager.RemoveAllTemporaryStructures();
            temporaryPlacementPositions.Clear();

            roadPositionsToRecheck.Clear();

            temporaryPlacementPositions = placementManager.GetPathBetween(startPosition, position);

            foreach (var temporaryPosition in temporaryPlacementPositions)
            {
                if (placementManager.CheckIfPositionIsFree(temporaryPosition) == false)
                {
                    roadPositionsToRecheck.Add(temporaryPosition);
                    continue;
                }
                

                

                foreach (var positionsToFix in roadPositionsToRecheck)
                {
                    roadFixer.FixRoadAtPosition(placementManager, positionsToFix);
                }
                placementManager.PlaceTemporaryStructure(temporaryPosition, roadFixer.deadEnd, CellType.Road);
            }
        }

        FixRoadPrefabs();

    }

    private void FixRoadPrefabs()
    {
        foreach (var temporaryPosition in temporaryPlacementPositions)
        {
            roadFixer.FixRoadAtPosition(placementManager, temporaryPosition);
            var neighbours = placementManager.GetNeighboursOfTypeFor(temporaryPosition, CellType.Road);
            foreach (var roadposition in neighbours)
            {
                if (roadPositionsToRecheck.Contains(roadposition)==false)
                {
                    roadPositionsToRecheck.Add(roadposition);
                }
            }
        }
        foreach (var positionToFix in roadPositionsToRecheck)
        {
            roadFixer.FixRoadAtPosition(placementManager, positionToFix);
        }
    }

    public void FinishPlacingRoad()
    {
        //structureManager.elementos += 1;
        placementMode = false;
        placementManager.AddtemporaryStructuresToStructureDictionary();
        if (temporaryPlacementPositions.Count > 0)
        {
            AudioPlayer.instance.PlayPlacementSound();
        }
        temporaryPlacementPositions.Clear();
        startPosition = Vector3Int.zero;
    }
}

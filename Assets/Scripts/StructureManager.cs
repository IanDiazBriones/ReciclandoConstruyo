using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
    public GameObject inventoryManager;

    public StructurePrefabWeighted[] housesPrefabe, specialPrefabs, bigStructuresPrefabs;
    public PlacementManager placementManager;

    private float[] houseWeights, specialWeights, bigStructureWeights;

    public GameObject MensajeFaltaMateriales;
    GameObject BDController;

    public int elementos = 0;

    private void Start()
    {
        BDController = GameObject.Find("BDController");
        houseWeights = housesPrefabe.Select(prefabStats => prefabStats.weight).ToArray();
        specialWeights = specialPrefabs.Select(prefabStats => prefabStats.weight).ToArray();
        bigStructureWeights = bigStructuresPrefabs.Select(prefabStats => prefabStats.weight).ToArray();
        
        //placementManager.PlaceObjectOnTheMap(Vector3Int.zero, housesPrefabe[1].prefab, CellType.Structure);
    }

    public void OnApplicationQuit()
    {
        BDController.GetComponent<ConexionBD>().InsertFinActividad();
    }

    public void OnApplicationPause()
    {
        BDController.GetComponent<ConexionBD>().InsertFinActividad();
    }

    public void PlaceHouse(Vector3Int position)
    {
        
       
        if (CheckPositionBeforePlacement(position))
        {
            if (inventoryManager.GetComponent<InventoryManager>().Carton >= 2 && inventoryManager.GetComponent<InventoryManager>().Vidrio >= 2)
            {
                int randomIndex = GetRandomWeightedIndex(houseWeights);
                string[] contents = new string[]
               {
                    ""+position,
                    "house",
                    ""+randomIndex
               };

                BDController.GetComponent<ConexionBD>().InsertMovement(position.x, position.y, 2, 9005, 300202, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                string saveString = string.Join("/", contents);
                File.AppendAllText(Application.persistentDataPath + "/save.txt", saveString + Environment.NewLine);
                elementos += 1;

                placementManager.PlaceObjectOnTheMap(position, housesPrefabe[randomIndex].prefab, CellType.Structure);
                AudioPlayer.instance.PlayPlacementSound();
                inventoryManager.GetComponent<InventoryManager>().Carton -= 2;
                inventoryManager.GetComponent<InventoryManager>().Vidrio -= 2;
                PlayerPrefs.SetInt("Carton", inventoryManager.GetComponent<InventoryManager>().Carton);
                PlayerPrefs.SetInt("Vidrio", inventoryManager.GetComponent<InventoryManager>().Vidrio);

            }
            else
            {
                Debug.Log("Faltan Materiales");
                MensajeFaltaMateriales.SetActive(true);
            }
        }
    }

    public void PlaceHouseCargando(Vector3Int position, int randomIndex)
    {
        
        string[] contents = new string[]
                {
                    ""+position,
                    "house",
                    ""+randomIndex
                };

        string saveString = string.Join("/", contents);
        File.AppendAllText(Application.persistentDataPath + "/save.txt", saveString + Environment.NewLine);
        elementos += 1;
        if (CheckPositionBeforePlacement(position))
        {
            


                placementManager.PlaceObjectOnTheMap(position, housesPrefabe[randomIndex].prefab, CellType.Structure);
                AudioPlayer.instance.PlayPlacementSound();


            
            
        }
    }



    internal void PlaceBigStructure(Vector3Int position)
    {
        int width = 2;
        int height = 2;
        int randomIndex = GetRandomWeightedIndex(bigStructureWeights);
        

        if (CheckBigStructure(position, width , height))
        {
            if (inventoryManager.GetComponent<InventoryManager>().Papel >= 4 && inventoryManager.GetComponent<InventoryManager>().Carton >= 4)
            {
                string[] contents = new string[]
                {
                    ""+position,
                    "BigStructure",
                    ""+randomIndex
                };

                BDController.GetComponent<ConexionBD>().InsertMovement(position.x, position.y, 2, 9005, 300204, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));


                string saveString = string.Join("/", contents);
            File.AppendAllText(Application.persistentDataPath + "/save.txt", saveString + Environment.NewLine);
                elementos += 1;
                placementManager.PlaceObjectOnTheMap(position, bigStructuresPrefabs[randomIndex].prefab, CellType.Structure, width, height);
            AudioPlayer.instance.PlayPlacementSound();
            inventoryManager.GetComponent<InventoryManager>().Papel -= 2;
            inventoryManager.GetComponent<InventoryManager>().Carton -= 2;
            inventoryManager.GetComponent<InventoryManager>().Metal -= 2;
            PlayerPrefs.SetInt("Carton", inventoryManager.GetComponent<InventoryManager>().Carton);
            PlayerPrefs.SetInt("Papel", inventoryManager.GetComponent<InventoryManager>().Papel);
            PlayerPrefs.SetInt("Metal", inventoryManager.GetComponent<InventoryManager>().Papel);
            }
            else
            {
                Debug.Log("Faltan Materiales");
                MensajeFaltaMateriales.SetActive(true);
            }
        }
    }

    internal void PlaceBigStructureCargando(Vector3Int position, int randomIndex)
    {
        int width = 2;
        int height = 2;
        

        if (CheckBigStructure(position, width, height))
        {
            
                string[] contents = new string[]
                {
                    ""+position,
                    "BigStructure",
                    ""+randomIndex
                };

            string saveString = string.Join("/", contents);
            File.AppendAllText(Application.persistentDataPath + "/save.txt", saveString + Environment.NewLine);
            elementos += 1;
            placementManager.PlaceObjectOnTheMap(position, bigStructuresPrefabs[randomIndex].prefab, CellType.Structure, width, height);
        }
    }

    private bool CheckBigStructure(Vector3Int position, int width, int height)
    {
        bool nearRoad = false;
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                var newPosition = position + new Vector3Int(x, 0, z);
                
                if (DefaultCheck(newPosition)==false)
                {
                    return false;
                }
                if (nearRoad == false)
                {
                    nearRoad = RoadCheck(newPosition);
                }
            }
        }
        return nearRoad;
    }

    public void PlaceSpecial(Vector3Int position)
    {
        
        if (CheckPositionBeforePlacement(position))
        {
            if (inventoryManager.GetComponent<InventoryManager>().Papel >= 2 && inventoryManager.GetComponent<InventoryManager>().Vidrio >= 2)
            {
                int randomIndex = GetRandomWeightedIndex(specialWeights);

            string[] contents = new string[]
                {
                    ""+position,
                    "special",
                    ""+randomIndex
                };
                BDController.GetComponent<ConexionBD>().InsertMovement(position.x, position.y, 2, 300203, 300204, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                string saveString = string.Join("/", contents);
            File.AppendAllText(Application.persistentDataPath + "/save.txt", saveString + Environment.NewLine);
                elementos += 1;
                placementManager.PlaceObjectOnTheMap(position, specialPrefabs[randomIndex].prefab, CellType.SpecialStructure);
            AudioPlayer.instance.PlayPlacementSound();
            inventoryManager.GetComponent<InventoryManager>().Papel -= 2;
            inventoryManager.GetComponent<InventoryManager>().Vidrio -= 2;
            PlayerPrefs.SetInt("Vidrio", inventoryManager.GetComponent<InventoryManager>().Vidrio);
            PlayerPrefs.SetInt("Papel", inventoryManager.GetComponent<InventoryManager>().Papel);
            }
            else
            {
                Debug.Log("Faltan Materiales");
                MensajeFaltaMateriales.SetActive(true);
            }
        }
    }

    public void PlaceSpecialCargando(Vector3Int position, int randomIndex)
    {
        
        if (CheckPositionBeforePlacement(position))
        {
            string[] contents = new string[]
                {
                    ""+position,
                    "special",
                    ""+randomIndex
                };

            string saveString = string.Join("/", contents);
            File.AppendAllText(Application.persistentDataPath + "/save.txt", saveString + Environment.NewLine);
            elementos += 1;
            placementManager.PlaceObjectOnTheMap(position, specialPrefabs[randomIndex].prefab, CellType.SpecialStructure);
            
        }
    }

    private int GetRandomWeightedIndex(float[] weights)
    {
        float sum = 0f;
        for (int i = 0; i < weights.Length; i++)
        {
            sum += weights[i];
        }

        float randomValue = UnityEngine.Random.Range(0, sum);
        float tempSum = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            //0->weihg[0] weight[0]->weight[1]
            if(randomValue >= tempSum && randomValue < tempSum + weights[i])
            {
                return i;
            }
            tempSum += weights[i];
        }
        return 0;
    }

    private bool CheckPositionBeforePlacement(Vector3Int position)
    {
        if (DefaultCheck(position) == false)
        {
            return false;
        }

        if (RoadCheck(position) == false)
            return false;
        
        return true;
    }

    private bool RoadCheck(Vector3Int position)
    {
        if (placementManager.GetNeighboursOfTypeFor(position, CellType.Road).Count <= 0)
        {
            Debug.Log("Must be placed near a road");
            return false;
        }
        return true;
    }

    private bool DefaultCheck(Vector3Int position)
    {
        if (placementManager.CheckIfPositionInBound(position) == false)
        {
            Debug.Log("This position is out of bounds");
            return false;
        }
        if (placementManager.CheckIfPositionIsFree(position) == false)
        {
            //Debug.Log("This position is not EMPTY");
            return false;
        }
        return true;
    }
}

[Serializable]
public struct StructurePrefabWeighted
{
    public GameObject prefab;
    [Range(0,1)]
    public float weight;
}

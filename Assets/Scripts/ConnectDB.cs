using MySql.Data.MySqlClient;
using System;
using UnityEngine;
 
public class ConnectDB : MonoBehaviour
{
    MySqlConnection conn = null;
    MySqlCommand cmd = null;
 
    // Connect to db and prepare command
    void Start()
    {
        MySqlConnection conn = new MySqlConnection("Database=ulearnet_reim_pilotaje;Server=127.0.0.1;Uid=admin;Password=1234;pooling=false;CharSet=utf8;port=3306");
        if (conn != null)
        {
            conn.Open();
 
            cmd = conn.CreateCommand();
            if (cmd != null)
            {
                cmd.CommandTimeout = 1000;
            }
            else
            {
                Debug.Log("Cannot create SQL command.");
            }
        }
        else
        {
            Debug.Log("Cannot open database.");
        }
    }
 
    // In each frame run SQL command(s)
    void Update()
    {
    	
        if (cmd != null)
        {
            try
            {
                cmd.CommandText = "INSERT INTO elemento (nombre, corresponde_elemento) VALUES ('Prueba', 1);";
 				Debug.Log("Inside Try");
 				cmd.ExecuteNonQuery(); 
                // Run SQL command, do stuff with result...
 
            }
            catch (MySqlException eMySQL)
            {
                Debug.Log(String.Format("MySQL exception with message: {0}", eMySQL.Message));
            }
            catch (Exception ex)
            {
                Debug.Log(String.Format("General exception with message: {0}", ex.Message));
            }
            finally
            {
                // Placeholder
                Debug.Log("Inside finally");
            }
        }
    }
 
  // Disconnect from db and do housekeeping
    void OnApplicationQuit()
    {
        if (cmd != null)
        {
            cmd.Dispose();
        }
 
        if (conn != null)
        {
            conn.Close();
            conn.Dispose();
        }
    }
}
 
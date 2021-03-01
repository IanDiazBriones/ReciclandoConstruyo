using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Linq;
using TMPro;
public class ConexionBD : MonoBehaviour
{
    /// PERIODO OBTENIDO EN BRUTO:
    public static string id_periodo = "202002";
    /// ID REIM EN BRUTO:
    public static string id_reim = "201";
    public static int id_tiempoxActividad_Actual;
    /// 
    MySqlCommand codigo;
    MySqlConnection conn;

    public InputField userInput;
    public InputField passwordInput;
    public TMP_Text usuariopassfail;
    bool activador;
    bool returned = false;

    public static int idUsuario;
    public static string usuario_nombre;
    public static string idSesion;
    private string usuario;
    private string password;
    private int id_session;
    public string sceneToChange;
    private static string idButtonPressed = null;
    private static string database = "ulearnet_reim_pilotaje";
    public Animator transition;
    public static bool inActivity = false;
    public static string nombre;
    
    public static List<Pregunta> Preguntas = new List<Pregunta>();



    public static List<string> Querys = new List<string>();
    bool isDone = true;
    public List<Task> tasks = new List<Task>();


    private void Start()
    {
        if(Preguntas == null || Preguntas.Count == 0) {
            GetPreguntas();
        }
    }

    public List<Pregunta> ReturnPreguntas()
    {
        return Preguntas;
    }
    // Update is called once per frame
    async void Update()
    {

        if (Querys.Count > 0 && isDone)
        {
            await EnviadorQuerys(Querys);
        }


    }


    async Task<int> EnviadorQuerys(List<string> Querys)
    {

        isDone = false;
        await Task.Run(() =>
        {
                using (MySqlConnection myConn = ConnectDB())
                using (MySqlCommand Mycodigo = myConn.CreateCommand())
                {

                    try
                    {
                        myConn.Open();
                        foreach (var query in Querys.ToList())
                        {
                            try
                            {
                                Mycodigo.CommandText = (query);
                                Mycodigo.ExecuteNonQuery();
                                Querys.Remove(query);
                            }
                            catch (MySqlException ex)
                            {
                                if (ex.ErrorCode != -2147467259)
                                {
                                    Debug.LogError(ex.ErrorCode);
                                    Querys.Remove(query);
                                }


                            }

                        }
                        
                    }
                    catch (MySqlException ex)
                    {
                        if (ex.ErrorCode != -2147467259)
                        {
                            Debug.LogError(ex.ErrorCode);
                        }
                        

                    }

                 
                }
        }
        );

        isDone = true;
        return 1;
    }


    public void InsertTouch(int id_elemento)
    {
        Querys.Add("INSERT INTO " + database + ".alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
                                  " VALUES (" + id_periodo + ", " + idUsuario + ", " + id_reim + ", " + "9005" + "," + id_elemento + ", " + System.Convert.ToInt32(Input.mousePosition.x) + ", " + System.Convert.ToInt32(Input.mousePosition.y) + ", " + "1" + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");
    }

    public void ElementoReciclado(int id_elemento, int actividad, int correcta, int zona, int tipo)
    {
        string Query =("INSERT INTO " + database + ".alumno_respuesta_actividad (id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)" +
                                  " VALUES (" + id_periodo + ", " + idUsuario + ", " + id_reim + ", " + actividad + "," + id_elemento + ", "+ zona + ", "+ tipo + ", " + correcta + ", " + "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ")");

        Querys.Add(Query);
        
    }

    public void SendSesionOpen(int idUsuario){

    	idSesion = idUsuario+"-"+System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        Querys.Add("INSERT INTO " +database+".asigna_reim_alumno (sesion_id, usuario_id, periodo_id, reim_id, datetime_inicio, datetime_termino)"+" VALUES ('"+idSesion+"', "+idUsuario+",   "+id_periodo+", "+id_reim+", '"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss")+"', '"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss")+"' "+")");
    	
        
    }


    public void CloseBD() {

    	if (codigo != null)
        {
            codigo.Dispose();
        }
 
        if (conn != null)
        {
            conn.Close();
            conn.Dispose();
        }
    }

    public void OnApplicationQuit()
    {
        string SQLQuery = "UPDATE " + database + ".asigna_reim_alumno SET  datetime_termino = '" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE sesion_id = '" + idSesion + "'";

        using (MySqlConnection myConn = ConnectDB())
        using (MySqlCommand Mycodigo = myConn.CreateCommand())
        {
            try
            {
                myConn.Open();
                Mycodigo.CommandText = (SQLQuery);
                Mycodigo.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError(ex.ErrorCode);
            }


        }



        if (inActivity)
        {

            InsertFinActividad();
        }

        conn = ConnectDB();
        conn.ClearAllPoolsAsync();

    	
    }

    public void OnApplicationPause()
    {
        string SQLQuery = "UPDATE " + database + ".asigna_reim_alumno SET  datetime_termino = '" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE sesion_id = '" + idSesion + "'";

        using (MySqlConnection myConn = ConnectDB())
        using (MySqlCommand Mycodigo = myConn.CreateCommand())
        {
            try
            {
                myConn.Open();
                Mycodigo.CommandText = (SQLQuery);
                Mycodigo.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError(ex.ErrorCode);
            }


        }



        if (inActivity)
        {

            InsertFinActividad();
        }

        conn = ConnectDB();
        conn.ClearAllPoolsAsync();
    }


    public MySqlConnection ConnectDB()
    {
    	string datosConexion = "host=ulearnet.org; Port =3306; UserName=reim_ulearnet; Password=KsclS$AcSx.20Cv83xT; Database="+database+ "; Pooling=True;";
        MySqlConnection conectar = new MySqlConnection(datosConexion);


       
        return conectar;
    }

    public string getNombre()
    {
        return nombre;
    }
    
    
    public void inicioSesion()
    {
        usuario = userInput.GetComponent<InputField>().text;
        password = passwordInput.GetComponent<InputField>().text;
        using (MySqlConnection myConn = ConnectDB())
        using (MySqlCommand Mycodigo = myConn.CreateCommand())
        {
            try
            {
                myConn.Open();
                Mycodigo.CommandText = ("SELECT id, nombres, apellido_paterno FROM " + database + ".usuario WHERE username = '" + usuario + "' AND password = '" + password + "'");
                Mycodigo.ExecuteNonQuery();

                using (MySqlDataReader leer = Mycodigo.ExecuteReader())
                {
                    returned = false;
                    if (leer.HasRows)
                    {
                        while (leer.Read())
                        {
                            idUsuario = leer.GetInt32(0);
                            nombre = leer.GetString(1) + " " + leer.GetString(2);
                            returned = true;
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError(ex.ErrorCode);
            }


        }
	    if (returned)
	    {
	        Debug.Log("Bienvenido a Ulearnet "+usuario);
            SendSesionOpen(idUsuario);
            StartCoroutine(LoadLevel(sceneToChange));

        }
	    else
	    {
	            
	        usuariopassfail.gameObject.SetActive(true);
	        Invoke("activarmensaje", 3f);
	    }
    }

    

    public void activarmensaje()
    {
        usuariopassfail.gameObject.SetActive(false);
    }

    public static void SetidButtonPressed(string value) 
    {
    	idButtonPressed = value;
    }

    public static int GetIdUsuario()
    {
        return idUsuario;
    }

    public static string GetIdReim()
    {
        return id_reim;
    }

    public void InsertInicioActividad(int actividad_id)
    {
        inActivity = true;
        InsertInicioActividadAync("INSERT INTO " +database+".tiempoxactividad (inicio, final, causa, usuario_id, reim_id, actividad_id)"+" VALUES ('"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss")+"', '"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss")+"', 0,   "+idUsuario+", "+id_reim+", "+actividad_id+")");

    }



    public  void InsertInicioActividadAync(string query)
    {


            using (MySqlConnection myConn = ConnectDB())
            using (MySqlCommand Mycodigo = myConn.CreateCommand())
            {

                try
                {
                    myConn.Open();
                    Mycodigo.CommandText = (query);
                    Mycodigo.ExecuteNonQuery();
                    query = "SELECT LAST_INSERT_ID();";
                    Mycodigo.CommandText = (query);
                    using (MySqlDataReader lectura = Mycodigo.ExecuteReader())
                    {
                        if (lectura.HasRows)
                        {
                            while (lectura.Read())
                            {
                                lectura.Read();
                                id_tiempoxActividad_Actual = lectura.GetInt32(0);


                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Debug.LogError(ex.ErrorCode);
                }


            }

        
    }

    public void InsertFinActividad()
    {
        
        string SQLQuery = "UPDATE  "+database+".tiempoxactividad SET final = '"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss")+"' WHERE id = "+id_tiempoxActividad_Actual+"";

        using (MySqlConnection myConn = ConnectDB())
        using (MySqlCommand Mycodigo = myConn.CreateCommand())
        {

            try
            {
                myConn.Open();
                Mycodigo.CommandText = (SQLQuery);
                Mycodigo.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError(ex.ErrorCode);
            }


        };

        conn = ConnectDB();
        conn.ClearAllPoolsAsync();
    }

    public void InsertFinActividadAsync()
    {

        string SQLQuery = "UPDATE  " + database + ".tiempoxactividad SET final = '" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE id = " + id_tiempoxActividad_Actual + "";

        using (MySqlConnection myConn = ConnectDB())
        using (MySqlCommand Mycodigo = myConn.CreateCommand())
        {

            try
            {
                myConn.Open();
                Mycodigo.CommandText = (SQLQuery);
                Mycodigo.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError(ex.ErrorCode);
            }


        };

        inActivity = false;

        conn = ConnectDB();
        conn.ClearAllPoolsAsync();

    }

    public void GetPreguntas()
    {

        using (MySqlConnection myConn = ConnectDB())
        using (MySqlCommand Mycodigo = myConn.CreateCommand())
        {
            try
            {
                myConn.Open();
                Mycodigo.CommandText = ("SELECT Pregunta, objetivo_aprendizaje_id, txt_alte FROM " + database + ".item inner join item_alt on item.IdItem = item_alt.ITEM_IdItem inner join alternativa on item_alt.idlaternativa = alternativa.idlaternativa  where reim_id = 201");
                using (MySqlDataReader leer = Mycodigo.ExecuteReader())
                {
                    int tipo = 0;
                    if (leer.HasRows)
                    {
                        while (leer.Read())
                        {
                            if (leer.GetString(1) == "32")
                            {
                                tipo = 1;
                            }
                            else if (leer.GetString(1) == "31")
                            {
                                tipo = 2;
                            };
                            Pregunta pregunta = new Pregunta();
                            pregunta.pregunta = leer.GetString(0);
                            pregunta.tipo = tipo;
                            pregunta.respuesta = leer.GetInt32(2);

                            Preguntas.Add(pregunta);




                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError(ex.ErrorCode);
            }
        }
    }

    public void InsertMovement(int MovementX, int MovementY, int Correcta, int IDActividad, int IDElemento, string time){

        Querys.Add("INSERT INTO "+database+".alumno_respuesta_actividad(id_per, id_user, id_reim, id_actividad, id_elemento, fila, columna, correcta, datetime_touch)"+" VALUES("+id_periodo+", "+idUsuario+", "+id_reim+", "+IDActividad+", "+IDElemento+", "+MovementX*1000+", "+MovementY*1000+", "+Correcta+", "+"'"+System.DateTime.Now .ToString("yyyy-MM-dd HH:mm:ss.fff")+"')");

        
    }

    IEnumerator LoadLevel (string leveltoload)
    {
    	transition.SetTrigger("Start");
    	yield return new WaitForSeconds(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(leveltoload, LoadSceneMode.Single);

    }
}
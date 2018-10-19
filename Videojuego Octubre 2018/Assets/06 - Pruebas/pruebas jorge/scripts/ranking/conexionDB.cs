using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine;

public class conexionDB : MonoBehaviour
{

    public string rutaDB;
    public string strConexion;
    public string DBFileName;

    IDbConnection dbconection;
    IDbCommand dbcommand;
    IDataReader reader;

	// Use this for initialization
	void Start ()
    {
		
	}

    void AbrirDB()
    {
        //crear y abrir la conexion
        rutaDB = Application.dataPath + "/StreamingAssets/" + DBFileName;
        strConexion = "URI=file:" + rutaDB;
        dbconection = new SqliteConnection(strConexion);
        dbconection.Open();

        //crear consulta
        dbcommand = dbconection.CreateCommand();
        string sqlQuery = "selct * from ";
    }

}

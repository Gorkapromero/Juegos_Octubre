using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using System;

public class RankingManager : MonoBehaviour
{
    public string rutaDB;
    public string Conexion;
    public string nombreDB;

    IDbConnection conexionDB;
    IDbCommand comandosDB;
    IDataReader leerDatos;

    private List<Ranking> rankings;

    // Use this for initialization
    void Start ()
    {
		
	}
	
    void AbrirDB()
    {
        rutaDB = Application.dataPath + "/StreamingAssets/" + nombreDB;
        Conexion = "URI=file:" + rutaDB;
        conexionDB = new SqliteConnection(Conexion);
        conexionDB.Open();
    }
	void ObtenerRanking()
    {
        rankings.Clear();
        AbrirDB();
        comandosDB = conexionDB.CreateCommand();
        string sqlQuery = "selct * from Ranking";
        comandosDB.CommandText = sqlQuery;

        leerDatos = comandosDB.ExecuteReader();
        while (leerDatos.Read())
        {
            rankings.Add(new Ranking(leerDatos.GetInt32(0), 
                                     leerDatos.GetString(1), 
                                     leerDatos.GetInt32(2), 
                                     leerDatos.GetInt32(3)));
        }
        leerDatos.Close();
        leerDatos = null;
        CerrarDB();
    }

    void InsertarPuntos(string n, string s)
    {
        AbrirDB();
        comandosDB = conexionDB.CreateCommand();
        string sqlQuery = String.Format("insert into Ranking(Nombre,Score) values(\"{0}\",\"{1}\")",n,s);
        comandosDB.CommandText = sqlQuery;

        comandosDB.ExecuteScalar();
        CerrarDB();
    }

    void BorrarPuntos(int id)
    {
        AbrirDB();
        comandosDB = conexionDB.CreateCommand();
        string sqlQuery = "delete from Ranking where PlayerId =" + id + "\"";
        comandosDB.CommandText = sqlQuery;

        comandosDB.ExecuteScalar();
        CerrarDB();
    }

    void CerrarDB()
    {
        comandosDB.Dispose();
        comandosDB = null;
        conexionDB.Close();
        conexionDB = null;
    }
}

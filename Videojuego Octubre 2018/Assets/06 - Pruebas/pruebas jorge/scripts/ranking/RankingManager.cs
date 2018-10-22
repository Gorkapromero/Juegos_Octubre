using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using System;

public class RankingManager : MonoBehaviour
{
    string rutaDB;
    string Conexion;
    public string nombreDB;

    public GameObject puntosPREF;
    public Transform PuntosPadre;
    public int topRank;

    IDbConnection conexionDB;
    IDbCommand comandosDB;
    IDataReader leerDatos;

    private List<Ranking> rankings;

    // Use this for initialization
    void Start ()
    {
        MostrarRanking();
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
        rankings.Sort();
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

    void MostrarRanking()
    {
        ObtenerRanking();
        for (int i = 0; i < topRank; i++)
        {
            if (i < rankings.Count)
            {
                GameObject tempPref = Instantiate(puntosPREF);
                tempPref.transform.SetParent(PuntosPadre);
                Ranking rankTemp = rankings[i];
                tempPref.GetComponent<RankingScript>().PonerPuntos("3" + (i + 1).ToString(),
                                                                    rankTemp.Nombre,
                                                                    rankTemp.Score.ToString());
            }
           
        }
    }

    void CerrarDB()
    {
        comandosDB.Dispose();
        comandosDB = null;
        conexionDB.Close();
        conexionDB = null;
    }
}

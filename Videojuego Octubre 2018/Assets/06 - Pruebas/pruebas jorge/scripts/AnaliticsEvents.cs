using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnaliticsEvents : MonoBehaviour
{
    public void AnaliticsProgresion(string Noleada,string tiempo)
    {
        Analytics.CustomEvent("Oleadas", new Dictionary<string, object>
        {
            {"oleada Nº", Noleada },
            {"Tiempo Partida", tiempo }
        });
    }

    public void AnalyticsComprarSkin(string nombreSkin,int precio)
    {
        Analytics.CustomEvent("Comprar Skin", new Dictionary<string, object>
        {
            {"Nombre Skin", nombreSkin },
            {"Precio", precio }
        });
    }

    public void AnalyticsRevivir(string modo,bool Estado)
    {
        Analytics.CustomEvent("Revivir", new Dictionary<string, object>
        {
            {"Modo", modo },
            {"Revivido", Estado }
        });
    }

    public void AnalyticsPremios(string Premio, bool Estado)
    {
        Analytics.CustomEvent("Premios", new Dictionary<string, object>
        {
            {"Premio", Premio },
            {"Revivido", Estado }
        });
    }

    public void AnalyticsDailyGift(bool Estado)
    {
        Analytics.CustomEvent("Daily Gift", new Dictionary<string, object>
        {
            {"Premio diario", Estado }
        });
    }

    public void AnalyticsHabilidades(string habilidad, bool Estado)
    {
        Analytics.CustomEvent("Habilidades", new Dictionary<string, object>
        {
            {"Habilidad", habilidad },
        });
    }
}

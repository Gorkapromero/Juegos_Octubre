using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnaliticsEvents : MonoBehaviour
{
    public void AnaliticsProgresion(string Noleada,string tiempo)
    {
        AnalyticsEvent.Custom("Oleadas", new Dictionary<string, object>
        {
            {"numero oleada", Noleada },
            {"Tiempo Partida", tiempo }
        });
    }

    public void AnalyticsComprarSkin(string nombreSkin,int precio)
    {
        AnalyticsEvent.Custom("Comprar Skin", new Dictionary<string, object>
        {
            {"Nombre Skin", nombreSkin },
            {"Precio", precio }
        });
    }

    public void AnalyticsRevivir(string modo,bool Estado)
    {
        AnalyticsEvent.Custom("Revivir", new Dictionary<string, object>
        {
            {"Modo", modo },
            {"Revivido", Estado }
        });
    }

    public void AnalyticsPremios(string Premio, bool Estado)
    {
        AnalyticsEvent.Custom("Premios", new Dictionary<string, object>
        {
            {"Premio", Premio },
            {"Recivido", Estado }
        });
    }

    public void AnalyticsDailyGift(bool Estado)
    {
        AnalyticsEvent.Custom("Daily Gift", new Dictionary<string, object>
        {
            {"Premio diario", Estado }
        });
    }

    public void AnalyticsHabilidades(string habilidad, bool Estado)
    {
        AnalyticsEvent.Custom("Habilidades", new Dictionary<string, object>
        {
            {"Habilidad", habilidad },
        });
    }
}

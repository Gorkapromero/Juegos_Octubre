using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

class TopScore : IComparable<TopScore>
{
    public int Id { get; set; }
    public int Score { get; set; }
    //public int Tiempo { get; set; }

    public TopScore(int id, int score/*, int tiempo*/)
    {
        this.Id = id;
        this.Score = score;
        //this.Tiempo = tiempo;
    }

    public int CompareTo(TopScore other)
    {
        // return this.Score.CompareTo(other.Score);
        return other.Score.CompareTo(this.Score);
    }
}
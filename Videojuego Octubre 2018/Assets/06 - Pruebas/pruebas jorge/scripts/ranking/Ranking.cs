using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Ranking : IComparable<Ranking>
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int Score { get; set; }
    //public int Tiempo { get; set; }

    public Ranking(int id, string name, int score/*, int tiempo*/)
    {
        this.Id = id;
        this.Nombre = name;
        this.Score = score;
        //this.Tiempo = tiempo;
    }

    public int CompareTo(Ranking other)
    {
        // return this.Score.CompareTo(other.Score);
        return other.Score.CompareTo(this.Score);
    }
}

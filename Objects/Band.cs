using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BandTracker
{
    public class Band
    {
        private int _id;
        private string _name;

        public Band(string Name, int Id = 0)
        {
            _id = Id;
            _name = Name;
        }

        public override bool Equals(System.Object otherBand)
        {
            if (!(otherBand is Band))
            {
                return false;
            }
            else
            {
                Band newBand = (Band) otherBand;
                bool idEquality = this.GetId() == newBand.GetId();
                bool nameEquality = this.GetName() == newBand.GetName();
                return (idEquality && nameEquality);
            }
        }
        public int GetId()
        {
            return _id;
        }
        public string GetName()
        {
            return _name;
        }

        public static List<Band> GetBand()
        {
            List<Band> allBands = new List<Band>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM bands;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int BandId = rdr.GetInt32(0);
                string BandName = rdr.GetString(1);
                Band newBand = new Band(BandName, BandId);
                allBands.Add(newBand);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return allBands;
        }

        public void Save()
        {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("INSERT INTO bands (name) OUTPUT INSERTED.id VALUES (@BandName);", conn);

        cmd.Parameters.Add(new SqlParameter("@BandName", this.GetName()));
        
        SqlDataReader rdr = cmd.ExecuteReader();

        while(rdr.Read())
        {
          this._id = rdr.GetInt32(0);
        }
        if (rdr != null)
        {
          rdr.Close();
        }
        if(conn != null)
        {
          conn.Close();
        }
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM bands;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}

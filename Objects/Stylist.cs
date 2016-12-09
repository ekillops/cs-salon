using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Salon
{
  public class Stylist
  {
    private string _name;
    private string _phoneNumber;
    private int _id;

    public Stylist(string name, string phoneNumber = "000-000-0000", int id = 0)
    {
      _name = name;
      _phoneNumber = phoneNumber;
      _id = id;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = (this.GetId() == newStylist.GetId());
        bool nameEquality = (this.GetName() == newStylist.GetName());
        bool phoneNumberEquality = (this.GetPhoneNumber() == newStylist.GetPhoneNumber());
        return (idEquality && nameEquality && phoneNumberEquality);
      }
    }

    public override int GetHashCode()
		{
			return _name.GetHashCode();
		}

    //CREATE
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("INSERT INTO stylists (name, phone_number) OUTPUT INSERTED.id VALUES (@name, @phone_number);", conn);

      cmd.Parameters.AddWithValue("@name", this.GetName());
      cmd.Parameters.AddWithValue("@phone_number", this.GetPhoneNumber());

      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    //READ
    public static Stylist Find(int id)
		{
			SqlConnection conn = DB.Connection();
			conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT name, phone_number FROM stylists WHERE id = @id;", conn);
      cmd.Parameters.AddWithValue("@id", id);

      string name = null;
      string phoneNumber = null;

      SqlDataReader rdr = cmd.ExecuteReader();

			while (rdr.Read())
			{
				name = rdr.GetString(0);
        phoneNumber = rdr.GetString(1);
			}
			if (rdr != null)
			{
				rdr.Close();
			}
			if (conn != null)
			{
				conn.Close();
			}

			return new Stylist(name, phoneNumber, id);
		}

    //UPDATE
    public static void Update(int targetId, string newName, string newPhoneNumber)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE stylists SET name = @new_name, phone_number = @new_phone_number WHERE id = @id", conn);
      cmd.Parameters.AddWithValue("@new_name", newName);
      cmd.Parameters.AddWithValue("@new_phone_number", newPhoneNumber);
      cmd.Parameters.AddWithValue("@id", targetId);

      cmd.ExecuteNonQuery();
      conn.Close();
    }

    //DESTROY
    public static void Delete(int id)
		{
			SqlConnection conn = DB.Connection();
			conn.Open();
			SqlCommand cmd = new SqlCommand("DELETE FROM stylists WHERE id = @id", conn);
			cmd.Parameters.AddWithValue("@id", id);

			cmd.ExecuteNonQuery();
			conn.Close();
		}

    public static List<Stylist> GetAll()
		{
			List<Stylist> allStylists = new List<Stylist>{};
			SqlConnection conn = DB.Connection();
			conn.Open();

			SqlCommand cmd = new SqlCommand("SELECT * FROM stylists;", conn);
			SqlDataReader rdr = cmd.ExecuteReader();

			while (rdr.Read())
			{
				int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string phoneNumber = rdr.GetString(2);

				Stylist newStylist = new Stylist(name, phoneNumber, id);
				allStylists.Add(newStylist);
			}

			if (rdr != null)
			{
				rdr.Close();
			}
			if (conn != null)
			{
				conn.Close();
			}

			return allStylists;
		}

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM stylists", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public List<Client> GetClients()
		{
			List<Client> returnList = new List<Client> {};

			SqlConnection conn = DB.Connection();
			conn.Open();

			SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE stylist_id = @id", conn);
			cmd.Parameters.AddWithValue("@id", _id);

			SqlDataReader rdr = cmd.ExecuteReader();

			while (rdr.Read())
			{
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        string clientPhoneNumber = rdr.GetString(2);

				Client newClient = new Client(clientName, clientPhoneNumber, _id, clientId);
				returnList.Add(newClient);
			}
			if (rdr != null)
			{
				rdr.Close();
			}
			if (conn != null)
			{
				conn.Close();
			}
			return returnList;
    }

    public static List<Stylist> SearchByValue(string columnToCheck, string searchInput)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand();
      cmd.Connection = conn;
      string input = "%"+searchInput+"%";

      switch (columnToCheck)
      {
        case "name":
            cmd.CommandText = "SELECT * FROM stylists WHERE name LIKE @input";
            break;
        case "phone_number":
            cmd.CommandText = "SELECT * FROM stylists WHERE phone_number LIKE @input";
            break;
        default:
            break;
      }
      cmd.Parameters.AddWithValue("@input", input);

      List<Stylist> searchResults = new List<Stylist> {};

      SqlDataReader rdr = cmd.ExecuteReader();
      int foundId = 0;
      string foundName = null;
      string foundPhoneNumber = null;

      while (rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        foundName = rdr.GetString(1);
        foundPhoneNumber = rdr.GetString(2);
        Stylist foundStylist = new Stylist(foundName, foundPhoneNumber, foundId);
        searchResults.Add(foundStylist);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return searchResults;
    }

    //GETTERS AND SETTERS
    public void SetName(string newName)
    {
      _name = newName;
    }
    public string GetName()
    {
      return _name;
    }

    public void SetPhoneNumber(string newPhoneNumber)
    {
      _phoneNumber = newPhoneNumber;
    }
    public string GetPhoneNumber()
    {
      return _phoneNumber;
    }

    public void SetId(int newId)
    {
      _id = newId;
    }
    public int GetId()
    {
      return _id;
    }
  }
}

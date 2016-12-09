using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Salon
{
  public class Client
  {
    private string _name;
    private string _phoneNumber;
    private int _stylistId;
    private int _id;

    public Client(string name, string phoneNumber = "000-000-0000", int stylistId = 0, int id = 0)
    {
      _name = name;
      _phoneNumber = phoneNumber;
      _stylistId = stylistId;
      _id = id;
    }

    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = (this.GetId() == newClient.GetId());
        bool nameEquality = (this.GetName() == newClient.GetName());
        bool phoneNumberEquality = (this.GetPhoneNumber() == newClient.GetPhoneNumber());
        bool stylistIdEquality = (this.GetStylistId() == newClient.GetStylistId());

        return (idEquality && nameEquality && phoneNumberEquality && stylistIdEquality);
      }
    }

    public override int GetHashCode()
		{
			return _name.GetHashCode();
		}

    public static Client Find(int id)
		{
			SqlConnection conn = DB.Connection();
			conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT name, phone_number, stylist_id, FROM clients WHERE id = @id;", conn);
      cmd.Parameters.AddWithValue("@id", id);

			SqlDataReader rdr = cmd.ExecuteReader();

			string name = null;
      string phoneNumber = null;
      int stylistId = 0;

			while (rdr.Read())
			{
				name = rdr.GetString(1);
        phoneNumber = rdr.GetString(2);
        stylistId = rdr.GetInt32(3);
			}
			if (rdr != null)
			{
				rdr.Close();
			}
			if (conn != null)
			{
				conn.Close();
			}

			return new Client(name, phoneNumber, stylistId, id);
		}

    public static void Delete(int id)
		{
			SqlConnection conn = DB.Connection();
			conn.Open();
			SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE id = @id", conn);
			cmd.Parameters.AddWithValue("@id", id);

			cmd.ExecuteNonQuery();
			conn.Close();
		}

    public static List<Client> GetAll()
		{
			List<Client> allClients = new List<Client>{};
			SqlConnection conn = DB.Connection();
			conn.Open();

			SqlCommand cmd = new SqlCommand("SELECT * FROM clients;", conn);
			SqlDataReader rdr = cmd.ExecuteReader();

			while (rdr.Read())
			{
				int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string phoneNumber = rdr.GetString(2);
        int stylistId = rdr.GetInt32(3);

				Client newClient = new Client(name, phoneNumber, stylistId, id);
				allClients.Add(newClient);
			}

			if (rdr != null)
			{
				rdr.Close();
			}
			if (conn != null)
			{
				conn.Close();
			}

			return allClients;
		}


		public static void DeleteAll()
		{
			SqlConnection conn = DB.Connection();
			conn.Open();
			SqlCommand cmd = new SqlCommand("DELETE FROM clients", conn);
			cmd.ExecuteNonQuery();
			conn.Close();
		}

    public void Save()
		{
			SqlConnection conn = DB.Connection();
			conn.Open();
			SqlCommand cmd = new SqlCommand("INSERT INTO clients (name, phone_number, stylist_id) OUTPUT INSERTED.id VALUES (@name, @phone_number, @stylist_id);", conn);

			cmd.Parameters.AddWithValue("@name", this.GetName());
      cmd.Parameters.AddWithValue("@phone_number", this.GetPhoneNumber());
      cmd.Parameters.AddWithValue("@stylist_id", this.GetStylistId());

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

    public void SetStylistId(int newStylistId)
    {
      _stylistId = newStylistId;
    }
    public int GetStylistId()
    {
      return _stylistId;
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

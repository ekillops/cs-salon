using System;
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

    public static Stylist Find(int id)
		{
			SqlConnection conn = DB.Connection();
			conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT name, phone_number FROM stylists WHERE id = @id;", conn);
      cmd.Parameters.AddWithValue("@id", id);

			SqlDataReader rdr = cmd.ExecuteReader();

			string name = null;
      string phoneNumber = null;

			while (rdr.Read())
			{
				name = rdr.GetString(1);
        phoneNumber = rdr.GetString(2);
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

    public static void Delete(int id)
		{
			SqlConnection conn = DB.Connection();
			conn.Open();
			SqlCommand cmd = new SqlCommand("DELETE FROM stylists WHERE id = @id", conn);
			cmd.Parameters.AddWithValue("@id", id);

			SqlDataReader rdr = cmd.ExecuteNonQuery();
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

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("INSERT INTO clients (name, phone_number) OUTPUT INSERTED.id VALUES (@name, @phone_number);", conn);

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

using System;
using System.Data;
using System.Data.SqlClient;

namespace Salon
{
  public class Client
  {
    private string _name;
    private string _phoneNumber;
    private int _client_id;
    private int _id;

    public Client(string name, string phoneNumber = "000-000-0000", int client_id = 0, int id = 0)
    {
      _name = name;
      _phoneNumber = phoneNumber;
      _stylist_id = stylist_id;
      _id = id;
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
      _stylist_id = newStylistId;
    }
    public int GetStylistId()
    {
      return _stylist_id;
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

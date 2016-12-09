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

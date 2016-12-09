using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Salon
{
  public class StylistTest : IDisposable
  {

    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Save_SavesToDataBase_EquivalentObject()
    {
      //Arrange
      Stylist testStylist = new Stylist("Jane Doe", "503-123-4567");
      //Act
      testStylist.Save();
      Stylist retrievedStylist = Stylist.Find(testStylist.GetId());
      //Assert
      Assert.Equal(testStylist, retrievedStylist);
    }

    [Fact]
    public void Delete_RemovesObjectFromDatabase_EmptyList()
    {
      //Arrange
      List<Stylist> expectedResult = new List<Stylist> {};
      Stylist testStylist = new Stylist("Jane Doe", "503-123-4567");
      //Act
      testStylist.Save();
      Stylist.Delete(testStylist.GetId());
      //Assert
      Assert.Equal(expectedResult, Stylist.GetAll());
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}

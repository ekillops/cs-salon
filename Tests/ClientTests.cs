using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Salon
{
  public class ClientTest : IDisposable
  {

    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Save_SavesToDataBase_EquivalentObject()
    {
      //Arrange
      Client testClient = new Client("Sally Smith", "503-111-2222", 1);
      //Act
      testClient.Save();
      Client retrievedClient = Client.Find(testClient.GetId());
      //Assert
      Assert.Equal(testClient, retrievedClient);
    }

    [Fact]
    public void Delete_RemovesObjectFromDatabase_EmptyList()
    {
      //Arrange
      List<Client> expectedResult = new List<Client> {};
      Client testClient = new Client("Sally Smith", "503-111-2222", 1);
      //Act
      testClient.Save();
      Client.Delete(testClient.GetId());
      //Assert
      Assert.Equal(expectedResult, Client.GetAll());
    }

    [Fact]
    public void Update_ChangesValuesInDatabase_EquivalentObject()
    {
      //Arrange
      Client testClient = new Client("Sally Smith", "503-111-2222", 1);
      testClient.Save();
      Client expectedResult = new Client("Sally Smith", "503-111-2222", 2, testClient.GetId());
      //Act
      testClient.Update("Sally Smith", "503-111-2222", 2);
      Client retrievedClient = Client.Find(testClient.GetId());
      //Assert
      Assert.Equal(expectedResult, retrievedClient);
    }

    [Theory]
    [InlineData("name", "Sally", 1)]
    [InlineData("phone_number", "4567", 1)]
    public void Search_FindsResultsBasedOnInputTheory(string searchColumn, string searchValue, int expectedMatches)
    {
      //Arrange
      Client testOne = new Client("Sally Smith", "503-111-2222", 1);
      Client testTwo = new Client("Bob Johnson", "503-333-4567", 2);
      testOne.Save();
      testTwo.Save();
      //Act
      List<Client> matches = Client.SearchByValue(searchColumn, searchValue);
      //Assert
      Assert.Equal(expectedMatches, matches.Count);
    }


    public void Dispose()
    {
      Client.DeleteAll();
    }
  }
}

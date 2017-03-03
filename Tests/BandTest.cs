using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
    public class BandTest : IDisposable
    {
        public BandTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_DatabaseEmptyAtFirst()
        {
            //Arrange, Act
            int result = Band.GetBands().Count;

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueIfNameEqual()
        {
            //Arrange, Act
            Band firstBand = new Band("Angry Fish");
            Band secondBand = new Band("Angry Fish");

            //Assert
            Assert.Equal(firstBand, secondBand);
        }

        [Fact]
        public void Test_Save_SavesToDatabase()
        {
            //Arrange
            Band testBand = new Band("Angry Fish");

            //Act
            testBand.Save();
            List<Band> result = Band.GetBands();
            List<Band> testList = new List<Band>{testBand};

            //Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Save_AssignsIdToObject()
        {
            //Arrange
            Band testBand = new Band("Angry Fish");

            //Act
            testBand.Save();
            Band savedBand = Band.GetBands()[0];


            int result = savedBand.GetId();
            int testId = testBand.GetId();

            //Assert
            Assert.Equal(testId, result);
        }

        [Fact]
        public void Test_Find_FindBandInDatabase()
        {
            //Arrange
            Band testBand = new Band("Angry Fish");
            testBand.Save();

            //Act
            Band foundBand = Band.Find(testBand.GetId());

            //Assert
            Assert.Equal(testBand, foundBand);
        }

        [Fact]
        public void Test_Delete_DeletesBandFromDatabase()
        {
            //Arrange
            Band testBand = new Band("Angry Fish");
            testBand.Save();

            //Act
            testBand.Delete();
            List<Band> result = Band.GetBands();
            List<Band> testBands = new List<Band>{};

            //Assert
            Assert.Equal(testBands, result);
        }

        [Fact]
        public void Test_AddVenue_AddsVenueToBand()
        {
          //Arrange
          Band testBand = new Band("Angry Fish");
          testBand.Save();

          Venue testVenue = new Venue("Red Rocks");
          testVenue.Save();

          //Act
          testBand.AddVenue(testVenue);

          List<Venue> result = testBand.GetVenues();
          List<Venue> testList = new List<Venue>{testVenue};

          //Assert
          Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_GetVenues_RetrievesAllVenuesInBands()
        {
            //Arrange
            Band testBand = new Band("Angry Fish");
            testBand.Save();
            Venue firstVenue = new Venue("Red Rocks");
            firstVenue.Save();
            Venue secondVenue = new Venue("The Fonda");
            secondVenue.Save();

            //Act
            testBand.AddVenue(firstVenue);
            testBand.AddVenue(secondVenue);
            List<Venue> testVenuesList = new List<Venue> {firstVenue, secondVenue};
            List<Venue> resultVenuesList = testBand.GetVenues();

            //Assert
            Assert.Equal(testVenuesList, resultVenuesList);
        }


        public void Dispose()
        {
            Venue.DeleteAll();
            Band.DeleteAll();
        }

    }
}

using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
    public class VenueTest : IDisposable
    {
        public VenueTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_CategoriesEmptyAtFirst()
        {
            //Arrange, Act
            int result = Venue.GetAll().Count;

            //Assert
            Assert.Equal(0, result);
        }

        public void Test_Equal_ReturnsTrueForSameName()
        {
            //Arrange, Act
            Venue firstVenue = new Venue("Red Rocks");
            Venue secondVenue = new Venue("Red Rocks");

            //Assert
            Assert.Equal(firstVenue, secondVenue);
        }

        [Fact]
        public void Test_Save_SavesVenueToDatabase()
        {
            //Arrange
            Venue testVenue = new Venue("Red Rocks");
            testVenue.Save();

            //Act
            List<Venue> result = Venue.GetAll();
            List<Venue> testList = new List<Venue>{testVenue};

            //Assert
            Assert.Equal(testList, result);
        }



        public void Dispose()
        {
            Venue.DeleteAll();
        }
    }
}

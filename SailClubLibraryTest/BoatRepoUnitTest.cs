using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;
using SailClubLibrary.Services;

namespace SailClubLibraryTest
{
    [TestClass]
    public class BoatRepoUnitTest
    {

        [TestMethod]
        public void TestAddBoat()
        {
            //Arrange
            IBoatRepository boatRepository = new BoatRepository();
            Boat boat1 = new Boat(1, BoatType.TERA, "Model", "16-3331", "Is very good :3", 32, 23, 33, "1982");
            int beforeCount = boatRepository.Count;

            //Act
            boatRepository.AddBoat(boat1);

            //Assert
            Assert.AreEqual(beforeCount + 1, boatRepository.Count);
        }

        [TestMethod]

        public void TestDeleteBoat()
        {
            //Arrange
            IBoatRepository boatRepository = new BoatRepository();
            Boat boat2 = new Boat(1, BoatType.TERA, "Model", "16-3335", "Is very good :3", 32, 23, 33, "1982");
            string sailNumberToBeDeleted = boat2.SailNumber;
            int beforeCount = boatRepository.Count;
            //Act
            boatRepository.RemoveBoat(sailNumberToBeDeleted);

            //Assert
            Assert.AreEqual(beforeCount - 1, boatRepository.Count);
        }

        [TestMethod]

        public void TestUpdateBoat()
        {
            //Arrange
            IBoatRepository boatRepository = new BoatRepository();
            Boat boatToUpdate = boatRepository.GetAllBoats()[1];
            string existingSailNumber = boatToUpdate.SailNumber;

            BoatType newBoatType = BoatType.WAYFARER;
            string newModel = "Perfect model";
            string newEngineInfo = "Perfect engine";
            double newDraft = 24;
            double newWidth = 10;
            double newLength = 20;
            string newYearOfConstruction = "2026";
            Boat updatedBoat = new Boat(2, newBoatType, newModel, existingSailNumber, newEngineInfo, newDraft, newWidth, newLength, newYearOfConstruction);

            //Act
            boatRepository.UpdateBoat(updatedBoat);

            //Assert
            Boat boatAfterUpdate = boatRepository.SearchBoat(existingSailNumber);
            Assert.AreEqual(newBoatType, boatAfterUpdate.TheBoatType);
            Assert.AreEqual(newModel, boatAfterUpdate.Model);
            Assert.AreEqual(newEngineInfo, boatAfterUpdate.EngineInfo);
            Assert.AreEqual(newDraft, boatAfterUpdate.Draft);
            Assert.AreEqual(newWidth, boatAfterUpdate.Width);
            Assert.AreEqual(newLength, boatAfterUpdate.Length);
            Assert.AreEqual(newYearOfConstruction, boatAfterUpdate.YearOfConstruction);

        }

        [TestMethod]

        public void TestSearchBoat()
        {
            //Arrange
            IBoatRepository boatRepository = new BoatRepository();
            string sailNumber = "16-1667";
            Boat boatToSearchFor = new Boat(2, BoatType.TERA, "Model", sailNumber, "Is very good :3", 32, 23, 33, "1982");
            boatRepository.AddBoat(boatToSearchFor);

            //Act
            Boat foundBoat = boatRepository.SearchBoat(sailNumber)!;

            //Assert
            Assert.AreEqual(boatToSearchFor, foundBoat);

        }
    }
}

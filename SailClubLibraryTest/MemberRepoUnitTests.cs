using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;
using SailClubLibrary.Services;

namespace SailClubLibraryTest;

[TestClass]
public class MemberRepoUnitTests
{
    [TestMethod]
    public void TestAddMember()
    {
        //Arrange
        IMemberRepository memberRepository = new MemberRepository();
        Member m1 = new Member(0, "Maria", "Strauss", "99999999", "Glostrupvej 1", "Glostrup", "Maria@mail.dk", MemberType.Adult, MemberRole.Member);
        int beforeCount = memberRepository.Count;

        //Act
        memberRepository.AddMember(m1);

        //Assert
        Assert.AreEqual(beforeCount + 1, memberRepository.Count);

    }

    [TestMethod]

    public void TestRemoveMember()
    {
        //Arrange
        IMemberRepository memberRepository = new MemberRepository();
        Member m1 = new Member(0, "Maria", "Strauss", "99999999", "Glostrupvej 1", "Glostrup", "Maria@mail.dk", MemberType.Adult, MemberRole.Member);
        memberRepository.AddMember(m1);
        int beforeCount = memberRepository.Count;

        //Act
        memberRepository.RemoveMember(m1);

        //Assert
        Assert.AreEqual(beforeCount - 1, memberRepository.Count);
    }

    [TestMethod]

    public void TestUpdateMember()
    {
        //Arrange
        IMemberRepository memberRepository = new MemberRepository();
        Member memberToUpdate = memberRepository.GetAllMembers()[0];
        string existingPhoneNumber = memberToUpdate.PhoneNumber;

        string newFirstName = "Poul";
        string newSurname = "Poul";
        string newAddress = "Poul";
        string newCity = "Poul";
        string newMail = "Poul";
        MemberType newMemberType = MemberType.Adult;
        MemberRole newMemberRole = MemberRole.Member;


        Member updatedMember = new Member(1, newFirstName, newSurname, existingPhoneNumber, newAddress, newCity, newMail, newMemberType, newMemberRole);

        //Act
        memberRepository.UpdateMember(updatedMember);

        //Assert
        Member memberAfterUpdate = memberRepository.SearchMember(existingPhoneNumber);
        Assert.AreEqual(newFirstName, memberAfterUpdate.FirstName);
        Assert.AreEqual(newSurname, memberAfterUpdate.SurName);
        Assert.AreEqual(newAddress, memberAfterUpdate.Address);
        Assert.AreEqual(newCity, memberAfterUpdate.City);
        Assert.AreEqual(newMail, memberAfterUpdate.Mail);
        Assert.AreEqual(newMemberType, memberAfterUpdate.TheMemberType);
        Assert.AreEqual(newMemberRole, memberAfterUpdate.TheMemberRole);
    }

    [TestMethod]

    public void TestSearchMember()
    {
        //Arrange
        IMemberRepository memberRepository = new MemberRepository();
        Member memberToSearchFor = memberRepository.GetAllMembers()[0];
        string phoneNumberToSearchFor = memberToSearchFor.PhoneNumber;

        //Act
        memberRepository.SearchMember(phoneNumberToSearchFor);

        //Assert
        Assert.AreEqual(phoneNumberToSearchFor, memberToSearchFor.PhoneNumber);
    }

    //[TestMethod]

    //public void TestSearchNonExistingMember()
    //{
    //    //Arrange
    //    IMemberRepository memberRepository = new MemberRepository();
    //    string phoneNumber = "999";

    //    //Act
    //    memberRepository.SearchMember(phoneNumber);

    //    //Assert
    //    Assert.AreNotEqual()
    //}

}

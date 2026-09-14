
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Controllers;
using EmployeeData;
using EmployeeDomain;

namespace MyApi.Tests.Controllers;

public class ContactControllerTests
{
    private static AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }


    [Fact]
    public async Task GetListItem_ReturnsAllContacts()
    {
        // Arrange
        await using var context = CreateContext();

        context.Contact.AddRange(
            new Contact
            {
                ContactId = 1,
                EmployeeId = 1,
                Email = "john@example.com",
                HomePhoneNumber = "0201111111"
            },
            new Contact
            {
                ContactId = 2,
                EmployeeId = 2,
                Email = "jane@example.com",
                HomePhoneNumber = "0202222222"
            }
        );

        await context.SaveChangesAsync();

        var controller = new ContactController(context);

        // Act
        var result = await controller.GetContactList();

        // Assert
        Assert.NotNull(result);

        var contacts = result.ToList();

        Assert.Equal(2, contacts.Count);
        Assert.Contains(contacts, x => x.Email == "john@example.com");
        Assert.Contains(contacts, x => x.Email == "jane@example.com");
    }


    [Fact]
    public async Task GetListItem_ReturnsContact_WhenContactExists()
    {
        // Arrange
        await using var context = CreateContext();

        var contact = new Contact
        {
            ContactId = 1,
            EmployeeId = 1,
            Email = "john@example.com",
            Description = "Employee contact"
        };

        context.Contact.Add(contact);
        await context.SaveChangesAsync();

        var controller = new ContactController(context);

        // Act
        var result = await controller.GetContactRecord(1);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Contact>>(result);

        var returnedContact =
            Assert.IsType<Contact>(actionResult.Value);

        Assert.Equal(1, returnedContact.ContactId);
        Assert.Equal(1, returnedContact.EmployeeId);
        Assert.Equal("john@example.com", returnedContact.Email);
    }


    [Fact]
    public async Task GetListItem_ReturnsNotFound_WhenContactDoesNotExist()
    {
        // Arrange
        await using var context = CreateContext();

        var controller = new ContactController(context);

        // Act
        var result = await controller.GetContactRecord(999);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }


    [Fact]
    public async Task Create_AddsContactAndReturnsOk()
    {
        // Arrange
        await using var context = CreateContext();

        var controller = new ContactController(context);

        var contact = new Contact
        {
            ContactId= 1,
            EmployeeId = 1,
            Email = "john@example.com",
            Description = "Employee contact",
            HomePhoneNumber = "0201111111",
            MobilPhoneNumber = "07111111111"
        };

        // Act
        var result = await controller.InsertContactRecord(contact);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);

        var returnedContact =
            Assert.IsType<Contact>(okResult.Value);

        Assert.Equal("john@example.com", returnedContact.Email);

        var contactInDatabase =
            await context.Contact.FindAsync(1);

        Assert.NotNull(contactInDatabase);
        Assert.Equal("john@example.com", contactInDatabase.Email);
    }


    [Fact]
    public async Task Update_ReturnsNotFound_WhenContactDoesNotExist()
    {
        // Arrange
        await using var context = CreateContext();

        var controller = new ContactController(context);

        var request = new Contact
        {
            Email = "updated@example.com"
        };

        // Act
        var result = await controller.UpdateContactRecord(999, request);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async Task Update_UpdatesContact_WhenContactExists()
    {
        // Arrange
        await using var context = CreateContext();

        var contact = new Contact
        {
            ContactId = 1,
            EmployeeId = 1,
            Email = "old@example.com",
            Description = "Old description",
            EmergencyContactName = "John Smith",
            EmergencyPhoneNumber = "0201111111",
            HomePhoneNumber = "0202222222",
            MobilPhoneNumber = "07111111111"
        };

        context.Contact.Add(contact);
        await context.SaveChangesAsync();

        var controller = new ContactController(context);

        var request = new Contact
        {
            Email = "new@example.com",
            Description = "Updated description",
            EmergencyContactName = "Jane Smith",
            EmergencyPhoneNumber = "0203333333",
            HomePhoneNumber = "0204444444",
            MobilPhoneNumber = "07222222222",
            EmployeeId = 2
        };

        // Act
        var result = await controller.UpdateContactRecord(1, request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);

        var returnedContact =
            Assert.IsType<Contact>(okResult.Value);

        Assert.Equal("new@example.com", returnedContact.Email);
        Assert.Equal("Updated description", returnedContact.Description);
        Assert.Equal("Jane Smith", returnedContact.EmergencyContactName);
        Assert.Equal("0203333333", returnedContact.EmergencyPhoneNumber);
        Assert.Equal("0204444444", returnedContact.HomePhoneNumber);
        Assert.Equal("07222222222", returnedContact.MobilPhoneNumber);
        Assert.Equal(2, returnedContact.EmployeeId);

        // Verify the database was actually updated
        var updatedContact =
            await context.Contact.FindAsync(1);

        Assert.NotNull(updatedContact);
        Assert.Equal("new@example.com", updatedContact.Email);
        Assert.Equal(2, updatedContact.EmployeeId);
    }


    [Fact]
    public async Task Update_KeepsExistingValues_WhenRequestValuesAreNull()
    {
        // Arrange
        await using var context = CreateContext();

        var contact = new Contact
        {
            ContactId= 1,
            EmployeeId = 1,
            Email = "existing@example.com",
            Description = "Existing description",
            EmergencyContactName = "John Smith",
            EmergencyPhoneNumber = "0201111111",
            HomePhoneNumber = "0202222222",
            MobilPhoneNumber = "07111111111"
        };

        context.Contact.Add(contact);
        await context.SaveChangesAsync();

        var controller = new ContactController(context);

        var request = new Contact
        {
            Email = null,
            Description = null,
            EmergencyContactName = null,
            EmergencyPhoneNumber = null,
            HomePhoneNumber = null,
            MobilPhoneNumber = null,
            EmployeeId = 0
        };

        // Act
        var result = await controller.UpdateContactRecord(1, request);

        // Assert
        Assert.IsType<OkObjectResult>(result);

        var updatedContact =
            await context.Contact.FindAsync(1);

        Assert.NotNull(updatedContact);

        Assert.Equal("existing@example.com", updatedContact.Email);
        Assert.Equal("Existing description", updatedContact.Description);
        Assert.Equal("John Smith", updatedContact.EmergencyContactName);
        Assert.Equal("0201111111", updatedContact.EmergencyPhoneNumber);
        Assert.Equal("0202222222", updatedContact.HomePhoneNumber);
        Assert.Equal("07111111111", updatedContact.MobilPhoneNumber);
        Assert.Equal(1, updatedContact.EmployeeId);
    }


    [Fact]
    public async Task DeleteItem_ReturnsNotFound_WhenContactDoesNotExist()
    {
        // Arrange
        await using var context = CreateContext();

        var controller = new ContactController(context);

        // Act
        var result = await controller.DeleteContactRecord(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async Task DeleteItem_DeletesContact_WhenContactExists()
    {
        // Arrange
        await using var context = CreateContext();

        var contact = new Contact
        {
            ContactId = 1,
            EmployeeId = 1,
            Email = "john@example.com"
        };

        context.Contact.Add(contact);
        await context.SaveChangesAsync();

        var controller = new ContactController(context);

        // Act
        var result = await controller.DeleteContactRecord(1);

        // Assert
        Assert.IsType<NoContentResult>(result);

        var deletedContact =
            await context.Contact.FindAsync(1);

        Assert.Null(deletedContact);
    }
}
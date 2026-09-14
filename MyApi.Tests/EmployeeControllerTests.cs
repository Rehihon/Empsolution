namespace MyApi.Tests;


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Controllers;
using EmployeeDomain;
using EmployeeData;




public class EmployeeControllerTests
{
    private static AppDbContext CreateContext()
    { 
        var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options; return new AppDbContext(options); }

    [Fact]
    public async Task GetListItem_ReturnsAllEmployees()
    {
        // Arrange
        await using var context = CreateContext();

        context.Employees.AddRange(
            new Employee
            {
                EmployeeId = 1,
                FirstName = "John",
                Surename = "Smith"
            },
            new Employee
            {
                EmployeeId = 2,
                FirstName = "Jane",
                Surename = "Brown"
            }
        );

        await context.SaveChangesAsync();

        var controller = new EmployeeController(context);

        // Act
        var result = await controller.GetEmployeeList();

        // Assert
        Assert.NotNull(result);

        var employees = result.ToList();

        Assert.Equal(2, employees.Count);
        Assert.Contains(employees, x => x.FirstName == "John");
        Assert.Contains(employees, x => x.FirstName == "Jane");
    }


    [Fact]
    public async Task GetListItem_ReturnsEmployee_WhenEmployeeExists()
    {
        // Arrange
        await using var context = CreateContext();

        var employee = new Employee
        {
            EmployeeId = 1,
            FirstName = "John",
            Surename = "Smith"
        };

        context.Employees.Add(employee);
        await context.SaveChangesAsync();

        var controller = new EmployeeController(context);

        // Act
        var result = await controller.GetEmployeeRecord(1);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Employee>>(result);

        var returnedEmployee =
            Assert.IsType<Employee>(actionResult.Value);

        Assert.Equal(1, returnedEmployee.EmployeeId);
        Assert.Equal("John", returnedEmployee.FirstName);
        Assert.Equal("Smith", returnedEmployee.Surename);
    }


    [Fact]
    public async Task GetListItem_ReturnsNotFound_WhenEmployeeDoesNotExist()
    {
        // Arrange
        await using var context = CreateContext();

        var controller = new EmployeeController(context);

        // Act
        var result = await controller.GetEmployeeRecord(999);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }


    [Fact]
    public async Task Create_AddsEmployeeAndReturnsOk()
    {
        // Arrange
        await using var context = CreateContext();

        var controller = new EmployeeController(context);

        var employee = new Employee
        {
            EmployeeId = 1,
            FirstName = "John",
            Surename = "Smith"
        };

        // Act
        var result = await controller.InsertEmployeeRecord(employee);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);

        var returnedEmployee =
            Assert.IsType<Employee>(okResult.Value);

        Assert.Equal("John", returnedEmployee.FirstName);

        var employeeInDatabase =
            await context.Employees.FindAsync(1);

        Assert.NotNull(employeeInDatabase);
        Assert.Equal("John", employeeInDatabase.FirstName);
    }


    [Fact]
    public async Task UpdateEmployee_ReturnsNotFound_WhenEmployeeDoesNotExist()
    {
        // Arrange
        await using var context = CreateContext();

        var controller = new EmployeeController(context);

        var request = new Employee
        {
            FirstName = "Updated"
        };

        // Act
        var result = await controller.UpdateEmployeeRecord(999, request);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async Task UpdateEmployee_UpdatesEmployee_WhenEmployeeExists()
    {
        // Arrange
        await using var context = CreateContext();

        var employee = new Employee
        {
            EmployeeId = 1,
            FirstName = "John",
            Surename = "Smith"
        };

        context.Employees.Add(employee);
        await context.SaveChangesAsync();

        var controller = new EmployeeController(context);

        var request = new Employee
        {
            FirstName = "James",
            Surename = "Brown"
        };

        // Act
        var result = await controller.UpdateEmployeeRecord(1, request);

        // Assert
        Assert.IsType<NoContentResult>(result);

        var updatedEmployee =
            await context.Employees.FindAsync(1);

        Assert.NotNull(updatedEmployee);
        Assert.Equal("James", updatedEmployee.FirstName);
        Assert.Equal("Brown", updatedEmployee.Surename);
    }


    [Fact]
    public async Task DeleteTodoItem_ReturnsNotFound_WhenEmployeeDoesNotExist()
    {
        // Arrange
        await using var context = CreateContext();

        var controller = new EmployeeController(context);

        // Act
        var result = await controller.DeleteEmployeeRecord(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async Task DeleteTodoItem_DeletesEmployee_WhenEmployeeExists()
    {
        // Arrange
        await using var context = CreateContext();

        var employee = new Employee
        {
            EmployeeId = 1,
            FirstName = "John",
            Surename = "Smith"
        };

        context.Employees.Add(employee);
        //dotawait context.SaveChangesAsync();

        var controller = new EmployeeController(context);

        // Act
        var result = await controller.DeleteEmployeeRecord(1);

        // Assert
        Assert.IsType<NoContentResult>(result);

        var deletedEmployee =
            await context.Employees.FindAsync(1);

        Assert.Null(deletedEmployee);
    }
}


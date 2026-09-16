using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Employeesln;

public class EmployeeService
{
    private readonly HttpClient _httpClient;

    public EmployeeService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://localhost:5001/");
    }

    public async Task<List<Employee>> GetEmployeesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Employee>>("MyApi/employees");

    }
    public async Task AddEmployeeAsync(Employee employee)
    {
        await _httpClient.PostAsJsonAsync("MyApi/employees", employee);
    }
    public async Task<List<Countries>> GetCountriesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Countries>>("MyApi/employees");

    }


}
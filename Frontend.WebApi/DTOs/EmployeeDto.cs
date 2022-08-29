using System;

namespace Frontend.WebApi.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public int Number { get; set; }

        public string City { get; set; }

        public DateTime StartDate { get; set; }
        public int CountryId { get; set; }

        public EmployeeDto(string name, string address, int number, string city, DateTime startDate, int countryId)
        {
            Name = name;
            Address = address;
            Number = number;
            City = city;
            StartDate = startDate;
            CountryId = countryId;
        }

    }
}

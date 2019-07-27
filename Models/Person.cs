namespace DynamicXmlCasting.Models
{

    public class Address
    {
        public string street { get; set; }
        public string zipCode { get; set; }
        public string State { get; set; }
    }

    public class Vehicle
    {
        public string make { get; set; }
        public string model { get; set; }
        public string vin { get; set; }
    }

    public class Person
    {
        public Address address { get; set; }
        public string name { get; set; }
        public string dob { get; set; }
        public Vehicle vehicle { get; set; }
    }

}
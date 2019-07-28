using System.Dynamic;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System;
using DynamicXmlCasting.Models;
using System.Collections;
using System.IO;
using DynamicXmlCasting.Xml;
using Newtonsoft.Json;

namespace DynamicXmlCasting
{
    public class Program
    {
        static void Main(string[] args)
        {
            // var xml = GetXml("address");
            dynamic xmlOutputData = CastXml();
            Console.WriteLine(xmlOutputData);
        }
        public static string GetXml(string testName){
            string xml="";
            switch (testName)
            {
                case "address":
                    xml = DynamicXmlCasting.Testing.Tests.addressTest;
                    break;
                case "person":
                    xml= DynamicXmlCasting.Testing.Tests.personTest;
                    break;
                default:
                    throw new NotSupportedException();
            }
            return xml;
        }
        public static dynamic CastXml(){
            XmlHelper ser = new XmlHelper();
            string path = string.Empty;
            string xmlInputData = string.Empty;
            string xmlOutputData = string.Empty;
            Random random = new Random();
            int randomNumber=random.Next(1,3);

            switch(randomNumber){
                case 1:
                    //customer test
                    path = Directory.GetCurrentDirectory() + @".\TestStrings\Customer.xml";
                    xmlInputData = File.ReadAllText(path);
                    break;
                case 2:
                    //person test
                    xmlInputData = Testing.Tests.personTest;
                    break;
                case 3:
                    //address test
                    xmlInputData = Testing.Tests.addressTest;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            XDocument doc = XDocument.Parse(xmlInputData); //or XDocument.Load(path)
            string jsonText = JsonConvert.SerializeXNode(doc);
            dynamic dyn = JsonConvert.DeserializeObject<ExpandoObject>(jsonText);
            // dynamic XmlObject = DynamicXml.Parse(xmlInputData);
            Customer customerTest = dyn as Customer;
            if(customerTest != null){
                return customerTest; 
            }
            Person personTest = dyn as Person;
            if(personTest!=null){
                return personTest;
            }
            Address addressTest = dyn as Address;
            if(addressTest!=null){
                return addressTest;
            }
            return dyn;
        }
    }
}

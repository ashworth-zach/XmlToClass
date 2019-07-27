using System;
using DynamicXmlCasting.Models;
using System.Collections;
using System.IO;
using DynamicXmlCasting.Xml;

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

            // EXAMPLE 2
            path = Directory.GetCurrentDirectory() + @".\TestStrings\Customer.xml";
            xmlInputData = File.ReadAllText(path);

            Customer customer2 = ser.Deserialize<Customer>(xmlInputData);
            xmlOutputData = ser.Serialize<Customer>(customer2);
            return xmlOutputData;
        }
        
    }
}

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

            dynamic xmlDeserialized = ser.Deserialize<Customer>(xmlInputData);
            xmlOutputData = ser.Serialize<Customer>(xmlDeserialized);
            return xmlOutputData;
        }
        
    }
}

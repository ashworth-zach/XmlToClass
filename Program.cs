using System.Dynamic;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System;
using DynamicXmlCasting.Models;
using System.Collections;
using System.IO;
using DynamicXmlCasting.Xml;
using Newtonsoft.Json;
using DynamicXmlCasting.Utilities;

namespace DynamicXmlCasting
{
    public class Program
    {
        static void Main(string[] args)
        {
            // var xml = GetXml("address");
            dynamic xmlOutputData = CastXml();
        }
        public static dynamic GetXml(string testName){
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
            dynamic type;
            switch(randomNumber){
                case 1:
                    //customer test
                    path = Directory.GetCurrentDirectory() + @".\TestStrings\Customer.xml";
                    xmlInputData = File.ReadAllText(path);

                    type = new Customer();
                    break;
                case 2:
                    //person test
                    xmlInputData = Testing.Tests.personTest;
                    type = new Person();
                    break;
                case 3:
                    //address test
                    xmlInputData = Testing.Tests.addressTest;
                    type = new Address();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            dynamic dyn = DynamicXml.XmlConvertToExpando(xmlInputData);

            dynamic newObject = ExpandoHelper.ParseDictionary(dyn as ExpandoObject, type.GetType());

            Console.WriteLine("Xml recieved is representing a: {0}", newObject.GetType());

            return dyn;
        }
    }
}

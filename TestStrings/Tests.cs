namespace DynamicXmlCasting.Testing{
    public static class Tests{
        public static string addressTest = "<address><State>OK</State><street>5951 s birmimasd blvd</street><zipCode>34234</zipCode></address>";
        public static string personTest="<person><address><State>OK</State><street>5951 s birmimasd blvd</street><zipCode>34234</zipCode></address><dob>DATETIME</dob><name>bob</name><vehicle><make />"+
         "<model /><vin>3423423423234fa</vin></vehicle></person>";
    }
}
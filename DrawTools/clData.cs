using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DrawTools
{
    public class clNode
    {
        public string typeNode { get; set; }
        public List<clNode> nodes { get; set; }
        [JsonExtensionData]
        public Dictionary<string, object> free { get; set; }
    }

    public class LstNodes
    {
        public List<clNode> nodes { get; set; }
    }

    public class LstLinks
    {
        public List<clLink> links { get; set; }
    }


    public class clLink
    {
        public string typeLink { get; set; }
        [JsonExtensionData]
        public Dictionary<string, object> free { get; set; }

    }
    public class clVue
    {
        public string guidVue { get; set; }
        public string nomVue { get; set; }
        public string guidGVue { get; set; }
        public string guidTypeVue { get; set; }
        public string nomTypeVue { get; set; }
        public string guidEnvironnement { get; set; }
        public string nomEnvironnement { get; set; }
        public string guidVueInf { get; set; }
        public string nomVueInf { get; set; }
        public List<clNode> nodes { get; set; }
        public List<clLink> links { get; set; }

    }

    public class clAppVersion
    {
        public string guidAppVersion { get; set; }
        public string version { get; set; }
        public List<clVue> vues { get; set; }
    }

    public class clApplication
    {
        public string guidApplication { get; set; }
        public string nomApplication { get; set; }
        public List<clAppVersion> appVersions { get; set; }
    }

    public class LstApplications
    {
        public List<clApplication> applications { get; set; }
    }

    public class ApplicationResp
    {
        public string status { get; set; }
        public string message { get; set; }
        public LstApplications content { get; set; }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class clDiscoQuery
    {
        public string query { get; set; }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

}

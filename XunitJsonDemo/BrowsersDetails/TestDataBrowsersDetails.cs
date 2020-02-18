using System.Collections.Generic;

namespace XunitJsonDemo.BrowsersDetails
{
    public class TestDataBrowsersDetails
    {
        public List<BrowserDetail> BrowsersDetails { get; set; }
    }

    public class BrowserDetail
    {
        public string Browser { get; set; }
        public string Language { get; set; }
        public string Url { get; set; }
    }
}

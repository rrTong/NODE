/*
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

public sealed class ApplicationView { }
//public static Size PreferredLaunchViewSize { get; set; }

namespace nodeWin10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public sealed partial class MainPage : Page
    {
        private readonly Uri timeInfoHost = new Uri("http://nodeapi.online/v2/users.json");
        private readonly HttpClient = new HttpClient();

        public MainPage()
        {
            this.InitializeComponent();

//            var headers = httpClient.DefaultRequestHeaders;
        }
    }

}
*/

/
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace nodeWin10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly Uri timeInfoHost = new Uri("http://nodeapi.online/v2/users.json");
        private readonly HttpClient httpClient = new HttpClient();

        public MainPage()
        {
            this.InitializeComponent();

            var headers = httpClient.DefaultRequestHeaders;
            headers.UserAgent.ParseAdd("ie");
            headers.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            TimeInfo ti = await PerformTimeRequest();
            this.TimeText.Text = ti.datetime;
        }

        private async Task<TimeInfo> PerformTimeRequest()
        {
            string json = await httpClient.GetStringAsync(timeInfoHost);
            return Deserialize<TimeInfo>(json);
        }

        public T Deserialize<T>(string json)
        {
            var bytes = Encoding.Unicode.GetBytes(json);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                var jser = new DataContractJsonSerializer(typeof(T));
                return (T)jser.ReadObject(ms);
            }
        }
    }

    [DataContract]
    public class TimeInfo
    {
        [DataMember]
        public string tz;
        [DataMember]
        public string hour;
        [DataMember]
        public string datetime;
        [DataMember]
        public string second;
        [DataMember]
        public string error;
        [DataMember]
        public string minute;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

namespace Ogame.Common
{
    public class Account
    {
        public string Email;
        public string Password;
        public Univers Univers;
        private HttpClient _HttpClient;
        public bool Connected;

        public Account(string Email, string Password, Univers Univers)
        {
            this.Email = Email;
            this.Password = Password;
            this.Univers = Univers;
            this._HttpClient = new HttpClient();
            _HttpClient.BaseAddress = new Uri("https://fr.ogame.gameforge.com/");
        }

        public static async Task<Account> CreateAccount(string Email, string Password, Univers Univers)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://" + Univers.Link());

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("somefakename", ""),
                    new KeyValuePair<string, string>("anotherfakename", ""),
                    new KeyValuePair<string, string>("v", "3"),
                    new KeyValuePair<string, string>("step", "validate"),
                    new KeyValuePair<string, string>("kid", ""),
                    new KeyValuePair<string, string>("errorCodeOn", "1"),
                    new KeyValuePair<string, string>("is_utf8", "1"),
                    new KeyValuePair<string, string>("email", Email),
                    new KeyValuePair<string, string>("password", Password),
                    new KeyValuePair<string, string>("uni_url", Univers.Link()),
                    new KeyValuePair<string, string>("agb", "on"),

                });

                var result = await client.PostAsync("/game/reg/newredirect.php", content);
                string resultContent = await result.Content.ReadAsStringAsync();
                if (resultContent.Contains("ogame-planet-coordinates"))
                    return new Account(Email, Password, Univers);
            }
            return null;
        }

        public async Task LevelUpMineMetal()
        {
            var result2 = await _HttpClient.GetAsync("https://s1-fr.ogame.gameforge.com/game/index.php?page=resources");
            string result2content = await result2.Content.ReadAsStringAsync();
            var token = StringUtils.GetStringAfter(result2content, "https://s1-fr.ogame.gameforge.com/game/index.php?page=resources&modus=1&type=1&menge=1&token=", 32);

            var result3 = await _HttpClient.GetAsync("https://s1-fr.ogame.gameforge.com/game/index.php?page=resources&modus=1&type=1&menge=1&token=" + token);
            var resp = await result3.Content.ReadAsStringAsync();
            Console.WriteLine("Mine De Metal Up");
        }

        public async Task Login()
        {
            
            var content = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("kid", ""),
                    new KeyValuePair<string, string>("login", this.Email),
                    new KeyValuePair<string, string>("pass", this.Password),
                    new KeyValuePair<string, string>("uni", this.Univers.Link())
            });



            var result = await _HttpClient.PostAsync("/main/login", content);
            string resultContent = await result.Content.ReadAsStringAsync();
            if (resultContent.Contains("ogame-planet-coordinates"))
                Connected = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Net;

namespace NaploNotifier
{
    public class MayorSession
    {
        public const string UserAgent = "Solymosi MaYoR Notifier v0.1";

        public string SessionID = "";
        public CookieContainer Cookies = new CookieContainer();

        public HtmlDocument DoRequest(string Page, string Sub, string F, string Parameters, string Policy, string Method, string PostData)
        {
            DoLogin(Policy);
            HttpWebResponse r = ExecuteRequest(CreateRequest(Page, Sub, F, Parameters, Policy, Method, PostData));
            HtmlDocument doc = new HtmlDocument();
            doc.Load(r.GetResponseStream(), Encoding.UTF8);
            r.Close();
            return doc;
        }

        public void DoLogin(string ToPolicy)
        {
            HttpWebRequest r = this.CreateRequest("auth", "", "login", "toPolicy=" + ToPolicy, "public", "post", "action=login&toPSF=::&userAccount=" + Mayor.Settings.User + "&userPassword=" + Mayor.Settings.Password + "&toPolicy=" + ToPolicy);
            HttpWebResponse p = ExecuteRequest(r);
            if (p.StatusCode != HttpStatusCode.Found || !p.GetResponseHeader("Location").Contains("policy=" + ToPolicy))
            {
                p.Close();
                throw new LoginFailedException();
            }
            try
            {
                Match m = Regex.Match(p.GetResponseHeader("Location"), "&sessionID=(?<id>[0-9a-f]+)", RegexOptions.IgnoreCase);
                SessionID = m.Groups["id"].Value;
            }
            catch { throw new LoginFailedException(); }
            finally { p.Close(); }
        }

        public string CreateURL(string Page, string Sub, string F, string Parameters, string Policy, string Skin)
        {
            return Mayor.Settings.BaseAddress + "?page=" + Page + "&sub=" + Sub + "&f=" + F + "&" + Parameters + "&sessionID=" + SessionID + "&policy=" + Policy + "&skin=" + Skin;
        }

        public HttpWebRequest CreateRequest(string Page, string Sub, string F, string Parameters, string Policy, string Method, string PostData)
        {
            ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });
            HttpWebRequest r = (HttpWebRequest)HttpWebRequest.Create(CreateURL(Page, Sub, F, Parameters, Policy, "ajax"));
            r.Timeout = -1;
            r.ProtocolVersion = new Version(1, 0);
            r.Method = Method;
            r.KeepAlive = false;
            r.CookieContainer = Cookies;
            r.Proxy = WebRequest.DefaultWebProxy;
            if (Method.ToLower() == "post")
            {
                r.ContentType = "application/x-www-form-urlencoded";
                byte[] content = Encoding.UTF8.GetBytes(PostData);
                r.ContentLength = content.Length;
                r.GetRequestStream();
                r.GetRequestStream().Write(content, 0, content.Length);
                r.GetRequestStream().Flush();
                r.GetRequestStream().Close();
            }
            r.AllowAutoRedirect = false;
            r.UserAgent = UserAgent;
            return r;
        }

        public HttpWebResponse ExecuteRequest(HttpWebRequest Request)
        {
            ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });
            HttpWebResponse r = (HttpWebResponse)Request.GetResponse();
            return r;
        }

        public static string GetTextFromStream(Stream s)
        {
            string r = "";
            byte[] b = new byte[256];
            int c = s.Read(b, 0, 256);
            while (c > 0)
            {
                r += Encoding.UTF8.GetString(b);
                c = s.Read(b, 0, 256);
            }
            return r;
        }

        public static bool SessionExists(WebResponse Response)
        {
            return Response.ResponseUri.ToString().Contains("policy=private");
        }
    }

    public class InvalidSessionException : Exception { }
    public class HttpRedirectException : Exception { }
    public class HttpErrorException : Exception { }
    public class LoginFailedException : Exception { }

}

using Ical.Net;
using Ical.Net.CalendarComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace HTMLTableWebScraper
{
    public static class knbsbCompetition
    {
        private static int fileSeqeunce = Convert.ToInt32((DateTime.UtcNow - new DateTime(2022, 1, 1, 0, 0, 0)).TotalSeconds);

        public static CalendarEvent GameCalenderEvent(IEnumerable<HtmlAgilityPack.HtmlNode> data,
                                                      DateTime date,
                                                      string timeZone,
                                                      string uidPrefix)
        {
            var home = System.Web.HttpUtility.HtmlDecode(data.ElementAt(0).InnerText.Trim());
            var away = System.Web.HttpUtility.HtmlDecode(data.ElementAt(1).InnerText.Trim());
            var field = System.Web.HttpUtility.HtmlDecode(data.ElementAt(2).InnerText.Trim());
            var matchNr = data.ElementAt(3).Descendants("span").Where(n => n.HasClass("external-match-id")).First().InnerText.Trim();
            var matchTime = data.ElementAt(3).Descendants("span").Where(n => n.HasClass("match-time")).First().InnerText.Trim();
            TimeSpan time = TimeSpan.Parse(matchTime);
            DateTime MatchDateTime = date + time;
            Console.WriteLine($"{matchNr} : {home} - {away} om {matchTime}");
            return CreateCalenderEvents.CreateBaseballCalendarEvent(matchNr,
                                                                        home,
                                                                        away,
                                                                        field,
                                                                        date,
                                                                        time,
                                                                        timeZone,
                                                                        uidPrefix,
                                                                        fileSeqeunce);

        }

        private static Calendar GetICalCalender(string wbscUrl,
                                               string uidPrefix,
                                               string timeZone)
        {
            var calendar = new Calendar();
            calendar.AddTimeZone(timeZone);

            string htmlCode;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, "AvoidError");
                htmlCode = client.DownloadString(wbscUrl);
            }

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlCode);
            var headers = doc.DocumentNode.SelectSingleNode("//div[@class='container-responsive']");
            if (headers != null)
            {
                foreach (var speldag in headers.Descendants("h4"))
                {
                    Console.WriteLine(speldag.InnerText.Trim());
                    var provider = System.Globalization.CultureInfo.CreateSpecificCulture("nl-NL");
                    DateTime date = DateTime.Parse(speldag.InnerText.Trim(), provider);
                    foreach (var wedstrijd in speldag.NextSibling.NextSibling.Descendants("tr"))
                    {
                        var data = wedstrijd.Descendants("td");
                        calendar.Events.Add(knbsbCompetition.GameCalenderEvent(data, date, timeZone, uidPrefix));
                    }
                }
            }
            return calendar;
        }

        public static void WriteICalCalender(string wbscUrl,
                                             string uidPrefix,
                                             string timeZone,
                                             string fileNamePrefix)
        {
            var calendar = GetICalCalender(wbscUrl,
                                           uidPrefix,
                                           timeZone);
            calendar.WriteCalendar(fileNamePrefix + "_" + fileSeqeunce.ToString());
        }
    }
}

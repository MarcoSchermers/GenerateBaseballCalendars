using System;
using System.Data;
using System.Linq;
using System.Net;
using Ical.Net.Serialization;
using System.IO;
using Newtonsoft.Json;
using GenerateBaseballCalendars.Competitions;

namespace GenerateBaseballCalendars
{


    public static class CalendarExtention
    {
        public static void WriteCalendar(this Ical.Net.Calendar calendar, string filename)
        {
            var serializer = new CalendarSerializer();
            var serializedCalendar = serializer.SerializeToString(calendar).Replace("PRODID:-//github.com/rianjs/ical.net//NONSGML ical.net 4.0//EN", "PRODID:-//  //NONSGML //EN");
            string docPath = Path.GetFullPath(@"..\..\..\..\Export", AppDomain.CurrentDomain.BaseDirectory);
            File.WriteAllText(Path.Combine(docPath, filename +  ".ics"), serializedCalendar);
        }
    }

    internal class Program
    {

        static void Main(string[] args)
        {
            wbscCompetition();
            knbsbCompetition();
            mlbCompetition();
            schaatsKalender();
            //mlbTransactions();
        }

        static void mlbTransactions()
        {


            using (WebClient client = new WebClient()) 
            {
                string jsonCode = "";
                jsonCode = client.DownloadString("https://statsapi.mlb.com/api/v1/transactions?teamId=142&startDate=2018-01-01&endDate=2023-03-01");
                TransactionsRoot myDeserializedClass = JsonConvert.DeserializeObject<TransactionsRoot>(jsonCode);
                var listType = myDeserializedClass.transactions.GroupBy(t => (t.typeCode, t.typeDesc),
                                                                         (key, group) => new { 
                                                                                                key.typeCode, 
                                                                                                key.typeDesc,
                                                                                                Count = group.Count(),
                                                                                                transactions = group
                                                                                             }
                                                                       );
                var list = myDeserializedClass.transactions.Where( t => t.person != null)
                                                           .GroupBy(t => (t.personId, t.personFullname), 
                                                                    (key, group) => new { 
                                                                                        key.personId,
                                                                                        key.personFullname,
                                                                                        group.Last().date,
                                                                                        group.Last().description,
                                                                                        transactions = group.OrderByDescending(t => t.date).ToList()
                                                                                       }
                                                                   )
                                                           .OrderByDescending(p => p.date)
                                                           .ToList();
                                                           

            }

        }

        static void mlbCompetition()
        {
            #region old code
            //

            //string filename = "";
            //timeZone = "Etc/UTC";



            //mlb
            /*
            jsonCode = client.DownloadString("https://statsapi.mlb.com/api/v1/schedule?sportId=1&startDate=2022-10-06&endDate=2022-11-10");
            filename = "MLB-Playoff-2022" + "_" + fileSeqeunce.ToString();
            uidPrefix = "NL-HH-Reg-2022-";
            */

            //abl

            //jsonCode = client.DownloadString("https://statsapi.mlb.com/api/v1/schedule?sportId=17&leagueId=595&startDate=2022-10-01&endDate=2023-03-01");
            //jsonCode = client.DownloadString("https://statsapi.mlb.com/api/v1/schedule?sportId=17&leagueId=595&startDate=2022-10-01&endDate=2023-03-01&hydrate=team(venue(timezone)),venue(timezone),gameInfo,seriesStatus,seriesSummary,gameData");
            //filename = "ALB-Regular-2022-2023" + "_" + fileSeqeunce.ToString();
            //uidPrefix = "AU-ABL-Reg-2022-2023-";


            //wbc

            //jsonCode = client.DownloadString();
            //filename = "WBC-2023" + "_" + fileSeqeunce.ToString();
            //uidPrefix = "WBC-2023-";


            //jsonCode = client.DownloadString("https://statsapi.mlb.com/api/v1/schedule?sportId=51&leagueId=160&startDate=2023-03-11&endDate=2023-03-11&gameTypes=F,D,L,W&teamId=944&hydrate=team(venue(timezone)),venue(timezone),gameInfo,seriesStatus,seriesSummary,gameData");
            //filename = "WBC-2023" + "_" + fileSeqeunce.ToString();
            //uidPrefix = "WBC-2023-";

            //jsonCode = client.DownloadString("https://statsapi.mlb.com/api/v1/schedule?sportId=1&startDate=2022-10-06&endDate=2022-11-10");
            //filename = "MLB-Playoff-2022" + "_" + fileSeqeunce.ToString();
            //uidPrefix = "NL-HH-Reg-2022-";

            #endregion


            try
            {
                #region Played Competitions
                //// WBC 2023 
                //MlbCompetition.WriteICalCalender("https://statsapi.mlb.com/api/v1/schedule?sportId=51&leagueId=160&startDate=2022-07-01&endDate=2023-04-01&gameTypes=F,D,L,W&hydrate=team(venue(timezone)),venue(timezone),gameInfo,seriesStatus,seriesSummary,gameData",
                //                                  "WBC-2023-",
                //                                  "Etc/UTC",
                //                                  "WBC-2023"
                //                                 );

                //// WBC 2023 -Final
                //MlbCompetition.WriteICalCalender("https://statsapi.mlb.com/api/v1/schedule?sportId=51&leagueId=160&startDate=2023-03-11&endDate=2023-03-11&gameTypes=F,D,L,W&teamId=944&hydrate=team(venue(timezone)),venue(timezone),gameInfo,seriesStatus,seriesSummary,gameData",
                //                                  "WBC-2023-",
                //                                  "Etc/UTC",
                //                                  "WBC-2023-filter"
                //                                 );

                //// ABL 2022-2023
                //MlbCompetition.WriteICalCalender("https://statsapi.mlb.com/api/v1/schedule?sportId=17&leagueId=595&startDate=2022-10-01&endDate=2023-03-01&hydrate=team(venue(timezone)),venue(timezone),gameInfo,seriesStatus,seriesSummary,gameData",
                //                                  "AU-ABL-Reg-2022-2023-",
                //                                  "Etc/UTC",
                //                                  "ALB-Regular-2022-2023"
                //                                 );

                // MLB Playoff 
                //MlbCompetition.WriteICalCalender("https://statsapi.mlb.com/api/v1/schedule?sportId=1&startDate=2023-10-02&endDate=2023-11-10&hydrate=team(venue(timezone)),venue(timezone),gameInfo,seriesStatus,seriesSummary,gameData",
                //                                  "MLB-2023-",
                //                                  "Etc/UTC",
                //                                  "MLB-Playoff-2023"
                //                                 );

                #endregion


                //// ABL 2023-2024
                MlbCompetition.WriteICalCalender("https://statsapi.mlb.com/api/v1/schedule?sportId=17&leagueId=595&startDate=2023-10-01&endDate=2024-03-01&hydrate=team(venue(timezone)),venue(timezone),gameInfo,seriesStatus,seriesSummary,gameData",
                                                  "AU-ABL-Reg-2023-2024-",
                                                  "Etc/UTC",
                                                  "ALB-Regular-2023-2024"
                                                 );



            }
            catch (Exception ex)
            {
                throw;
            }
        }

        static void wbscCompetition()
        {
            try
            {

                #region Played Competitions
                /*

                // HHW 2022
                WbscCompetition.WriteICalCalender("https://stats.knbsbstats.nl/en/events/2022-honkbalweek-haarlem/schedule-and-results",
                                                  "NL-HH-Reg-2022-",
                                                  "Europe/Amsterdam",
                                                  "HHW-2022"
                                                 );

                // WK U18 2023
                WbscCompetition.WriteICalCalender("https://www.wbsc.org/en/events/2023-u18-baseball-world-cup/schedule-and-results",
                                                  "WBSC-WCU18-2023-",
                                                  "Asia/Taipei",
                                                  "WC-U18-2023"
                                                 );


                //// EK 2023 
                WbscCompetition.WriteICalCalender("https://www.wbsceurope.org/en/events/2023-european-baseball-championship/schedule-and-results",
                                                  "WBSC-EU-EC-2023-",
                                                  "Europe/Prague",
                                                  "EC-2023"
                                                 );

                 */
                #endregion

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        static void knbsbCompetition()
        {


            try
            {
                #region Played Competitions
                /*
                // Hoofdklasse seizoen 2023 knbsb site
                
                knbsbCompetition.WriteICalCalender("https://www.knbsb.nl/sportlink_api/competitions/Honkbal/6399/programma/",
                                                   "NL-HH-WCTop-2023-",
                                                   "Europe/Amsterdam",
                                                   "KNBSB-WCTop-2023"
                                                  );
                knbsbCompetition.WriteICalCalender("https://www.knbsb.nl/sportlink_api/competitions/Honkbal/6400/programma/",
                                                   "NL-HH-WCBottom-2023-",
                                                   "Europe/Amsterdam",
                                                   "KNBSB-WCBottom-2023"
                                                  );
                knbsbCompetition.WriteICalCalender("https://www.knbsb.nl/sportlink_api/competitions/Honkbal/6401/programma/",
                                                   "NL-HH-PO-2023-",
                                                   "Europe/Amsterdam",
                                                   "KNBSB-PO-2023"
                                                  );

                KnbsbCompetition.WriteICalCalender("https://www.knbsb.nl/sportlink_api/competitions/Honkbal/6402/programma/",
                                                   "NL-HH-HS-2023-",
                                                   "Europe/Amsterdam",
                                                   "KNBSB-HS-2023"
                                                  );

                // Hoofdklasse seizoen 2023 knbsb stats site
                knbsbInWbscCompetition.WriteICalCalender("https://stats.knbsbstats.nl/en/events/2023-hoofdklasse-honkbal/schedule-and-results",
                                                         "NL-HH-{RoundRef}-2023-",
                                                         "Europe/Amsterdam",
                                                         "KNBSB-2023"
                                                        );

                */
                #endregion



            }
            catch (Exception ex)
            {
                throw;
            }

        }


        static void schaatsKalender()
        {
            try
            {
                SchaatsenKalender.WriteICalCalender("SKATE-2023-", "Skate-2023");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

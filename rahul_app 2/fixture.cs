using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rahul_app_2
{
    public class Match
    {
        public string match_id { get; set; }
        public string match_static_id { get; set; }
        public string match_comp_id { get; set; }
        public string match_date { get; set; }
        public string match_formatted_date { get; set; }
        public string match_season_beta { get; set; }
        public string match_week_beta { get; set; }
        public string match_venue_beta { get; set; }
        public string match_venue_id_beta { get; set; }
        public string match_venue_city_beta { get; set; }
        public string match_status { get; set; }
        public string match_timer { get; set; }
        public string match_time { get; set; }
        public string match_commentary_available { get; set; }
        public string match_localteam_id { get; set; }
        public string match_localteam_name { get; set; }
        public string match_localteam_score { get; set; }
        public string match_visitorteam_id { get; set; }
        public string match_visitorteam_name { get; set; }
        public string match_visitorteam_score { get; set; }
        public string match_ht_score { get; set; }
        public string match_ft_score { get; set; }
        public string match_et_score { get; set; }
    }

    public class Params
    {
        public string Action { get; set; }
        public string APIKey { get; set; }
        public string comp_id { get; set; }
        public string match_date { get; set; }
    }

    public class fixture
    {
        public int APIVersion { get; set; }
        public int APIRequestsRemaining { get; set; }
        public string DeveloperAuthentication { get; set; }
        public List<Match> matches { get; set; }
        public string Action { get; set; }
        public Params Params { get; set; }
        public double ComputationTime { get; set; }
        public string IP { get; set; }
        public string ERROR { get; set; }
        public string ServerName { get; set; }
        public string ServerAddress { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rahul_app_2
{
    public class Category
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int parent { get; set; }
        public int post_count { get; set; }
    }

    public class Category2
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int parent { get; set; }
        public int post_count { get; set; }
    }

    public class Author
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string nickname { get; set; }
        public string url { get; set; }
        public string description { get; set; }
    }

    public class CustomFields
    {
        public List<string> imgurl { get; set; }
        public List<string> position { get; set; }
        public List<string> starplayer { get; set; }
    }

    public class Post
    {
        public int id { get; set; }
        public string type { get; set; }
        public string slug { get; set; }
        public string url { get; set; }
        public string status { get; set; }
        public string title { get; set; }
        public string title_plain { get; set; }
        public string content { get; set; }
        public string excerpt { get; set; }
        public string date { get; set; }
        public string modified { get; set; }
        public List<Category2> categories { get; set; }
        public List<object> tags { get; set; }
        public Author author { get; set; }
        public List<object> comments { get; set; }
        public List<object> attachments { get; set; }
        public int comment_count { get; set; }
        public string comment_status { get; set; }
        public CustomFields custom_fields { get; set; }
        public string logo { get; set; }
        public string position { get; set; }
        public string imp { get; set; }
    }

    public class players
    {
        public string status { get; set; }
        public int count { get; set; }
        public int pages { get; set; }
        public Category category { get; set; }
        public List<Post> posts { get; set; }
    }
}

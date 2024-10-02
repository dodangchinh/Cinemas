using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chinh_C1_Cinemas
{
    public class MovieRepository : IRepository<Movie>
    {
        public List<Movie> _lstMovie { get; set; }

        public MovieRepository() 
        { 
            _lstMovie = new List<Movie>();
            Load();
        }

        void Load()
        {
            DataProvider.pathData = "data/Movie.xml";
            DataProvider.Open();

            string xPath = string.Format("//Movie");

            XmlNodeList listNode = DataProvider.getDsNode(xPath);

            foreach (XmlNode item in listNode)
            {
                Movie movie = new Movie();
                movie.Id = item.Attributes["Id"].Value;
                movie.Name = item.Attributes["Name"].Value;
                movie.Description = item.Attributes["Description"].Value;
                movie.Duration = int.Parse(item.Attributes["Duration"].Value);
                movie.StartAirDate = DateTime.Parse(item.Attributes["StartAirDate"].Value);
                movie.EndAirDate = DateTime.Parse(item.Attributes["EndAirDate"].Value);
                if (int.Parse(item.Attributes["Status"].Value) == 1)
                    movie.Status = true;
                else
                    movie.Status = false;
                _lstMovie.Add(movie);
                Parameter.nMovie++;
            }
            DataProvider.Close();
        }

        public void Add(Movie item)
        {
            _lstMovie.Add((Movie)item);

            // save item in file book2.xml
            DataProvider.pathData = "data/Movie.xml";
            DataProvider.Open();

            XmlNode newNode = DataProvider.createNode("Movie");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("Id");
            attr1.Value = item.Id;
            XmlAttribute attr2 = DataProvider.createAttr("Name");
            attr2.Value = item.Name;
            XmlAttribute attr3 = DataProvider.createAttr("Description");
            attr3.Value = item.Description;
            XmlAttribute attr4 = DataProvider.createAttr("Duration");
            attr4.Value = item.Duration.ToString();
            XmlAttribute attr5 = DataProvider.createAttr("StartAirDate");
            attr5.Value = item.StartAirDate.ToString("yyyy-MM-dd HH:mm:ss");
            XmlAttribute attr6 = DataProvider.createAttr("EndAirDate");
            attr6.Value = item.EndAirDate.ToString("yyyy-MM-dd HH:mm:ss");
            XmlAttribute attr7 = DataProvider.createAttr("Status");
            if(item.Status)
                attr7.Value = "1";
            else
                attr7.Value = "0";

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);
            newNode.Attributes.Append(attr6);
            newNode.Attributes.Append(attr7);

            string xPath = string.Format("//Movies");
            XmlNode node = DataProvider.getNode(xPath);
            DataProvider.AppendNode(node, newNode);

            DataProvider.Close();
        }

        public void Delete(Movie item)
        {
            _lstMovie.Remove(item);
            // save item in file book2.xml
            DataProvider.pathData = "data/Movie.xml";
            DataProvider.Open();
            string xPath = string.Format("//Movie[@Id='{0}']", item.Id);
            XmlNode oldNode = DataProvider.getNode(xPath);

            DataProvider.RemoveNode(oldNode);
            DataProvider.Close();
        }

        public Movie Get(string Id)
        {
            foreach (var item in _lstMovie)
            {
                if(Id.ToLower().CompareTo(item.Id.ToLower()) == 0)
                    return item;
            }
            return null;
        }

        public List<Movie> Gets()
        {
            return _lstMovie;
        }


        void UpdateStatus(Movie movie)
        {
            foreach(var item in _lstMovie)
                if(movie.Id.ToLower().CompareTo(item.Id.ToLower()) == 0)
                    item.Status = movie.Status;
        }

        public void Update(Movie item)
        {
            UpdateStatus(item);
            // save item in file book2.xml
            DataProvider.pathData = "data/Movie.xml";
            DataProvider.Open();
            string xPath = string.Format("//Movie[@Id='{0}']", item.Id);
            XmlNode oldNode = DataProvider.getNode(xPath);

            XmlNode newNode = DataProvider.createNode("Movie");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("Id");
            attr1.Value = item.Id;
            XmlAttribute attr2 = DataProvider.createAttr("Name");
            attr2.Value = item.Name;
            XmlAttribute attr3 = DataProvider.createAttr("Description");
            attr3.Value = item.Description;
            XmlAttribute attr4 = DataProvider.createAttr("Duration");
            attr4.Value = item.Duration.ToString();
            XmlAttribute attr5 = DataProvider.createAttr("StartAirDate");
            attr5.Value = item.StartAirDate.ToString("yyyy-MM-dd HH:mm:ss");
            XmlAttribute attr6 = DataProvider.createAttr("EndAirDate");
            attr6.Value = item.EndAirDate.ToString("yyyy-MM-dd HH:mm:ss");
            XmlAttribute attr7 = DataProvider.createAttr("Status");
            if (item.Status)
                attr7.Value = "1";
            else
                attr7.Value = "0";

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);
            newNode.Attributes.Append(attr6);
            newNode.Attributes.Append(attr7);

            DataProvider.InsertNode(newNode, oldNode);
            DataProvider.RemoveNode(oldNode);


            DataProvider.Close();
        }
    }
}

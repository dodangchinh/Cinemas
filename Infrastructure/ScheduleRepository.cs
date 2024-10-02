using Chinh_C1_Cinemas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chinh_C1_Cinemas
{
    public class ScheduleRepository : IRepository<Schedule>
    {
        public List<Schedule> lstSchedule { get; set; }
        private List<Movie> lstMovie { get; set; }
        private List<Cinema> lstCinema {  get; set; }

        public ScheduleRepository(List<Movie> lstMovie, List<Cinema> lstCinema)
        {
            lstSchedule = new List<Schedule>();
            this.lstMovie = lstMovie;
            this.lstCinema = lstCinema;
            Load();
        }

        void Load()
        {
            DataProvider.pathData = "data/Schedule.xml";
            DataProvider.Open();

            string xPath = string.Format("//Schedule");

            XmlNodeList listNode = DataProvider.getDsNode(xPath);

            foreach (XmlNode item in listNode)
            {
                Schedule schedule = new Schedule(getCinema(item.Attributes["IdCinema"].Value).Clone());
                schedule.Id = int.Parse(item.Attributes["Id"].Value);
                schedule.Movie = getMovie(item.Attributes["IdMovie"].Value);
                schedule.AirDate = DateTime.Parse(item.Attributes["AirDate"].Value);
                if (int.Parse(item.Attributes["Status"].Value) == 1)
                    schedule.Status = true;
                else
                    schedule.Status = false;
                Parameter.nSchedule++;
                lstSchedule.Add(schedule);
            }
            DataProvider.Close();
        }

        Cinema getCinema(string id)
        {
            foreach (var item in lstCinema)
                if(item.Id == id)
                    return item;
            return null;
        }
        Movie getMovie(string id)
        {
            foreach (var item in lstMovie)
                if (item.Id == id)
                    return item;
            return null;
        }

        public void Add(Schedule item)
        {
            lstSchedule.Add((Schedule)item);

            // save item in file book2.xml
            DataProvider.pathData = "data/Schedule.xml";
            DataProvider.Open();

            XmlNode newNode = DataProvider.createNode("Schedule");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("Id");
            attr1.Value = item.Id.ToString();
            XmlAttribute attr2 = DataProvider.createAttr("IdMovie");
            attr2.Value = item.Movie.Id;
            XmlAttribute attr3 = DataProvider.createAttr("IdCinema");
            attr3.Value = item.Cinema.Id;
            XmlAttribute attr4 = DataProvider.createAttr("AirDate");
            attr4.Value = item.AirDate.ToString("dd/MM/yyy");
            XmlAttribute attr5 = DataProvider.createAttr("Status");
            if (item.Status)
                attr5.Value = "1";
            else
                attr5.Value = "0";

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);

            string xPath = string.Format("//Schedules");
            XmlNode node = DataProvider.getNode(xPath);
            DataProvider.AppendNode(node, newNode);

            DataProvider.Close();
        }

        public void Delete(Schedule item)
        {
            lstSchedule.Remove(item);
            // save item in file book2.xml
            DataProvider.pathData = "data/Schedule.xml";
            DataProvider.Open();
            string xPath = string.Format("//Schedule[@Id='{0}']", item.Id);
            XmlNode oldNode = DataProvider.getNode(xPath);

            DataProvider.RemoveNode(oldNode);
            DataProvider.Close();
        }

        public Schedule Get(string id)
        {
            int Id;
            foreach(var item in lstSchedule)
                if (int.TryParse(id, out Id))
                    if (item.Id == Id)
                        return item;
            return null;
        }

        public List<Schedule> Gets()
        {
            return lstSchedule;
        }
    
        public void Update(Schedule schedule)
        {
            // save item in file book2.xml
            DataProvider.pathData = "data/Schedule.xml";
            DataProvider.Open();
            string xPath = string.Format("//Schedule[@Id='{0}']", schedule.Id);
            XmlNode oldNode = DataProvider.getNode(xPath);

            XmlNode newNode = DataProvider.createNode("Schedule");
            XmlAttribute attr1 = DataProvider.createAttr("Id");
            attr1.Value = schedule.Id.ToString();
            XmlAttribute attr2 = DataProvider.createAttr("IdMovie");
            attr2.Value = schedule.Movie.Id;
            XmlAttribute attr3 = DataProvider.createAttr("IdCinema");
            attr3.Value = schedule.Cinema.Id;
            XmlAttribute attr4 = DataProvider.createAttr("AirDate");
            attr4.Value = schedule.AirDate.ToString("dd/MM/yyy");
            XmlAttribute attr5 = DataProvider.createAttr("Status");
            if (schedule.Status)
                attr5.Value = "1";
            else
                attr5.Value = "0";

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);

            DataProvider.InsertNode(newNode, oldNode);
            DataProvider.RemoveNode(oldNode);


            DataProvider.Close();
        }
    }
}

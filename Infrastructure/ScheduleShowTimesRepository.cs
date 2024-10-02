using Chinh_C1_Cinemas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chinh_C1_Cinemas
{
    public class ScheduleShowTimesRepository : IRepository<ScheduleShowTimes>
    {
        public List<ScheduleShowTimes> listScheduleShowTimes;
        private List<Schedule> lstSchedule {  get; set; }

        public ScheduleShowTimesRepository(List<Schedule> lstSchedule)
        {
            listScheduleShowTimes = new List<ScheduleShowTimes>();
            this.lstSchedule = lstSchedule;
            Load();
        }

        void Load()
        {
            DataProvider.pathData = "data/ScheduleShowTimes.xml";
            DataProvider.Open();

            string xPath = string.Format("//ScheduleShowTime");

            XmlNodeList listNode = DataProvider.getDsNode(xPath);

            foreach (XmlNode item in listNode)
            {
                ScheduleShowTimes scheduleShowTimes = new ScheduleShowTimes();
                scheduleShowTimes.Id = int.Parse(item.Attributes["Id"].Value);
                scheduleShowTimes.schedule = getSchedule(int.Parse(item.Attributes["IdSchedule"].Value)).Clone();
                scheduleShowTimes.AirDate = DateTime.Parse(item.Attributes["AirDate"].Value);
                if (int.Parse(item.Attributes["Status"].Value) == 1)
                    scheduleShowTimes.Status = true;
                else
                    scheduleShowTimes.Status = false;

                xPath = string.Format("//ScheduleShowTime[@IdSchedule='{0}']/BoughtSeat", scheduleShowTimes.schedule.Id);
                XmlNodeList listNodeSeat = DataProvider.getDsNode(xPath);

                foreach(XmlNode itemSeat in listNodeSeat)
                    scheduleShowTimes.schedule.Cinema.lstSeat[int.Parse(itemSeat.Attributes["SeatNo"].Value) - 1].Status = false;
                Parameter.nScheduleShowTimes++;
                listScheduleShowTimes.Add(scheduleShowTimes);
            }
            DataProvider.Close();
        }

        Schedule getSchedule(int id)
        {
            foreach (var item in lstSchedule)
                if(item.Id == id)
                    return item;
            return null;
        }

        public void Add(ScheduleShowTimes item)
        {
            listScheduleShowTimes.Add(item);

            // save item in file book2.xml
            DataProvider.pathData = "data/ScheduleShowTimes.xml";
            DataProvider.Open();

            XmlNode newNode = DataProvider.createNode("ScheduleShowTime");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("Id");
            attr1.Value = item.Id.ToString();
            XmlAttribute attr2 = DataProvider.createAttr("IdSchedule");
            attr2.Value = item.schedule.Id.ToString();
            XmlAttribute attr3 = DataProvider.createAttr("AirDate");
            attr3.Value = item.AirDate.ToString("yyyy-MM-dd HH:mm:ss");
            XmlAttribute attr4 = DataProvider.createAttr("Status");
            if (item.Status)
                attr4.Value = "1";
            else
                attr4.Value = "0";

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);

            string xPath = string.Format("//ScheduleShowTimes");
            XmlNode node = DataProvider.getNode(xPath);
            DataProvider.AppendNode(node, newNode);

            DataProvider.Close();
        }

        public void Delete(ScheduleShowTimes item)
        {
            listScheduleShowTimes.Remove(item);
            // save item in file book2.xml
            DataProvider.pathData = "data/ScheduleShowTimes.xml";
            DataProvider.Open();
            string xPath = string.Format("//ScheduleShowTime[@Id='{0}']", item.Id);
            XmlNode oldNode = DataProvider.getNode(xPath);

            DataProvider.RemoveNode(oldNode);
            DataProvider.Close();
        }

        public ScheduleShowTimes Get(string Id)
        {
            foreach (var item in listScheduleShowTimes)
                if(item.Id == int.Parse(Id))
                    return item;
            return null;
        }

        public List<ScheduleShowTimes> Gets()
        {
            return listScheduleShowTimes;
        }

        public void Update(ScheduleShowTimes item)
        {
            // save item in file book2.xml
            DataProvider.pathData = "data/ScheduleShowTimes.xml";
            DataProvider.Open();
            string xPath = string.Format("//ScheduleShowTime[@Id='{0}']", item.Id);
            XmlNode oldNode = DataProvider.getNode(xPath);

            XmlNode newNode = DataProvider.createNode("ScheduleShowTime");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("Id");
            attr1.Value = item.Id.ToString();
            XmlAttribute attr2 = DataProvider.createAttr("IdSchedule");
            attr2.Value = item.schedule.Id.ToString();
            XmlAttribute attr3 = DataProvider.createAttr("AirDate");
            attr3.Value = item.AirDate.ToString("yyyy-MM-dd HH:mm:ss");
            XmlAttribute attr4 = DataProvider.createAttr("Status");
            if (item.Status)
                attr4.Value = "1";
            else
                attr4.Value = "0";

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);

            DataProvider.InsertNode(newNode, oldNode);
            DataProvider.RemoveNode(oldNode);
          
            XmlNode newNodeSeat = null;
            foreach (var tempSeat in item.schedule.Cinema.lstSeat)
            {
                if(tempSeat.Status == false)
                {
                    newNodeSeat = DataProvider.createNode("BoughtSeat");
                    XmlAttribute attrSeat = DataProvider.createAttr("SeatNo");
                    attrSeat.Value = tempSeat.No.ToString();
                    newNodeSeat.Attributes.Append(attrSeat);
                    if (newNodeSeat != null)
                    {
                        xPath = string.Format("//ScheduleShowTime[@IdSchedule='{0}']", item.schedule.Id);
                        XmlNode node = DataProvider.getNode(xPath);
                        DataProvider.AppendNode(node, newNodeSeat);
                    }
                }                   
            }

            
            
            DataProvider.Close();
        }
    }
}

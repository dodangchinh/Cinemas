using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chinh_C1_Cinemas
{
    public class OrdersRepository : IRepository<Orders>
    {
        public List<string> lstAge {  get; set; }   
        public List<Orders> lstOrder { get; set; }
        private List<ScheduleShowTimes> lstScheduleShowTimes { get; set; }

        public OrdersRepository(List<ScheduleShowTimes> lstScheduleShowTimes) 
        {
            lstOrder = new List<Orders>();
            this.lstScheduleShowTimes = lstScheduleShowTimes;
            this.lstAge = GetListAge();
            Load();
        }

        void Load()
        {
            DataProvider.pathData = "data/Orders.xml";
            DataProvider.Open();

            string xPath = string.Format("//Order");

            XmlNodeList listNode = DataProvider.getDsNode(xPath);

            foreach (XmlNode item in listNode)
            {
                
                string Name = item.Attributes["CustomerName"].Value;
                string Phone = item.Attributes["PhoneNumber"].Value;

                ScheduleShowTimes scheduleShowTimes = new ScheduleShowTimes();
                scheduleShowTimes = GetScheduleShowTimes(int.Parse(item.Attributes["IdScheduleShowTime"].Value)).Clone();

                Orders orders = new Orders(++Parameter.nOrders, Name, Phone, scheduleShowTimes, DateTime.Parse(item.Attributes["Date"].Value));

                xPath = string.Format("//Order[@Id='{0}']/DetailOrder", orders.Id);
                XmlNodeList listNodeDetailOrder = DataProvider.getDsNode(xPath);

                foreach (XmlNode itemDetailOrder in listNodeDetailOrder)
                {
                    int No = int.Parse(itemDetailOrder.Attributes["SeatNo"].Value);
                    string Age = itemDetailOrder.Attributes["Age"].Value;
                    DetailOrder detailOrder = new DetailOrder(Age, No, scheduleShowTimes.schedule);

                    orders.lstDetailOrder.Add(detailOrder);
                }
                orders.getTotal();
                lstOrder.Add(orders);
            }
            DataProvider.Close();
        }

        List<string> GetListAge()
        {
            List<string> lstAge = new List<string>();

            lstAge.Add("Adult");
            lstAge.Add("Child");
            return lstAge;
        }

        ScheduleShowTimes GetScheduleShowTimes(int Id)

        {
            foreach (var item in lstScheduleShowTimes)
                if(item.Id == Id)
                    return item;
            return null;
        }

        public void Add(Orders item)
        {
            lstOrder.Add(item);

            // save item in file book2.xml
            DataProvider.pathData = "data/Orders.xml";
            DataProvider.Open();

            XmlNode newNode = DataProvider.createNode("Order");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("Id");
            attr1.Value = item.Id.ToString();
            XmlAttribute attr2 = DataProvider.createAttr("IdScheduleShowTime");
            attr2.Value = item.scheduleShowTimes.Id.ToString();
            XmlAttribute attr3 = DataProvider.createAttr("CinemaType");
            attr3.Value = item.CinemaType;
            XmlAttribute attr4 = DataProvider.createAttr("CustomerName");
            attr4.Value = item.Name;
            XmlAttribute attr5 = DataProvider.createAttr("PhoneNumber");
            attr5.Value = item.PhoneNumber;
            XmlAttribute attr6 = DataProvider.createAttr("Date");
            attr6.Value = item.Date.ToString("yyyy-MM-dd HH:mm:ss");
            XmlAttribute attr7 = DataProvider.createAttr("Total");
            attr7.Value = item.Total.ToString();

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);
            newNode.Attributes.Append(attr6);
            newNode.Attributes.Append(attr7);

            string xPath = string.Format("//Orders");
            XmlNode node = DataProvider.getNode(xPath);
            DataProvider.AppendNode(node, newNode);

            foreach (var tempDetailOrder in item.lstDetailOrder)
            {
                XmlNode newNode2 = DataProvider.createNode("DetailOrder");
                // XmlNode newNode = doc.CreateElement("Book");
                XmlAttribute attr8 = DataProvider.createAttr("Age");
                attr8.Value = tempDetailOrder.Age;
                XmlAttribute attr9 = DataProvider.createAttr("SeatNo");
                attr9.Value = tempDetailOrder.SeatNo.ToString();
                XmlAttribute attr10 = DataProvider.createAttr("Price");
                attr10.Value = tempDetailOrder.Price.ToString();
                XmlAttribute attr11 = DataProvider.createAttr("Discount");
                attr11.Value = tempDetailOrder.Discount.ToString();

                newNode2.Attributes.Append(attr8);
                newNode2.Attributes.Append(attr9);
                newNode2.Attributes.Append(attr10);
                newNode2.Attributes.Append(attr11);

                xPath = string.Format("//Order[@Id='{0}']", item.Id);
                XmlNode node1 = DataProvider.getNode(xPath);
                DataProvider.AppendNode(node1, newNode2);
            }

            DataProvider.Close();
        }

        public void Delete(Orders item)
        {
            throw new NotImplementedException();
        }

        public Orders Get(string Id)
        {
            throw new NotImplementedException();
        }

        public List<Orders> Gets()
        {
            return lstOrder;
        }

        public void Update(Orders item)
        {
            // save item in file book2.xml
            DataProvider.pathData = "data/Orders.xml";
            DataProvider.Open();
            string xPath = string.Format("//Order[@Id='{0}']", item.Id);
            XmlNode oldNode = DataProvider.getNode(xPath);

            XmlNode newNode = DataProvider.createNode("Order");
            // XmlNode newNode = doc.CreateElement("Book");
            XmlAttribute attr1 = DataProvider.createAttr("Id");
            attr1.Value = item.Id.ToString();
            XmlAttribute attr2 = DataProvider.createAttr("IdScheduleShowTime");
            attr2.Value = item.scheduleShowTimes.Id.ToString();
            XmlAttribute attr3 = DataProvider.createAttr("CinemaType");
            attr3.Value = item.CinemaType;
            XmlAttribute attr4 = DataProvider.createAttr("CustomerName");
            attr4.Value = item.Name;
            XmlAttribute attr5 = DataProvider.createAttr("PhoneNumber");
            attr5.Value = item.PhoneNumber;
            XmlAttribute attr6 = DataProvider.createAttr("Date");
            attr6.Value = item.Date.ToString("yyyy-MM-dd HH:mm:ss");
            XmlAttribute attr7 = DataProvider.createAttr("Total");
            attr7.Value = item.Total.ToString();

            newNode.Attributes.Append(attr1);
            newNode.Attributes.Append(attr2);
            newNode.Attributes.Append(attr3);
            newNode.Attributes.Append(attr4);
            newNode.Attributes.Append(attr5);
            newNode.Attributes.Append(attr6);
            newNode.Attributes.Append(attr7);

            xPath = string.Format("//Orders");
            XmlNode node = DataProvider.getNode(xPath);
            DataProvider.AppendNode(node, newNode);

            foreach (var tempDetailOrder in item.lstDetailOrder)
            {
                XmlNode newNode2 = DataProvider.createNode("DetailOrder");
                // XmlNode newNode = doc.CreateElement("Book");
                XmlAttribute attr8 = DataProvider.createAttr("Age");
                attr8.Value = tempDetailOrder.Age;
                XmlAttribute attr9 = DataProvider.createAttr("SeatNo");
                attr9.Value = tempDetailOrder.SeatNo.ToString();
                XmlAttribute attr10 = DataProvider.createAttr("Price");
                attr10.Value = tempDetailOrder.Price.ToString();
                XmlAttribute attr11 = DataProvider.createAttr("Discount");
                attr11.Value = tempDetailOrder.Discount.ToString();

                newNode2.Attributes.Append(attr8);
                newNode2.Attributes.Append(attr9);
                newNode2.Attributes.Append(attr10);
                newNode2.Attributes.Append(attr11);

                xPath = string.Format("//Order[@Id='{0}']", item.Id);
                XmlNode node1 = DataProvider.getNode(xPath);
                DataProvider.AppendNode(node1, newNode2);
            }

            DataProvider.InsertNode(newNode, oldNode);
            DataProvider.RemoveNode(oldNode);

            DataProvider.Close();
        }
    }
}

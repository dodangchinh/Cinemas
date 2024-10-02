using Chinh_C1_Cinemas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chinh_C1_Cinemas
{
    public class CinemaRepository
    {
        public List<Cinema> lstCinema {  get; set; }

        public CinemaRepository()
        {
            lstCinema = new List<Cinema>();

            DataProvider.pathData = "data/Cinemas.xml";
            DataProvider.Open();

            string xPath = string.Format("//Cinema");

            XmlNodeList listNode = DataProvider.getDsNode(xPath);

            Cinema cinema;
            Seat seat;
            foreach (XmlNode itemCinema in listNode)
            {
                cinema = getDataType(itemCinema.Attributes["Type"].Value).Clone();
                cinema.Id = itemCinema.Attributes["IdCinema"].Value;
                cinema.Name = itemCinema.Attributes["Name"].Value;
                cinema.ChangePrice();
                cinema.ChangeDiscount();
                xPath = string.Format("//Cinema[@IdCinema='{0}']/Seat", cinema.Id);
                XmlNodeList listNodeCinema = DataProvider.getDsNode(xPath);

                cinema.lstSeat = new List<Seat>();
                foreach (XmlNode itemSeat in listNodeCinema)
                {                    
                    seat = new Seat();
                    seat.Id = itemSeat.Attributes["Id"].Value;
                    seat.Name = itemSeat.Attributes["Name"].Value;
                    if (int.Parse(itemSeat.Attributes["Status"].Value) == 1)
                        seat.Status = true;
                    else
                        seat.Status = false;
                    seat.No = ++Parameter.nSeatNo;
                    cinema.lstSeat.Add(seat);                   
                }
                Parameter.nSeatNo = 0;
                lstCinema.Add(cinema);
            }          
        }

        public Cinema getDataType(string type)
        {
            switch (type)
            {
                case "Standard":
                    return new CinemaStandard();
                case "Vip":
                    return new CinemaVip();
            }
            return null;
        }

        public List<Cinema> Gets()
        {
            return lstCinema;
        }
    }
}

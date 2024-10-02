using Chinh_C1_Cinemas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_C1_Cinemas
{
    public class Seat
    {
        public int No {  get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status = true;

        public Seat(int No, string Id, string Name, bool Status)
        {
            this.No = No;
            this.Id = Id;
            this.Name = Name;
            this.Status = Status;
        }

        public Seat(int No, string Id, string Name) 
        {
            this.No = No;
            this.Id = Id;
            this.Name = Name;
        }

        public Seat() { }

        public Seat Clone()
        {
            return new Seat(this.No, this.Id, this.Name, this.Status);
        }
        
    }
}

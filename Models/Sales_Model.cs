using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment4.Models
{
    public class Sales_Model
    {

        [Table("Sales")]
        public class Sales
        {
            [Key]
            public int sales_id { get; set; }
            public decimal sales_amt { get; set; }
            public decimal commission { get; set; }
            [ForeignKey("Vehicle")]
            public int vin_number { get; set; }
            [ForeignKey("Customer")]
            public int customer_id { get; set; }
            [ForeignKey("Salesperson")]
            public int salesperson_id { get; set; }
        }

        [Table("Salesperson")]
        public class Salesperson
        {
            [Key]
            public int salesperson_id { get; set; }
            public string name { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
        }

        [Table("Vehicle")]
        public class Vehicle
        {
            [Key]
            public int vin_number { get; set; }
            public string make { get; set; }
            public string model_name { get; set; }
            public string model_year { get; set; }
            public string color { get; set; }
        }

        [Table("Customer")]
        public class Customer
        {
            [Key]
            public int customer_id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
        }

        public class ChartTables
        {
            public decimal sales_amt { get; set; }
            public string model_year { get; set; }
        }

    }
}

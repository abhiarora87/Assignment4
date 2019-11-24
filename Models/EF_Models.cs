using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment4.Models
{
    public class EF_Models
    {
        public class Automobile
        {
            public int id { get; set; }
            public string model { get; set; }
            public string model_year { get; set; }
            public string engine_size { get; set; }
            public int engine_cylinder_count { get; set; }
            public string manufacturer_name { get; set; }
            public string category_name { get; set; }
        }

        //public class Automobiles
        //{
        //    public string metadata { get; set; }
        //    public string inputs { get; set; }
        //    public string status { get; set; }
        //    public List<Automobile> result { get; set; }

        //}


        public class Resultset
        {
            public int count { get; set; }
        }

        public class Metadata
        {
            public string version { get; set; }
            public Resultset resultset { get; set; }
        }

        public class Inputs
        {
            public string current { get; set; }
            public string category_id { get; set; }
            public string fuel_id { get; set; }
        }

        public class Result
        {
            public int id { get; set; }
            public int fuel_id { get; set; }
            public string phev_type { get; set; }
            public int light_duty_fuel_configuration_id { get; set; }
            public int light_duty_manufacturer_id { get; set; }
            public int light_duty_category_id { get; set; }
            public string model { get; set; }
            public string model_year { get; set; }
            public string photo_url { get; set; }
            public int? electric_range { get; set; }
            public string transmission_type { get; set; }
            public string engine_type { get; set; }
            public string engine_size { get; set; }
            public int engine_cylinder_count { get; set; }
            public string engine_description { get; set; }
            public string notes { get; set; }
            public string manufacturer_name { get; set; }
            public string manufacturer_url { get; set; }
            public string fuel_code { get; set; }
            public string fuel_name { get; set; }
            public string light_duty_fuel_configuration_name { get; set; }
            public string category_name { get; set; }
            public string alternative_fuel_economy_combined { get; set; }
            public string conventional_fuel_economy_combined { get; set; }
            public string alternative_fuel_economy_city { get; set; }
            [NotMapped]
            public object alternative_fuel_economy_highway { get; set; }
            public string conventional_fuel_economy_city { get; set; }
            public string conventional_fuel_economy_highway { get; set; }
            [NotMapped]
            public List<object> light_duty_emission_certifications { get; set; }
        }

        public class RootObject
        {
            public Metadata metadata { get; set; }
            public Inputs inputs { get; set; }
            public int status { get; set; }
            public List<Result> result { get; set; }
        }

    }
}



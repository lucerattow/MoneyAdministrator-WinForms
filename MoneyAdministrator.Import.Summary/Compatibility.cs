using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.Import.Summary
{
    public static class Compatibility
    {
        public static List<Bank> Banks;

        public static readonly Bank HSBC;
        public static readonly Bank Supervielle;

        static Compatibility()
        {
            HSBC = new Bank()
            {
                Name = "hsbc",
                Brands = new string[] { Brand.Mastercard }
            };
            Supervielle = new Bank()
            {
                Name = "supervielle",
                Brands = new string[] { Brand.Mastercard, Brand.Visa }
            };
            Banks = new List<Bank>()
            {
                HSBC,
                Supervielle,
            };
        }
    }

    public class Bank
    {
        public string Name { get; set; }
        public string[] Brands { get; set; }
    }

    public static class Brand
    {
        public static string Visa = "visa";
        public static string Mastercard = "mastercard";
    }
}

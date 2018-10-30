using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionnaireClient.Model
{
    public class Sale
    {
        public Sale()
        {
            this.Products = new HashSet<Product>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        private DateTime datePeriode = DateTime.Now;
        public DateTime DatePeriode
        {
            get { return datePeriode; }
            set { datePeriode = value; }
        }

        public virtual ICollection<Product> Products { get; set; }
    }
}

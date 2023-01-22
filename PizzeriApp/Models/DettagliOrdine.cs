namespace PizzeriApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DettagliOrdine")]
    public partial class DettagliOrdine
    {
        [Key]
        public int IDdettagliOrdine { get; set; }

        public int? Quantita { get; set; }

        public int? IdPizza { get; set; }

        public int? IdOrdini { get; set; }

        public int? IdUtente { get; set; }

        [NotMapped]
        public string PizzaNome { get; set; }

        [NotMapped]
        public string NomeUtente { get; set; }

        public virtual Ordini Ordini { get; set; }

        public virtual Pizza Pizza { get; set; }

        public virtual Utente Utente { get; set; }

        public static List<DettagliOrdine> Carrello = new List<DettagliOrdine>();
    }
}

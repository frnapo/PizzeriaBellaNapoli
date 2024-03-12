namespace BellaNapoli.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Utenti")]
    public partial class Utenti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Utenti()
        {
            Ordini = new HashSet<Ordini>();
        }

        [Key]
        public int idUtente { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Cognome { get; set; }

        [Display(Name = "E-mail")]
        [Required]
        [StringLength(200)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Psw { get; set; }

        public bool isAdmin { get; set; }

        [Required]
        [StringLength(5)]
        public string CAP { get; set; }

        [Display(Name = "Citt�")]
        [Required]
        [StringLength(200)]
        public string Citta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ordini> Ordini { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FlowWebApp.Models
{
    public class Report
    {
        public int Id { get; set; }
        public virtual ICollection<Payment> Payments { get; internal set; } = new List<Payment>();

        [NotMapped]
        public virtual IFormFile UploadFile { get; set; }
        [AllowNull, NotMapped]
        public virtual IFormFile DownloadFile { get; set; }
        [AllowNull]
        public virtual IdentityUser ApplicationUser { get; set; }
        public bool Active() => Payments is not null && Payments.Any() && !Payments.Any(p => p.OverDue());
    }
}

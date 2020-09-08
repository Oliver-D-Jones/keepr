using System.ComponentModel.DataAnnotations;

namespace Keepr.Models
{
    public class RefVaultKeep
    {
        public int Id { get; set; }

        [Required]
        public int KeepId { get; set; }

        [Required]
        public int VaultId { get; set; }
        public string UserId { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBMapping.Model
{
    public class Department
    {
       [ForeignKey("Employee")]
        [Key]
        public Guid DaprtmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentLoc { get; set; }
        public virtual Employee Employee { get; set; }
    }
}

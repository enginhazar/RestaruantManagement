using System.ComponentModel.DataAnnotations.Schema;

namespace Rm.Core.Entity
{
    
    public class Table : EntityBase
    {
        public string TableName { get; set; }
        public int Capacity { get; set; }
    }
}

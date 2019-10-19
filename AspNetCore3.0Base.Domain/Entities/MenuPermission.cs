using System.ComponentModel.DataAnnotations;

namespace AspNetCore3._0Base.Domain.Entities
{
    public class MenuPermission
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Icon { get; set; }
        public int? SubMenuId { get; set; }
        public bool ShowMenu { get; set; }
        public string RoleName { get; set; }
        public int Side { get; set; }
        public int Subside { get; set; }
        public string Class { get; set; }
        public string IconClass { get; set; }
 
        

    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReqManager.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("REQUIREMENT_TYPE", Schema = "REQ")]
    public class RequirementType
    {
        public RequirementType()
        {
            this.Requirement = new HashSet<Requirement>();
            this.RequirimentTemplate = new HashSet<RequirementTemplate>();
            this.RequirementSubType = new HashSet<RequirementSubType>();
        }
    
        [Key]
        public int RequirementTypeID { get; set; }
        [MaxLength(4)]
        [Index(IsUnique = true)]
        public string abbreviation { get; set; }
        [MinLength(5)]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string description { get; set; }
    
        public virtual ICollection<Requirement> Requirement { get; set; }
        public virtual ICollection<RequirementSubType> RequirementSubType { get; set; }
        public virtual ICollection<RequirementTemplate> RequirimentTemplate { get; set; }
    }
}

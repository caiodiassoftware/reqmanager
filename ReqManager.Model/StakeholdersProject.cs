namespace ReqManager.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("STAKEHOLDERS_PROJECT", Schema = "PROJ")]
    public class StakeholdersProject
    {
        public StakeholdersProject()
        {
            this.StakeholderRequirement = new HashSet<StakeholderRequirement>();
        }
    
        [Key]
        public int StakeholdersProjectID { get; set; }
        [Index("IX_STAKEHOLDERS_PROJECT", 1, IsUnique = true)]
        public int ProjectID { get; set; }
        [Index("IX_STAKEHOLDERS_PROJECT", 2, IsUnique = true)]
        public int StakeholderID { get; set; }
        public DateTime creationDate { get; set; }
        [Required]
        [MaxLength(255)]
        public string description { get; set; }
        [Required]
        [Range(1, 9)]
        public int importanceValue { get; set; }

        public virtual Project Project { get; set; }
        public virtual Stakeholders Stakeholders { get; set; }
        public virtual ICollection<StakeholderRequirement> StakeholderRequirement { get; set; }
    }
}

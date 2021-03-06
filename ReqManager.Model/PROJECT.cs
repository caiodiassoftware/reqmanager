using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("PROJECT", Schema = "PROJ")]
    public class Project
    {
        public Project()
        {
            this.ProjectArtifact = new HashSet<ProjectArtifact>();
            this.HistoryProject = new HashSet<HistoryProject>();
            this.StakeholderProject = new HashSet<StakeholdersProject>();
        }
    
        [Key]
        public int ProjectID { get; set; }
        public int CreationUserID { get; set; }
        public int ProjectPhasesID { get; set; }
        [Required]
        [MaxLength(255)]
        public string description { get; set; }
        [Required]
        [MaxLength(300)]
        public string pathForTraceability { get; set; }
        [Required]
        [MaxLength(1000)]
        public string environmentalInformation { get; set; }
        [Required]
        [MaxLength(1000)]
        public string managementInformation { get; set; }
        [Required]
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
        [Required]
        public System.DateTime creationDate { get; set; }
        [MaxLength(25)]
        [Index(IsUnique = true)]
        public string code { get; set; }
        [Required]
        [Range(0, 9999999999999999.99)]
        public decimal cost { get; set; }

        [ForeignKey("CreationUserID")]
        public virtual Users Users { get; set; }
        public virtual ICollection<ProjectArtifact> ProjectArtifact { get; set; }
        public virtual ICollection<HistoryProject> HistoryProject { get; set; }
        public virtual ProjectPhases ProjectPhases { get; set; }
        public virtual ICollection<StakeholdersProject> StakeholderProject { get; set; }
        public virtual ICollection<Requirement> RequirementCharacteristics { get; set; }
    }
}

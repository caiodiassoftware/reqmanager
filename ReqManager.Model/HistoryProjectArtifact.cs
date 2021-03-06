using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("HISTORY_PROJECT_ARTIFACT", Schema = "ART")]
    public class HistoryProjectArtifact
    {
        [Key]
        public int HistoryArtefactID { get; set; }
        public int ProjectArtifactID { get; set; }
        [Required]
        [MaxLength(50)]
        public string descriptionTypeArtifact { get; set; }
        [Required]
        [MaxLength(50)]
        public string descriptionImportance { get; set; }
        [Required]
        [MaxLength(300)]
        public string path { get; set; }
        [Required]
        [MaxLength(500)]
        public string description { get; set; }
        public System.DateTime creationDate { get; set; }
        [Required]
        [MaxLength(25), MinLength(5)]
        public string login { get; set; }
    
        public virtual ProjectArtifact ProjectArtifact { get; set; }
    }
}

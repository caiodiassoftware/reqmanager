using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("LINK_BETWEEN_REQUIREMENTS_ARTIFACTS", Schema = "LINK")]
    public class LinkBetweenRequirementsArtifacts
    {
        public LinkBetweenRequirementsArtifacts()
        {
            this.LinkArtifactAttributes = new HashSet<LinkArtifactAttributes>();
        }
    
        [Key]
        public int LinkArtifactRequirementID { get; set; }
        [Required]
        public DateTime creationDate { get; set; }
        [Required]
        [MaxLength(25)]
        public string code { get; set; }
    
        public virtual Users Users { get; set; }
        public virtual ProjectArtifact ProjectArtifact { get; set; }
        public virtual Requirement Requirement { get; set; }
        public virtual ICollection<LinkArtifactAttributes> LinkArtifactAttributes { get; set; }        
        public virtual TypeLink TypeLink { get; set; }
    }
}

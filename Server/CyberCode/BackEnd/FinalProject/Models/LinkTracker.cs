using FinalProject.Models.BaseClass;

namespace FinalProject.Models
{
    public class LinkTracker:BaseEntity
    {
        
        public DateTime ClickedAt { get; set;}
        public bool isClicked { get; set; }
        public string userEmail { get; set; }


    }
}

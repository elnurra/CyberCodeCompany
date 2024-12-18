namespace FinalProject.Areas.AdminArea.ViewModels.PhishingCRUD 
{ 
    public class PhisingCreateVM
    {
        public string? Link { get; set; }
        public string userEmail { get; set; }
        public DateTime ClickedAt { get; set; }
        public bool isClicked { get; set; }
    }
}

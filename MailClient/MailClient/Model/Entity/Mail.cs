namespace MailClient.Model.Entity
{
    public class Mail 
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public AttachmentDTO Attachment1 { get; set; } = new AttachmentDTO();
        public AttachmentDTO Attachment2 { get; set; } = new AttachmentDTO();
        public AttachmentDTO Attachment3 { get; set; } = new AttachmentDTO();
    }
}

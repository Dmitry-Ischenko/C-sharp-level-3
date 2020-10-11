using MailClient.lib.Models.Base;

namespace MailClient.lib.Models
{
    public class Message: ModelBase
    {
        private string _Subject;
        private string _Body;

        public string Subject { 
            get=> _Subject; 
            set => Set(ref _Subject, value); 
        }

        public string Body { 
            get=> _Body; 
            set=> Set(ref _Body, value); 
        }
    }
}

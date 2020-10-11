using MailClient.lib.Models.Base;

namespace MailClient.lib.Models
{
    public class Recipient: ModelBase
    {
        private string _Name;
        private string _Address;
        private bool _Active;
        
        public string Name { 
            get=>_Name; 
            set=> Set(ref _Name, value); 
        }

        public string Address { 
            get=> _Address; 
            set=> Set(ref _Address, value); 
        }

        public bool Active { 
            get=>_Active; 
            set=>Set(ref _Active,value); 
        }
    }
}

using MailSender.Models.Base;

namespace MailSender.Models
{
    public class Sender: ModelBase
    {
        private string _name;
        private string _address;
        private string _password;
        private string _server;
        private int _port;
        private bool _usessl;  
        
        
        public string Name { 
            get=> _name; 
            set=> Set(ref _name, value); 
        }

        public string Address { 
            get=> _address; 
            set=> Set(ref _address, value); 
        }

        public string Password { 
            get=> _password; 
            set=> Set(ref _password, value); 
        }

        public string Server { 
            get=> _server; 
            set=> Set(ref _server, value); 
        }

        public int Port { 
            get=> _port; 
            set=> Set(ref _port, value); 
        }
        
        public bool UseSSl { 
            get=> _usessl; 
            set=> Set(ref _usessl, value); 
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Base.DataContractCore.Authentication

{
    public class LoginRequest
    {
        [Required]
        public string USER_NAME { set; get; }
        
        [Required]
        public string PASSWORD { set; get; }

    }
}

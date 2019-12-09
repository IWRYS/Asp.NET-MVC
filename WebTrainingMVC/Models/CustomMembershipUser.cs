using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace WebTrainingMVC.Models
{
    public class CustomMembershipUser : MembershipUser
    {
        public UserViewModel Data { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Users.Commands.LoginUser
{
   public class LoginUserCommand : IRequest<string>
    {


        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}


using System;
using BigProjectOne.Libraries.Models.Interfaces;

namespace BigProjectOne.Libraries.LibrairiesUnitTest
{
    public class SimpleUser : ISimpleUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
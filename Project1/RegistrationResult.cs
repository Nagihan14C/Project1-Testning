using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistration;

public class RegistrationResult
    {
    public bool Success { get; }
    public string Username { get; }
    public string Message { get; }

    public RegistrationResult(bool success, string username, string message)
    {
        Success = success;
        Username = username;
        Message = message;
    }
}


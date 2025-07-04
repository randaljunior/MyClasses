using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClasses.Options;

public interface IError
{
    string? Message { get; init; }

    string? ToString()
    {
        return Message;
    }
}

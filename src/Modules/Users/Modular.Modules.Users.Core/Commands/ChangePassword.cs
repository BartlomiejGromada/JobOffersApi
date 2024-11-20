using Modular.Abstractions.Commands;
using System;

namespace Modular.Modules.Users.Core.Commands
{
    internal record ChangePassword(Guid UserId, string CurrentPassword, string NewPassword) : ICommand;
}
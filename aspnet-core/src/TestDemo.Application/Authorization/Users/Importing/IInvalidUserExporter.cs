using System.Collections.Generic;
using TestDemo.Authorization.Users.Importing.Dto;
using TestDemo.Dto;

namespace TestDemo.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}

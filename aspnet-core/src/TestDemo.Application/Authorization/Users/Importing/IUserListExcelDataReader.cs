using System.Collections.Generic;
using TestDemo.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace TestDemo.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}

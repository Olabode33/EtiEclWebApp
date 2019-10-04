using System.Collections.Generic;
using TestDemo.Authorization.Users.Dto;
using TestDemo.Dto;

namespace TestDemo.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}
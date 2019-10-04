using System.Collections.Generic;
using TestDemo.Chat.Dto;
using TestDemo.Dto;

namespace TestDemo.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(List<ChatMessageExportDto> messages);
    }
}

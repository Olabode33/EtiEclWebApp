using Abp.AspNetZeroCore.Net;
using Abp.Dependency;
using DinkToPdf;
using DinkToPdf.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestDemo.Auditing.Dto;
using TestDemo.Dto;
using TestDemo.Storage;

namespace TestDemo.Auditing.Exporting
{
    public class AuditLogListPdfExporter : ITransientDependency, IAuditLogListPdfExporter
    {
        private readonly IConverter _converter;
        private readonly ITempFileCacheManager _tempFileCacheManager;

        public AuditLogListPdfExporter(IConverter converter, ITempFileCacheManager tempFileCacheManager)
        {
            _converter = converter;
            _tempFileCacheManager = tempFileCacheManager;
        }

        public FileDto ExportToPdf(List<PrintAuditLogDto> input)
        {
            var html = ConvertAuditLogListToHtmlTable(input);
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Landscape
                },
                Objects = {
                    new ObjectSettings()
                    {
                        HtmlContent = html
                    }
                }
            };

            var fileDto = new FileDto("AuditLog.pdf", MimeTypeNames.ApplicationPdf);
            SavePdf(_converter.Convert(doc), fileDto);

            return fileDto;
        }

        private string ConvertAuditLogListToHtmlTable(List<PrintAuditLogDto> logs)
        {
            var headerId = "<th>Id</th>";
            var headerPropertyName = "<th>Column Name</th>";
            var headerOriginalValue = "<th>Original Value</th>";
            var headerNewValue = "<th>New Value</th>";
            var headerCreationTime = "<th>Action Date</th>";
            var headerChangeType = "<th>Action Taken</th>";
            var headerEntityId = "<th>Entity Id</th>";
            var headerEntityType = "<th>Entity</th>";
            var headerBrowserInfo = "<th>Browser Info</th>";
            var headerClientIpAddress = "<th>Client IP Address</th>";
            //var headerClientName = "<th>Client Name</th>";
            var headerUserId = "<th>Action User Id</th>";
            var headerUserName = "<th>Action User Name</th>";
            var headerImpersonatorUserId = "<th>Impersonator User Id</th>";
            var headerImpersonatorUser = "<th>Impersonator User</th>";

            var headers = $"<tr>{headerId}{headerChangeType}{headerCreationTime}{headerUserId}{headerUserName}{headerEntityType}{headerEntityId}{headerPropertyName}{headerOriginalValue}{headerNewValue}{headerClientIpAddress}{headerBrowserInfo}{headerImpersonatorUserId}{headerImpersonatorUser}</tr>";
            var rows = new StringBuilder();
            foreach (var log in logs)
            {
                var id = $"<td>{log.Id}</td>";
                var changeType = $"<td>{log.ChangeType}</td>";
                var creationTime = $"<td>{log.CreationTime}</td>";
                var userId = $"<td>{log.UserId}</td>";
                var userName = $"<td>{log.UserName}</td>";
                var entityType = $"<td>{log.EntityTypeFullName}</td>";
                var entityId = $"<td>{log.EntityId}</td>";
                var propertyName = $"<td>{log.PropertyName}</td>";
                var originalValue = $"<td>{log.OriginalValue}</td>";
                var newValue = $"<td>{log.NewValue}</td>";
                var ipAddress = $"<td>{log.ClientIpAddress}</td>";
                var browserInfo = $"<td>{log.BrowserInfo}</td>";
                var impersonatorUserId = $"<td>{log.ImpersonatorUserId}</td>";
                var impersonatorUser = $"<td>{log.ImpersonatorUser}</td>";


                var row = $"<tr>{id}{changeType}{creationTime}{userId}{userName}{entityType}{entityId}{propertyName}{originalValue}{newValue}{ipAddress}{browserInfo}{impersonatorUserId}{impersonatorUser}</tr>";
                rows.Append(row);
            }
            return $"<table>{headers}{rows.ToString()}</table>";
        }

        protected void SavePdf(byte[] bytePackage, FileDto file)
        {
            _tempFileCacheManager.SetFile(file.FileToken, bytePackage);
        }
    }
}


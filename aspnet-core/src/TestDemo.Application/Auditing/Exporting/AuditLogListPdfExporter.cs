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
                    Orientation = Orientation.Landscape,
                    DocumentTitle = "Audit Log List as at " + DateTime.Now.ToString()
                },
                Objects = {
                    new ObjectSettings()
                    {
                        HtmlContent = html,
                        PagesCount = true,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                    }
                }
            };

            var fileDto = new FileDto("AuditLog-" + DateTime.Now.ToString() + ".pdf", MimeTypeNames.ApplicationPdf);
            SavePdf(_converter.Convert(doc), fileDto);

            return fileDto;
        }

        private string ConvertAuditLogListToHtmlTable(List<PrintAuditLogDto> logs)
        {
            var headerId = "<th>Id</th>";
            var headerPropertyName = "<th>COLUMN_NAME</th>";
            var headerOriginalValue = "<th>OLD_VALUE</th>";
            var headerNewValue = "<th>NEW_VALUE</th>";
            var headerCreationTime = "<th>MAKER_DATE TIME</th>";
            var headerChangeType = "<th>ACTIVITY PERFORMED</th>";
            var headerEntityId = "<th>ENTITY_ID</th>";
            var headerEntityType = "<th>ENTITY</th>";
            var headerBrowserInfo = "<th>MAKER_BROWSER_INFO</th>";
            var headerClientIpAddress = "<th>MAKER_IP ADDRESS</th>";
            //var headerClientName = "<th>Client Name</th>";
            var headerUserId = "<th>MAKER_ID</th>";
            var headerUserName = "<th>MAKER_NAME</th>";
            var headerImpersonatorUserId = "<th>Impersonator User Id</th>";
            var headerImpersonatorUser = "<th>Impersonator User</th>";
            var headerCheckerId = "<th>CHECKER_ID</th>";
            var headerCheckerName = "<th>CHECKER_NAME</th>";
            var headerCheckerDate = "<th>CHECKER_DATE TIME</th>";
            var headerCheckerIp = "<th>CHECKER_IP ADDRESS</th>";

            var headers = $"<tr>{headerId}{headerUserId}{headerUserName}{headerCreationTime}{headerClientIpAddress}{headerChangeType}{headerEntityType}{headerEntityId}{headerPropertyName}{headerOriginalValue}{headerNewValue}{headerCheckerId}{headerCheckerName}{headerCheckerDate}{headerCheckerIp}</tr>";
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
                var checkerId = $"<td>{log.CheckerId}</td>";
                var checkerName = $"<td>{log.CheckerName}</td>";
                var checkerDate = $"<td>{log.CheckerDate}</td>";
                var checkerIp = $"<td>{log.CheckerIp}</td>";


                var row = $"<tr>{id}{userId}{userName}{creationTime}{ipAddress}{changeType}{entityType}{entityId}{propertyName}{originalValue}{newValue}{checkerId}{checkerName}{checkerDate}{checkerIp}</tr>";
                rows.Append(row);
            }



            var style = @"html * { 
                            /* font-size: 12px; */
                        }
                        h4 {
                            color: #fff;
                            background-color: #005B83;
                            text-align: center;
                            padding: 6px;
                        }
                        thead {
                            display: table-header-group;
                        }
                        table {
                            border-collapse: collapse;
                            width: 100%;
                        }
                        table td, table th {
                            border: 1px solid #005B83;
                            padding: 8px;
                        }
                        table tr:nth-child(even) {
                            background-color: #e6f7ff;
                        }
                        table tr:hover {
                            background-color: #ddd;
                        }
                        table th {
                            padding-top: 12px;
                            padding-bottom: 12px;
                            text-align: left;
                            background-color: #005B83;
                            color: white;
                        }";

            var table = $"<table>{headers}{rows.ToString()}</table>";
            return $"<!DOCTYPE html><html><head><style>{style}</style></head><body>{table}</body></html>";
        }

        protected void SavePdf(byte[] bytePackage, FileDto file)
        {
            _tempFileCacheManager.SetFile(file.FileToken, bytePackage);
        }
    }
}


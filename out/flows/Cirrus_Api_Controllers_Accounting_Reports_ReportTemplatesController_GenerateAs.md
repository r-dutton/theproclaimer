[web] POST /api/accounting/reports/templates/generate/download  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GenerateAs)  [L220–L225] status=201 [auth=user]
  └─ sends_request BuildReportCommand -> BuildReportHandler [L223]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.BuildReportHandler.Handle [L49–L105]
      └─ maps_to ReportTemplateCreateModel [L101]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L75]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<File> (Scoped (inferred))
        └─ method WriteQuery [L66]
          └─ implementation Cirrus.Data.Repository.Accounting.FileRepository.WriteQuery
  └─ impact_summary
    └─ requests 1
      └─ BuildReportCommand
    └─ handlers 1
      └─ BuildReportHandler
    └─ mappings 1
      └─ ReportTemplateCreateModel


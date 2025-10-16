[web] POST /api/accounting/reports/templates/generate/download  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GenerateAs)  [L220–L225] status=201 [auth=user]
  └─ sends_request BuildReportCommand [L223]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.BuildReportHandler.Handle [L49–L105]
      └─ maps_to ReportTemplateCreateModel [L101]
      └─ uses_service IControlledRepository<File>
        └─ method WriteQuery [L66]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L101]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L75]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)


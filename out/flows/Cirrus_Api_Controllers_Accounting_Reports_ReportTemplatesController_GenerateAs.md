[web] POST /api/accounting/reports/templates/generate/download  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GenerateAs)  [L220–L225] [auth=user]
  └─ sends_request BuildReportCommand [L223]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.BuildReportHandler.Handle [L49–L105]
      └─ maps_to ReportTemplateCreateModel [L101]
      └─ uses_service IControlledRepository<File>
        └─ method WriteQuery [L66]
      └─ uses_service IMapper
        └─ method Map [L101]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L75]


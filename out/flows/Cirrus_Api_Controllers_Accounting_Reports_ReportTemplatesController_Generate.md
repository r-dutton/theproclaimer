[web] POST /api/accounting/reports/templates/{templateId}/generate  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.Generate)  [L209–L214] [auth=user]
  └─ sends_request BuildReportCommand [L212]
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


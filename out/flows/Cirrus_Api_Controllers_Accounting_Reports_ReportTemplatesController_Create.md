[web] POST /api/accounting/reports/templates/  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.Create)  [L175–L192] [auth=user]
  └─ calls FileRepository.WriteQuery [L178]
  └─ calls ReportTemplateRepository.Add [L190]
  └─ calls ReportTemplateRepository.WriteQuery [L181]
  └─ writes_to File [L178]
    └─ reads_from Files
  └─ writes_to ReportTemplate [L190]
  └─ writes_to ReportTemplate [L181]
  └─ uses_service IControlledRepository<File>
    └─ method WriteQuery [L178]
  └─ uses_service IControlledRepository<ReportTemplate>
    └─ method WriteQuery [L181]
  └─ sends_request GetReportTemplateParametersQuery [L188]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.GetReportTemplateParametersQueryHandler.Handle [L31–L86]
      └─ calls ReportContentRepository.LoadWriteProperties [L82]
      └─ uses_service IControlledRepository<Dataset>
        └─ method WriteQuery [L66]
      └─ uses_service IControlledRepository<Entity>
        └─ method WriteQuery [L74]
      └─ uses_service IControlledRepository<ReportPageType>
        └─ method WriteQuery [L71]
  └─ sends_request CanIAccessFileQuery [L179]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessFileQueryHandler.Handle [L43–L101]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L66]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L90]
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L68]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L68]
      └─ uses_cache IDistributedCache [L79]
        └─ method SetRecordAsync [write] [L79]
      └─ uses_cache IDistributedCache [L71]
        └─ method DoesRecordExistAsync [access] [L71]
      └─ uses_cache IDistributedCache [L68]
        └─ method CreateAccessKey [write] [L68]


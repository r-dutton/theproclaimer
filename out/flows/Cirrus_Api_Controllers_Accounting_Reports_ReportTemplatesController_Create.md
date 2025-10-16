[web] POST /api/accounting/reports/templates/  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.Create)  [L175–L192] status=201 [auth=user]
  └─ calls FileRepository.WriteQuery [L178]
  └─ calls ReportTemplateRepository.Add [L190]
  └─ calls ReportTemplateRepository.WriteQuery [L181]
  └─ write File [L178]
    └─ reads_from Files
  └─ insert ReportTemplate [L190]
  └─ write ReportTemplate [L181]
  └─ uses_service IControlledRepository<File>
    └─ method WriteQuery [L178]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<ReportTemplate>
    └─ method WriteQuery [L181]
      └─ ... (no implementation details available)
  └─ sends_request GetReportTemplateParametersQuery [L188]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.GetReportTemplateParametersQueryHandler.Handle [L31–L86]
      └─ calls ReportContentRepository.LoadWriteProperties [L82]
      └─ uses_service IControlledRepository<Dataset>
        └─ method WriteQuery [L66]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Entity>
        └─ method WriteQuery [L74]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<ReportPageType>
        └─ method WriteQuery [L71]
          └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L179]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessFileQueryHandler.Handle [L43–L101]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L66]
          └─ implementation IRequestInfoService.IsValidServiceAccountRequest [L20-L20]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L90]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L68]
          └─ implementation ITenantService.GetCurrentTenant [L14-L14]
          └─ ... (no implementation details available)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L68]
          └─ implementation IUserService.GetUserId [L18-L18]
          └─ ... (no implementation details available)
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L79]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L71]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L68]


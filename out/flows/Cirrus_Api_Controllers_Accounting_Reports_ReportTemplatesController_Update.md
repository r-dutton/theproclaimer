[web] PUT /api/accounting/reports/templates/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.Update)  [L279–L295] status=200 [auth=user]
  └─ maps_to ReportTemplateModifiedInfoDto [L294]
  └─ calls PublishedReportBatchRepository.ReadQuery [L289]
  └─ query PublishedReportBatch [L289]
    └─ reads_from PublishedReportBatches
  └─ uses_service IControlledRepository<PublishedReportBatch>
    └─ method ReadQuery [L289]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L294]
      └─ ... (no implementation details available)
  └─ sends_request GetReportTemplateParametersQuery [L292]
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
  └─ sends_request CanIAccessFileQuery [L285]
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


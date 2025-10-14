[web] PUT /api/accounting/reports/templates/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.Update)  [L279–L295] [auth=user]
  └─ maps_to ReportTemplateModifiedInfoDto [L294]
  └─ calls PublishedReportBatchRepository.ReadQuery [L289]
  └─ queries PublishedReportBatch [L289]
    └─ reads_from PublishedReportBatches
  └─ uses_service IControlledRepository<PublishedReportBatch>
    └─ method ReadQuery [L289]
  └─ uses_service IMapper
    └─ method Map [L294]
  └─ sends_request GetReportTemplateParametersQuery [L292]
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
  └─ sends_request CanIAccessFileQuery [L285]
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


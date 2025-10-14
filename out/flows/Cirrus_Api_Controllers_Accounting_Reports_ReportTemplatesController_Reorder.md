[web] PUT /api/accounting/reports/templates/{id}/reorder  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.Reorder)  [L312–L330] [auth=user]
  └─ calls ReportTemplateRepository.WriteQuery [L321]
  └─ calls ReportTemplateRepository.ReadQuery [L315]
  └─ queries ReportTemplate [L315]
  └─ writes_to ReportTemplate [L321]
  └─ uses_service IControlledRepository<ReportTemplate>
    └─ method ReadQuery [L315]
  └─ sends_request CanIAccessFileQuery [L319]
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


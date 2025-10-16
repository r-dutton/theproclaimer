[web] GET /api/accounting/reports/templates/forfile/{fileId}  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetAll)  [L160–L169] status=200 [auth=user]
  └─ maps_to ReportTemplateLightDto [L164]
    └─ automapper.registration CirrusMappingProfile (ReportTemplate->ReportTemplateLightDto) [L546]
  └─ calls ReportTemplateRepository.ReadQuery [L164]
  └─ query ReportTemplate [L164]
  └─ uses_service IControlledRepository<ReportTemplate>
    └─ method ReadQuery [L164]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L163]
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


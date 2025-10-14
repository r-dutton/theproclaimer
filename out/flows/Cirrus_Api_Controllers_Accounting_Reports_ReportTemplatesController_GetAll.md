[web] GET /api/accounting/reports/templates/forfile/{fileId}  (Cirrus.Api.Controllers.Accounting.Reports.ReportTemplatesController.GetAll)  [L160–L169] [auth=user]
  └─ maps_to ReportTemplateLightDto [L164]
    └─ automapper.registration CirrusMappingProfile (ReportTemplate->ReportTemplateLightDto) [L546]
  └─ calls ReportTemplateRepository.ReadQuery [L164]
  └─ queries ReportTemplate [L164]
  └─ uses_service IControlledRepository<ReportTemplate>
    └─ method ReadQuery [L164]
  └─ sends_request CanIAccessFileQuery [L163]
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


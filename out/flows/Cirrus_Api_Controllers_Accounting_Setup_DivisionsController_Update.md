[web] PUT /api/accounting/divisions/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Setup.DivisionsController.Update)  [L83–L90] [auth=user]
  └─ calls DivisionRepository.WriteQuery [L86]
  └─ writes_to Division [L86]
    └─ reads_from Divisions
  └─ uses_service IControlledRepository<Division>
    └─ method WriteQuery [L86]
  └─ sends_request CanIAccessFileQuery [L87]
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


[web] DELETE /api/accounting/files/{id:Guid}  (Cirrus.Api.Controllers.Accounting.FilesController.Delete)  [L318–L332] [auth=user]
  └─ calls FileRepository.Remove [L323]
  └─ calls FileRepository.WriteQuery [L321]
  └─ writes_to File [L323]
    └─ reads_from Files
  └─ writes_to File [L321]
    └─ reads_from Files
  └─ uses_service IControlledRepository<File>
    └─ method WriteQuery [L321]
  └─ sends_request CanIAccessFileQuery [L322]
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


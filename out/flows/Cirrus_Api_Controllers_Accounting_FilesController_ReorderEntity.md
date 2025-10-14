[web] PUT /api/accounting/files/{id:Guid}/reorder-entity/{entityId:Guid}  (Cirrus.Api.Controllers.Accounting.FilesController.ReorderEntity)  [L294–L301] [auth=user]
  └─ calls FileRepository.WriteQuery [L297]
  └─ writes_to File [L297]
    └─ reads_from Files
  └─ uses_service IControlledRepository<File>
    └─ method WriteQuery [L297]
  └─ sends_request CanIAccessFileQuery [L298]
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


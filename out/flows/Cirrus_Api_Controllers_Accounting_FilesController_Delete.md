[web] DELETE /api/accounting/files/{id:Guid}  (Cirrus.Api.Controllers.Accounting.FilesController.Delete)  [L318–L332] status=200 [auth=user]
  └─ calls FileRepository.Remove [L323]
  └─ calls FileRepository.WriteQuery [L321]
  └─ delete File [L323]
    └─ reads_from Files
  └─ write File [L321]
    └─ reads_from Files
  └─ uses_service IControlledRepository<File>
    └─ method WriteQuery [L321]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L322]
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


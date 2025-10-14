[web] POST /api/workpaper-records/  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.Create)  [L167–L180] [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.ReadQuery [L170]
  └─ queries Binder [L170]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L170]
  └─ sends_request CreateWorkpaperRecordCommand [L177]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.WorkpaperRecords.CreateWorkpaperRecordCommandHandler.Handle [L36–L149]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L99]
      └─ uses_service IControlledRepository<LoanMatrix>
        └─ method WriteQuery [L104]
      └─ uses_service IControlledRepository<WorkpaperRecord>
        └─ method WriteQuery [L60]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L70]
  └─ sends_request CanIAccessBinderQuery [L175]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.CanIAccessBinderQueryHandler.Handle [L60–L126]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L101]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L89]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L117]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L92]
      └─ uses_service UserService
        └─ method GetUserId [L91]
      └─ uses_cache IDistributedCache [L121]
        └─ method SetRecordAsync [write] [L121]
      └─ uses_cache IDistributedCache [L109]
        └─ method DoesRecordExistAsync [access] [L109]
      └─ uses_cache IDistributedCache [L92]
        └─ method CreateAccessKey [write] [L92]


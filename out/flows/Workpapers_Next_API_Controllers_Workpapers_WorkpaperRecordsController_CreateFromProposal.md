[web] POST /api/workpaper-records/from-proposal  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.CreateFromProposal)  [L182–L197] [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.ReadQuery [L185]
  └─ queries Binder [L185]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L185]
  └─ sends_request CreateWorkpaperRecordCommand [L194]
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
  └─ sends_request CreateWorkpaperRecordModelFromProposalCommand [L193]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.WorkpaperRecords.CreateWorkpaperRecordModelFromProposalCommandHandler.Handle [L34–L335]
      └─ uses_service IControlledRepository<BinderRecordTemplate>
        └─ method ReadQuery [L85]
      └─ uses_service IControlledRepository<RecordStatus> (AddScoped)
        └─ method WriteQuery [L70]
      └─ uses_service IControlledRepository<RollOverRecord>
        └─ method ReadQuery [L270]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L136]
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
        └─ method ReadQuery [L110]
      └─ uses_service IControlledRepository<Worksheet>
        └─ method ReadQuery [L198]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L183]
      └─ uses_service UnitOfWork
        └─ method LocalTable [L268]
  └─ sends_request CanIAccessBinderQuery [L191]
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


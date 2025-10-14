[web] POST /api/binders/{binderId:guid}/automation-runs  (Workpapers.Next.API.Controllers.Workpapers.AutomationBots.AutomationRunsController.CreateAutomationRun)  [L70–L80] [auth=AuthorizationPolicies.User]
  └─ sends_request CreateAutomationRunCommand [L77]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.AutomationBots.CreateAutomationRunCommandHandler.Handle [L30–L86]
      └─ maps_to AutomationRunDto [L72]
        └─ converts_to AutomationRunUpdateModel [L960]
      └─ uses_service IBotService (AddScoped)
        └─ method GetBot [L62]
      └─ uses_service IControlledRepository<AutomationRun>
        └─ method Add [L70]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L54]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L64]
      └─ uses_service IMapper
        └─ method Map [L72]
  └─ sends_request CanIAccessBinderQuery [L74]
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


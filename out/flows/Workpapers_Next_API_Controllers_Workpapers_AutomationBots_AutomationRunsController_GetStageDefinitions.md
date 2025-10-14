[web] GET /api/binders/{binderId:guid}/automation-runs/{id:guid}/stage-definitions  (Workpapers.Next.API.Controllers.Workpapers.AutomationBots.AutomationRunsController.GetStageDefinitions)  [L52–L58] [auth=AuthorizationPolicies.User]
  └─ sends_request CanIAccessBinderQuery [L55]
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
  └─ sends_request GetStageDefinitionsForAutomationRunQuery [L57]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.AutomationBots.GetStageDefinitionsForAutomationRunQueryHandler.Handle [L30–L53]
      └─ maps_to StageDefinitionDto [L51]
      └─ uses_service BotService
        └─ method GetStageDefinitions [L51]
      └─ uses_service IControlledRepository<AutomationRun>
        └─ method ReadQuery [L45]
      └─ uses_service IMapper
        └─ method Map [L51]


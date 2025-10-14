[web] GET /api/binders/{binderId:guid}/automation-runs/{id:guid}/stages  (Workpapers.Next.API.Controllers.Workpapers.AutomationBots.AutomationRunsController.GetStages)  [L60–L68] [auth=AuthorizationPolicies.User]
  └─ sends_request CanIAccessBinderQuery [L63]
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
  └─ sends_request GetStageForAutomationRunQuery [L66]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.AutomationBots.GetStagesForAutomationRunQueryHandler.Handle [L49–L138]
      └─ maps_to AutomationRunAccountDto [L83]
        └─ converts_to AutomationRunAccountModel [L962]
      └─ maps_to AutomationRunDatumDto [L95]
        └─ converts_to AutomationRunDatumModel [L965]
      └─ maps_to AutomationRunDocumentDto [L99]
        └─ converts_to AutomationRunDocumentModel [L968]
      └─ maps_to AutomationRunJournalDto [L87]
        └─ converts_to AutomationRunJournalModel [L963]
      └─ maps_to AutomationRunRecordDto [L91]
        └─ converts_to AutomationRunRecordModel [L964]
      └─ maps_to AutomationRunStageDto [L70]
        └─ converts_to AutomationRunStageModel [L961]
      └─ maps_to AutomationRunStageDto [L63]
        └─ converts_to AutomationRunStageModel [L961]
      └─ uses_service IControlledRepository<AutomationRun>
        └─ method ReadQuery [L122]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L64]
  └─ sends_request GetStageForAutomationRunWithDataQuery [L67]


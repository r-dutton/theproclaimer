[web] PUT /api/proposed-items/automation-bots/refresh  (Workpapers.Next.API.Controllers.Workpapers.ProposedItemsController.RefreshAutomationBots)  [L141–L158] [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.WriteQuery [L144]
  └─ writes_to Binder [L144]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method WriteQuery [L144]
  └─ sends_request CreateUpdateProposedItemsCommand [L155]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.ProposedItems.CreateUpdateProposedItemsCommandHandler.Handle [L36–L184]
      └─ uses_service IControlledRepository<ProposedItem>
        └─ method WriteQuery [L63]
  └─ sends_request CanIAccessBinderQuery [L152]
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
  └─ sends_request GetAutomationProposalsQuery [L154]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.AutomationBots.GetAutomationProposalsQueryHandler.Handle [L35–L222]
      └─ uses_service BotService
        └─ method GetBotDefinitions [L66]
      └─ uses_service IControlledRepository<AutomationRun>
        └─ method ReadQuery [L96]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L59]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L103]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L78]


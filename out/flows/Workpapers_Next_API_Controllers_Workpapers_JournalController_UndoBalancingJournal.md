[web] DELETE /api/journals/balancing-journal/{journalId}  (Workpapers.Next.API.Controllers.Workpapers.JournalController.UndoBalancingJournal)  [L220–L227] [auth=AuthorizationPolicies.User]
  └─ sends_request UndoBalancingJournalCommand [L224]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.UndoBalancingJournalCommandHandler.Handle [L34–L94]
      └─ calls JournalRepository.Remove [L68]
      └─ calls JournalRepository.WriteQueryWithArchived [L64]
      └─ calls JournalRepository.WriteQuery [L53]
      └─ uses_service ApiService (SingleInstance)
        └─ method GetFeatures [L81]
      └─ uses_service ILogger<UndoBalancingJournalCommandHandler>
        └─ method LogError [L90]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L57]


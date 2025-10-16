[web] DELETE /api/journals/balancing-journal/{journalId}  (Workpapers.Next.API.Controllers.Workpapers.JournalController.UndoBalancingJournal)  [L220–L227] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request UndoBalancingJournalCommand [L224]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.UndoBalancingJournalCommandHandler.Handle [L34–L94]
      └─ calls JournalRepository.Remove [L68]
      └─ calls JournalRepository.WriteQueryWithArchived [L64]
      └─ calls JournalRepository.WriteQuery [L53]
      └─ uses_service ApiService (SingleInstance)
        └─ method GetFeatures [L81]
          └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetFeatures [L14-L75]
      └─ uses_service ILogger<UndoBalancingJournalCommandHandler>
        └─ method LogError [L90]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L57]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
      └─ logs ILogger<UndoBalancingJournalCommandHandler> [Error] [L90]


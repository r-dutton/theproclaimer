[web] DELETE /api/journals/balancing-journal/{journalId}  (Workpapers.Next.API.Controllers.Workpapers.JournalController.UndoBalancingJournal)  [L220–L227] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request UndoBalancingJournalCommand -> UndoBalancingJournalCommandHandler [L224]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.UndoBalancingJournalCommandHandler.Handle [L34–L94]
      └─ calls JournalRepository (methods: Remove,WriteQueryWithArchived,WriteQuery) [L68]
      └─ uses_service ApiService (SingleInstance)
        └─ method GetFeatures [L81]
          └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetFeatures [L14-L75]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L57]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ logs ILogger<UndoBalancingJournalCommandHandler> [Error] [L90]
  └─ impact_summary
    └─ requests 1
      └─ UndoBalancingJournalCommand
    └─ handlers 1
      └─ UndoBalancingJournalCommandHandler


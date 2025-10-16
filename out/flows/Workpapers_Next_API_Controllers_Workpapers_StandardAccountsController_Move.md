[web] PUT /api/standard-accounts/move  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.Move)  [L166–L180] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ sends_request MoveStandardAccountsCommand -> MoveStandardAccountsCommandHandler [L177]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.MoveStandardAccountsCommandHandler.Handle [L46–L284]
      └─ uses_service ILeadScheduleService (AddScoped)
        └─ method HasParentLeadSchedule [L196]
          └─ implementation Cirrus.ApplicationService.Features.LeadSchedules.LeadScheduleService.HasParentLeadSchedule [L14-L50]
            └─ uses_service IControlledRepository<StandardAccount> (Scoped (inferred))
              └─ method GetAccountsInHierarchy [L27]
                └─ implementation Cirrus.Data.Repository.Accounting.Ledger.StandardAccountRepository.GetAccountsInHierarchy
      └─ uses_service IControlledRepository<MasterAccount> (Scoped (inferred))
        └─ method ReadQuery [L158]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.MasterAccountRepository.ReadQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L126]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<StandardAccount> (Scoped (inferred))
        └─ method ReadQuery [L75]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.StandardAccountRepository.ReadQuery
      └─ logs ILogger [Warning] [L250]
  └─ impact_summary
    └─ requests 1
      └─ MoveStandardAccountsCommand
    └─ handlers 1
      └─ MoveStandardAccountsCommandHandler


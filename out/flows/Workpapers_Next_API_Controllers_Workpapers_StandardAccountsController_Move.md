[web] PUT /api/standard-accounts/move  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.Move)  [L166–L180] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ sends_request MoveStandardAccountsCommand [L177]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.MoveStandardAccountsCommandHandler.Handle [L46–L284]
      └─ uses_service IControlledRepository<MasterAccount>
        └─ method ReadQuery [L158]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method ReadQuery [L75]
          └─ ... (no implementation details available)
      └─ uses_service ILeadScheduleService (AddScoped)
        └─ method HasParentLeadSchedule [L196]
          └─ implementation Cirrus.ApplicationService.Features.LeadSchedules.LeadScheduleService.HasParentLeadSchedule [L14-L50]
      └─ uses_service ILogger
        └─ method LogWarning [L250]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L126]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
      └─ logs ILogger [Warning] [L250]


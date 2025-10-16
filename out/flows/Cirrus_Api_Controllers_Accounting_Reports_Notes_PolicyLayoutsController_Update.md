[web] PUT /api/accounting/reports/notes/policy-layouts/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyLayoutsController.Update)  [L74–L80] status=200 [auth=Authentication.AdminPolicy]
  └─ calls PolicyLayoutRepository.WriteQuery [L78]
  └─ write PolicyLayout [L78]
    └─ reads_from PolicyLayouts
  └─ sends_request OptimisePolicyLayoutModelCommand -> OptimisePolicyLayoutModelCommandHandler [L77]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Reports.PolicyLayouts.OptimisePolicyLayoutModelCommandHandler.Handle [L24–L94]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L37]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ PolicyLayout writes=1 reads=0
    └─ requests 1
      └─ OptimisePolicyLayoutModelCommand
    └─ handlers 1
      └─ OptimisePolicyLayoutModelCommandHandler


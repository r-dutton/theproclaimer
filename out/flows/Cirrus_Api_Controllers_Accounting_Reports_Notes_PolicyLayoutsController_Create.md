[web] POST /api/accounting/reports/notes/policy-layouts/  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyLayoutsController.Create)  [L62–L69] status=201 [auth=Authentication.AdminPolicy]
  └─ calls PolicyLayoutRepository.Add [L67]
  └─ insert PolicyLayout [L67]
    └─ reads_from PolicyLayouts
  └─ sends_request OptimisePolicyLayoutModelCommand -> OptimisePolicyLayoutModelCommandHandler [L65]
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


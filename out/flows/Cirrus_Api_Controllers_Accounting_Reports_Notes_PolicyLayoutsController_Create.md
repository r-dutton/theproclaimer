[web] POST /api/accounting/reports/notes/policy-layouts/  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyLayoutsController.Create)  [L62–L69] status=201 [auth=Authentication.AdminPolicy]
  └─ calls PolicyLayoutRepository.Add [L67]
  └─ insert PolicyLayout [L67]
    └─ reads_from PolicyLayouts
  └─ uses_service IControlledRepository<PolicyLayout>
    └─ method Add [L67]
      └─ ... (no implementation details available)
  └─ sends_request OptimisePolicyLayoutModelCommand [L65]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Reports.PolicyLayouts.OptimisePolicyLayoutModelCommandHandler.Handle [L24–L94]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L37]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

